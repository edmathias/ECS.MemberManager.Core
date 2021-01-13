using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Dapper.Contrib.Extensions;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ECS.MemberManager.Core.DataAccess.ADO
{
    public class SponsorDal : ISponsorDal
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public SponsorDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");

            _db = new SqlConnection(cnxnString);
        }

        public SponsorDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }

        public async Task<List<Sponsor>> Fetch()
        {
            var contactForSponsorTypes =await _db.GetAllAsync<Sponsor>();
            return contactForSponsorTypes.ToList();
        }
        public async Task<Sponsor> Fetch(int id)
        {
            return await _db.GetAsync<Sponsor>(id);
        }

        public async Task<Sponsor> Insert(Sponsor contactForSponsorToInsert)
        {
            var sql = new StringBuilder();
            sql.AppendLine(
                "INSERT INTO [dbo].[Sponsors] ([PersonId], [OrganizationId], [Status], [DateOfFirstContact], ");
            sql.AppendLine("[ReferredBy], [DateSponsorAccepted], [TypeName], [Details], [SponsorUntilDate], ");
            sql.AppendLine("[Notes], [LastUpdatedBy], [LastUpdatedDate])");
            sql.AppendLine(
                "Values(@PersonId, @OrganizationId, @Status, @DateOfFirstContact, @ReferredBy, @DateSponsorAccepted,");
            sql.AppendLine(" @TypeName, @Details, @SponsorUntilDate, @Notes, @LastUpdatedBy, @LastUpdatedDate");
            sql.AppendLine("SELECT SCOPE_IDENTITY()");

            contactForSponsorToInsert.Id = await _db.ExecuteScalarAsync<int>(sql.ToString(), contactForSponsorToInsert);

            var insertedEmail = await _db.GetAsync<Sponsor>(contactForSponsorToInsert.Id);
            contactForSponsorToInsert.RowVersion = insertedEmail.RowVersion;

            return contactForSponsorToInsert;
        }

        public async Task<Sponsor> Update(Sponsor contactForSponsorToUpdate)
        {
            var sql = new StringBuilder();

            sql.AppendLine("UPDATE Sponsors");
            sql.AppendLine("SET [PersonId] = @PersonId,");
            sql.AppendLine("[OrganizationId] = @OrganizationId, );");
            sql.AppendLine("[Status] = @Status,");
            sql.AppendLine("[DateOfFirstContact] = @DateOfFirstContact,");
            sql.AppendLine("[ReferredBy] = @ReferredBy,");
            sql.AppendLine("[DateSponsorAccepted] = @DateSponsorAccepted,");
            sql.AppendLine("[TypeName] = @TypeName,");
            sql.AppendLine("[Details] = @Details,");
            sql.AppendLine("[SponsorUntilDate] = @SponsorUntilDate,");
            sql.AppendLine("[Notes] = @Notes,");
            sql.AppendLine("[LastUpdatedBy] = @LastUpdatedBy,");
            sql.AppendLine("[LastUpdatedDate] = @LastUpdatedDate");
            sql.AppendLine("OUTPUT inserted.RowVersion");
            sql.AppendLine("WHERE Id = @Id AND RowVersion = @RowVersion ");

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql.ToString(), contactForSponsorToUpdate);
            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            contactForSponsorToUpdate.RowVersion = rowVersion;

            return contactForSponsorToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<Sponsor>(new Sponsor() {Id = id});
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
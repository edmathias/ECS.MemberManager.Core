using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ECS.MemberManager.Core.DataAccess.ADO
{
    public class SponsorDal : IDal<Sponsor>
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
            var contactForSponsorTypes = await _db.GetAllAsync<Sponsor>();
            return contactForSponsorTypes.ToList();
        }

        public async Task<Sponsor> Fetch(int id)
        {
            return await _db.GetAsync<Sponsor>(id);
        }

        public async Task<Sponsor> Insert(Sponsor sponsorToInsert)
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

            sponsorToInsert.Id = await _db.ExecuteScalarAsync<int>(sql.ToString(), new
            {
                PersonId = sponsorToInsert.Person.Id,
                OrganizationId = sponsorToInsert.Organization.Id,
                Status = sponsorToInsert.Status,
                DateOfFirstContact = sponsorToInsert.DateOfFirstContact,
                ReferredBy = sponsorToInsert.ReferredBy,
                DateSponsorAccepted = sponsorToInsert.DateSponsorAccepted,
                TypeName = sponsorToInsert.TypeName,
                Details = sponsorToInsert.Details,
                SponsorUntilDate = sponsorToInsert.SponsorUntilDate,
                Notes = sponsorToInsert.Notes,
                LastUpdatedBy = sponsorToInsert.LastUpdatedBy,
                LastUpdatedDate = sponsorToInsert.LastUpdatedDate
            });

            var insertedEmail = await _db.GetAsync<Sponsor>(sponsorToInsert.Id);
            sponsorToInsert.RowVersion = insertedEmail.RowVersion;

            return sponsorToInsert;
        }

        public async Task<Sponsor> Update(Sponsor sponsorToUpdate)
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

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql.ToString(), new
            {
                Id = sponsorToUpdate.Id,
                PersonId = sponsorToUpdate.Person.Id,
                OrganizationId = sponsorToUpdate.Organization.Id,
                Status = sponsorToUpdate.Status,
                DateOfFirstContact = sponsorToUpdate.DateOfFirstContact,
                ReferredBy = sponsorToUpdate.ReferredBy,
                DateSponsorAccepted = sponsorToUpdate.DateSponsorAccepted,
                TypeName = sponsorToUpdate.TypeName,
                Details = sponsorToUpdate.Details,
                SponsorUntilDate = sponsorToUpdate.SponsorUntilDate,
                Notes = sponsorToUpdate.Notes,
                LastUpdatedBy = sponsorToUpdate.LastUpdatedBy,
                LastUpdatedDate = sponsorToUpdate.LastUpdatedDate,
                RowVersion = sponsorToUpdate.RowVersion
            });

            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            sponsorToUpdate.RowVersion = rowVersion;

            return sponsorToUpdate;
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
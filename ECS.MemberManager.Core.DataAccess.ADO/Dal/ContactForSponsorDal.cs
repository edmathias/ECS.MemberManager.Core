using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
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
    public class ContactForSponsorDal : IContactForSponsorDal
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public ContactForSponsorDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");

            _db = new SqlConnection(cnxnString);
        }

        public ContactForSponsorDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }

        public async Task<List<ContactForSponsor>> Fetch()
        {
            var contactForSponsorTypes =await _db.GetAllAsync<ContactForSponsor>();
            return contactForSponsorTypes.ToList();
        }
        public async Task<ContactForSponsor> Fetch(int id)
        {
            return await _db.GetAsync<ContactForSponsor>(id);
        }

        public async Task<ContactForSponsor> Insert(ContactForSponsor contactForSponsorToInsert)
        {
            var sql = "INSERT INTO ContactForSponsors (SponsorId,DateWhenContacted,Purpose,RecordOfDiscussion,"+
                      "PersonId,Notes, LastUpdatedBy,LastUpdatedDate) " +
                      "VALUES(@SponsorId,@DateWhenContacted,@Purpose,@RecordOfDiscussion,@PersonId,@Notes,@LastUpdatedBy,@LastUpdatedDate);" +
                      "SELECT SCOPE_IDENTITY()";

            contactForSponsorToInsert.Id = await _db.ExecuteScalarAsync<int>(sql, contactForSponsorToInsert);

            var insertedEmail = await _db.GetAsync<ContactForSponsor>(contactForSponsorToInsert.Id);
            contactForSponsorToInsert.RowVersion = insertedEmail.RowVersion;

            return contactForSponsorToInsert;
        }

        public async Task<ContactForSponsor> Update(ContactForSponsor contactForSponsorToUpdate)
        {
            var sql = "UPDATE ContactForSponsors " +
                      "SET SponsorId = @SponsorId," +
                      "DateWhenContacted = @DateWhenContacted,"+
                      "Purpose=@Purpose," +
                      "RecordOfDiscussion=@RecordOfDiscussion,"+
                      "PurposeId= @PurposeId,"+
                      "Notes=@Notes,"+
                      "LastUpdatedBy=@LastUpdatedBy," +
                      "LastUpdatedDate=@LastUpdatedDate," +
                      "OUTPUT inserted.RowVersion " +
                      "WHERE Id = @Id AND RowVersion = @RowVersion ";

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql, contactForSponsorToUpdate);
            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            contactForSponsorToUpdate.RowVersion = rowVersion;

            return contactForSponsorToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<ContactForSponsor>(new ContactForSponsor() {Id = id});
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
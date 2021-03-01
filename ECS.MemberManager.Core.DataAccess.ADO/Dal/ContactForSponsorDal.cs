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
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from ContactForSponsors cs");
            sql.AppendLine("left join Sponsors s on cs.SponsorId = s.Id");
            sql.AppendLine("left join Persons p on cs.PersonId = p.Id");
             var result = await _db.QueryAsync<ContactForSponsor,Sponsor,Person,ContactForSponsor>(sql.ToString(),
                (contact, sponsor, person) =>
                {
                    contact.Sponsor = sponsor;
                    contact.Person = person;
                    return contact;
                }
            );

             return result.ToList();
        }
        public async Task<ContactForSponsor> Fetch(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from ContactForSponsors cs");
            sql.AppendLine("left join Sponsors s on cs.SponsorId = s.Id");
            sql.AppendLine("left join Persons p on cs.PersonId = p.Id");
            sql.AppendLine($"where cs.Id = {id}");
            var result = await _db.QueryAsync<ContactForSponsor,Sponsor,Person,ContactForSponsor>(sql.ToString(),
                (contact, sponsor, person) =>
                {
                    contact.Sponsor = sponsor;
                    contact.Person = person;
                    return contact;
                }
            );

            return result.First();            
        }

        public async Task<ContactForSponsor> Insert(ContactForSponsor contactForSponsorToInsert)
        {
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO ContactForSponsors (SponsorId,DateWhenContacted,Purpose,RecordOfDiscussion,");
            sql.AppendLine("PersonId,Notes, LastUpdatedBy,LastUpdatedDate)");
            sql.AppendLine("VALUES(@SponsorId,@DateWhenContacted,@Purpose,@RecordOfDiscussion,@PersonId,");
            sql.AppendLine("@Notes,@LastUpdatedBy,@LastUpdatedDate) ");
            sql.AppendLine("SELECT SCOPE_IDENTITY()");

            contactForSponsorToInsert.Id = await _db.ExecuteScalarAsync<int>(sql.ToString(), new
            {
                SponsorId = contactForSponsorToInsert.Sponsor.Id,
                DateWhenContacted = contactForSponsorToInsert.DateWhenContacted,
                Purpose = contactForSponsorToInsert.Purpose,
                RecordOfDiscussion = contactForSponsorToInsert.RecordOfDiscussion,
                PersonId = contactForSponsorToInsert.Person.Id,
                Notes = contactForSponsorToInsert.Notes,
                LastUpdatedBy = contactForSponsorToInsert.LastUpdatedBy,
                LastUpdatedDate = contactForSponsorToInsert.LastUpdatedDate
            });

            var insertedEmail = await _db.GetAsync<ContactForSponsor>(contactForSponsorToInsert.Id);
            contactForSponsorToInsert.RowVersion = insertedEmail.RowVersion;

            return contactForSponsorToInsert;
        }

        public async Task<ContactForSponsor> Update(ContactForSponsor contactForSponsorToUpdate)
        {
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE ContactForSponsors");
            sql.AppendLine("SET SponsorId=@SponsorId,");
            sql.AppendLine("DateWhenContacted=@DateWhenContacted,");
            sql.AppendLine("Purpose=@Purpose,");
            sql.AppendLine("RecordOfDiscussion=@RecordOfDiscussion,");
            sql.AppendLine("PersonId=@PersonId,");
            sql.AppendLine("Notes=@Notes,");
            sql.AppendLine("LastUpdatedBy=@LastUpdatedBy,");
            sql.AppendLine("LastUpdatedDate=@LastUpdatedDate");
            sql.AppendLine("OUTPUT inserted.RowVersion");
            sql.AppendLine("WHERE Id = @Id AND RowVersion = @RowVersion");

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql.ToString(), new
            {
                Id=contactForSponsorToUpdate.Id,
                SponsorId = contactForSponsorToUpdate.Sponsor.Id,
                DateWhenContacted = contactForSponsorToUpdate.DateWhenContacted,
                Purpose=contactForSponsorToUpdate.Purpose,
                RecordOfDiscussion = contactForSponsorToUpdate.RecordOfDiscussion,
                PersonId = contactForSponsorToUpdate.Person.Id,
                Notes = contactForSponsorToUpdate.Notes,
                LastUpdatedBy = contactForSponsorToUpdate.LastUpdatedBy,
                LastUpdatedDate = contactForSponsorToUpdate.LastUpdatedDate,
                RowVersion = contactForSponsorToUpdate.RowVersion
            });
            
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
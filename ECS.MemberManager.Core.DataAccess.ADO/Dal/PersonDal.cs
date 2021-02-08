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
    public class PersonDal : IPersonDal
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public PersonDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");

            _db = new SqlConnection(cnxnString);
        }

        public PersonDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }

        public async Task<List<Person>> Fetch()
        {
            var sql = "select * from Persons " +
                      "left join Titles on Persons.TitleId = Titles.Id " +
                      "left join EMails on  Persons.EMailId = EMails.Id";
                      
            var result = await _db.QueryAsync<Person,Title,EMail,Person>(sql,
                (person, title, email) =>
                {
                    person.Title = title;
                    person.EMail = email;
                    return person;
                }
            );

            return result.ToList();
        }
        public async Task<Person> Fetch(int id)
        {
            var sql = "select * from Persons " +
                      "left join Titles on Persons.TitleId = Titles.Id " +
                      "left join EMails on  Persons.EMailId = EMails.Id " +
                      $"where Persons.Id = {id}";
                      
            var result = await _db.QueryAsync<Person,Title,EMail,Person>(sql,
                (person, title, email) =>
                {
                    person.Title = title;
                    person.EMail = email;
                    return person;
                }
            );

            return result.First();
        }

        public async Task<Person> Insert(Person personToInsert)
        {
            var sql = new StringBuilder();
            sql.AppendLine(
                "INSERT INTO Persons (TitleId, LastName, MiddleName, FirstName, DateOfFirstContact, BirthDate, LastUpdatedBy, LastUpdatedDate, Code, Notes, EMailId)");
            sql.AppendLine(
                "Values(@TitleId, @LastName, @MiddleName, @FirstName, @DateOfFirstContact, @BirthDate, @LastUpdatedBy, @LastUpdatedDate, @Code, @Notes, @EMailId)");
            sql.AppendLine("SELECT SCOPE_IDENTITY()");

            personToInsert.Id = await _db.ExecuteScalarAsync<int>(sql.ToString(), new
            {
                TitleId = personToInsert.Title.Id,
                LastName = personToInsert.LastName,
                MiddleName = personToInsert.MiddleName,
                FirstName = personToInsert.FirstName,
                DateOfFirstContact = personToInsert.DateOfFirstContact,
                BirthDate = personToInsert.BirthDate,
                LastUpdatedBy = personToInsert.LastUpdatedBy,
                LastUpdatedDate = personToInsert.LastUpdatedDate,
                Code = personToInsert.Code,
                Notes = personToInsert.Notes,
                EMailId = personToInsert.EMail.Id
            });

            var insertedEmail = await _db.GetAsync<Person>(personToInsert.Id);
            personToInsert.RowVersion = insertedEmail.RowVersion;

            return personToInsert;
        }

        public async Task<Person> Update(Person personToUpdate)
        {
            var sql = new StringBuilder();

            sql.AppendLine("UPDATE Persons");
            sql.AppendLine("SET [TitleId] = @TitleId,");
            sql.AppendLine("[LastName] = @LastName,");
            sql.AppendLine("[MiddleName] = @MiddleName,");
            sql.AppendLine("[FirstName] = @FirstName,");
            sql.AppendLine("[DateOfFirstContact] = @DateOfFirstContact,");
            sql.AppendLine("[BirthDate] = @BirthDate,");
            sql.AppendLine("[LastUpdatedBy] = @LastUpdatedBy,");
            sql.AppendLine("[LastUpdatedDate] = @LastUpdatedDate,");
            sql.AppendLine("[Code] = @Code,");
            sql.AppendLine("[Notes] = @Notes,");
            sql.AppendLine("[EMailId] = @EMailId");
            sql.AppendLine("OUTPUT inserted.RowVersion");
            sql.AppendLine("WHERE Id = @Id AND RowVersion = @RowVersion ");

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql.ToString(), new
            {
                Id = personToUpdate.Id,
                TitleId = personToUpdate.Title.Id,
                LastName = personToUpdate.LastName,
                MiddleName = personToUpdate.MiddleName,
                FirstName = personToUpdate.FirstName,
                DateOfFirstContact = personToUpdate.DateOfFirstContact,
                BirthDate = personToUpdate.BirthDate,
                LastUpdatedBy = personToUpdate.LastUpdatedBy,
                LastUpdatedDate = personToUpdate.LastUpdatedDate,
                Code = personToUpdate.Code,
                Notes = personToUpdate.Notes,
                RowVersion = personToUpdate.RowVersion,
                EMailId = personToUpdate.EMail.Id
            });
            
            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            personToUpdate.RowVersion = rowVersion;

            return personToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<Person>(new Person() {Id = id});
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
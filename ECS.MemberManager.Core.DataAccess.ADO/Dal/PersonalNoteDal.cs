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
    public class PersonalNoteDal : IDal<PersonalNote>
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public PersonalNoteDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");

            _db = new SqlConnection(cnxnString);
        }

        public PersonalNoteDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }

        public async Task<List<PersonalNote>> Fetch()
        {
            var sql = "select * from PersonalNotes " +
                      "left join Persons on PersonalNotes.PersonId = Persons.Id "; 

            var result = await _db.QueryAsync<PersonalNote, Person, PersonalNote>(sql,
                (personalnote, person) =>
                {
                    personalnote.Person = person;
                    return personalnote;
                }
            );

            return result.ToList();
        }

        public async Task<PersonalNote> Fetch(int id)
        {
            var sql = "select * from PersonalNotes " +
                      "left join Persons on PersonalNotes.PersonId = Persons.Id "+ 
                      $"where PersonalNotes.Id = {id}";

            var result = await _db.QueryAsync<PersonalNote, Person, PersonalNote>(sql,
                (personalnote,person ) =>
                {
                    personalnote.Person = person;
                    return personalnote;
                }
            );

            return result.First();
        }

        public async Task<PersonalNote> Insert(PersonalNote personalNoteToInsert)
        {
            var sql = new StringBuilder();
            sql.AppendLine(
                "INSERT INTO [dbo].[PersonalNotes]( [PersonId], [Description], [StartDate], [DateEnd], [LastUpdatedBy], [LastUpdatedDate], [Note])");
            sql.AppendLine(
                "Values(@PersonId, @Description, @StartDate, @DateEnd, @LastUpdatedBy, @LastUpdatedDate, @Note)");
            sql.AppendLine("SELECT SCOPE_IDENTITY()");

            personalNoteToInsert.Id = await _db.ExecuteScalarAsync<int>(sql.ToString(), new
            {
                Id = personalNoteToInsert.Id,
                PersonId = personalNoteToInsert.Person.Id,
                Description = personalNoteToInsert.Description,
                StartDate = personalNoteToInsert.StartDate,
                DateEnd = personalNoteToInsert.DateEnd,
                LastUpdatedBy = personalNoteToInsert.LastUpdatedBy,
                LastUpdatedDate = personalNoteToInsert.LastUpdatedDate,
                Note = personalNoteToInsert.Note,
            });

            var insertedNote = await _db.GetAsync<PersonalNote>(personalNoteToInsert.Id);
            personalNoteToInsert.RowVersion = insertedNote.RowVersion;

            return personalNoteToInsert;
        }

        public async Task<PersonalNote> Update(PersonalNote personToUpdate)
        {
            var sql = new StringBuilder();

            sql.AppendLine("UPDATE PersonalNotes");
            sql.AppendLine("SET [PersonId] = @PersonId,");
            sql.AppendLine("[Description] = @Description,");
            sql.AppendLine("[StartDate] = @StartDate, ");
            sql.AppendLine("[DateEnd] = @DateEnd,");
            sql.AppendLine("[LastUpdatedBy] = @LastUpdatedBy,");
            sql.AppendLine("[LastUpdatedDate] = @LastUpdatedDate, ");
            sql.AppendLine("[Note] = @Note");
            sql.AppendLine("OUTPUT inserted.RowVersion");
            sql.AppendLine("WHERE Id = @Id AND RowVersion = @RowVersion ");

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql.ToString(), new
            {
                Id = personToUpdate.Id,
                PersonId = personToUpdate.Person.Id,
                Description = personToUpdate.Description,
                StartDate = personToUpdate.StartDate,
                DateEnd = personToUpdate.DateEnd,
                LastUpdatedBy = personToUpdate.LastUpdatedBy,
                LastUpdatedDate = personToUpdate.LastUpdatedDate,
                Note = personToUpdate.Note,
                RowVersion = personToUpdate.RowVersion,
            });

            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            personToUpdate.RowVersion = rowVersion;

            return personToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<PersonalNote>(new PersonalNote() {Id = id});
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
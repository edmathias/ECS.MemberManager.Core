using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ECS.MemberManager.Core.DataAccess.ADO
{
    public class TermInOfficeDal : ITermInOfficeDal
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public TermInOfficeDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");

            _db = new SqlConnection(cnxnString);
        }

        public TermInOfficeDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }

        public async Task<List<TermInOffice>> Fetch()
        {
            var sql = "select * from TermInOffices tio " +
                      "left join Persons p on tio.PersonId = p.Id " +
                      "left join Offices o on tio.OfficeId = o.Id "; 
                      
            var result = await _db.QueryAsync<TermInOffice,Person,Office,TermInOffice>(sql,
                (term, person, office) =>
                {
                    term.Person = person;
                    term.Office = office;
                    return term;
                }
            );

            return result.ToList();
        }

        public async Task<TermInOffice> Fetch(int id)
        {
            var sql = "select * from TermInOffices tio " +
                      "left join Persons p on tio.PersonId = p.Id " +
                      "left join Offices o on tio.OfficeId = o.Id " +
                      $"WHERE tio.Id = {id}";
                      
            var result = await _db.QueryAsync<TermInOffice,Person,Office,TermInOffice>(sql,
                (term, person, office) =>
                {
                    term.Person = person;
                    term.Office = office;
                    return term;
                }
            );

            return result.FirstOrDefault();
        }

        public async Task<TermInOffice> Insert(TermInOffice eventToInsert)
        {
            var sql =
                "INSERT INTO [dbo].[TermInOffices] ([PersonId], [OfficeId], [StartDate], [LastUpdatedBy], [LastUpdatedDate], [Notes]) "+
                "SELECT @PersonId, @OfficeId, @StartDate, @LastUpdatedBy, @LastUpdatedDate, @Notes "+
                "SELECT SCOPE_IDENTITY()";
            eventToInsert.Id = await _db.ExecuteScalarAsync<int>(sql, new
            {
                Id = eventToInsert.Id,
                PersonId = eventToInsert.Person.Id,
                OfficeId = eventToInsert.Office.Id,
                StartDate = eventToInsert.StartDate,
                LastUpdatedBy = eventToInsert.LastUpdatedBy,
                LastUpdatedDate = eventToInsert.LastUpdatedDate,
                Notes = eventToInsert.Notes
            } );

            //reretrieve TermInOffice to get rowversion
            var insertedEmail = await _db.GetAsync<TermInOffice>(eventToInsert.Id);
            eventToInsert.RowVersion = insertedEmail.RowVersion;

            return eventToInsert;
        }

        public async Task<TermInOffice> Update(TermInOffice eventToUpdate)
        {
            var sql = "UPDATE [dbo].[TermInOffices] " +
                      "SET [PersonId] = @PersonId, " +
                      "[OfficeId] = @OfficeId, " +
                      "[StartDate] = @StartDate, " +
                      "[LastUpdatedBy] = @LastUpdatedBy, " +
                      "[LastUpdatedDate] = @LastUpdatedDate, " +
                      "[Notes] = @Notes " +
                      "OUTPUT inserted.RowVersion " +
                      "WHERE  [Id] = @Id AND RowVersion = @RowVersion ";

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql, new 
            {
                Id = eventToUpdate.Id,
                PersonId = eventToUpdate.Person.Id,
                OfficeId = eventToUpdate.Office.Id,
                StartDate = eventToUpdate.StartDate,
                LastUpdatedBy = eventToUpdate.LastUpdatedBy,
                LastUpdatedDate = eventToUpdate.LastUpdatedDate,
                Notes = eventToUpdate.Notes,
                RowVersion = eventToUpdate.RowVersion
            });
            
            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            eventToUpdate.RowVersion = rowVersion;

            return eventToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<TermInOffice>(new TermInOffice() {Id = id});
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
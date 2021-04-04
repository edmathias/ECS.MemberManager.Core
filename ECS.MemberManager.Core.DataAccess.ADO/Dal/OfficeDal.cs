using System.Collections.Generic;
using System.Data;
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
    public class OfficeDal : IDal<Office>
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public OfficeDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");

            _db = new SqlConnection(cnxnString);
        }

        public OfficeDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }

        public async Task<List<Office>> Fetch()
        {
            var offices = await _db.GetAllAsync<Office>();
            return offices.ToList();
        }

        public async Task<Office> Fetch(int id)
        {
            return await _db.GetAsync<Office>(id);
        }

        public async Task<Office> Insert(Office officeToInsert)
        {
            var sql =
                "INSERT INTO [dbo].[Offices] ([Name], [Term], [CalendarPeriod], [ChosenHow], [Appointer], [LastUpdatedBy], [LastUpdatedDate], [Notes]) " +
                "SELECT @Name, @Term, @CalendarPeriod, @ChosenHow, @Appointer, @LastUpdatedBy, @LastUpdatedDate, @Notes " +
                "SELECT SCOPE_IDENTITY()";
            officeToInsert.Id = await _db.ExecuteScalarAsync<int>(sql, officeToInsert);

            //reretrieve Office to get rowversion
            var insertedOffice = await _db.GetAsync<Office>(officeToInsert.Id);
            officeToInsert.RowVersion = insertedOffice.RowVersion;

            return officeToInsert;
        }

        public async Task<Office> Update(Office officeToUpdate)
        {
            var sql = "UPDATE Offices " +
                      "SET [Name] = @Name, " +
                      "[Term] = @Term, " +
                      "[CalendarPeriod] = @CalendarPeriod, " +
                      "[ChosenHow] = @ChosenHow, " +
                      "[Appointer] = @Appointer, " +
                      "[LastUpdatedBy] = @LastUpdatedBy, " +
                      "[LastUpdatedDate] = @LastUpdatedDate, " +
                      "[Notes] = @Notes " +
                      "OUTPUT inserted.RowVersion " +
                      "WHERE Id = @Id AND RowVersion = @RowVersion ";

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql, officeToUpdate);
            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            officeToUpdate.RowVersion = rowVersion;

            return officeToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<Office>(new Office() {Id = id});
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
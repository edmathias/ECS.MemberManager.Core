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
    public class MemberStatusDal : IMemberStatusDal
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public MemberStatusDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");

            _db = new SqlConnection(cnxnString);
        }

        public MemberStatusDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }

        public async Task<List<MemberStatus>> Fetch()
        {
            var eMailTypes = await _db.GetAllAsync<MemberStatus>();
            return eMailTypes.ToList();
        }

        public async Task<MemberStatus> Fetch(int id)
        {
            return await _db.GetAsync<MemberStatus>(id);
        }

        public async Task<MemberStatus> Insert(MemberStatus eMailTypeToInsert)
        {
            var sql = "INSERT INTO MemberStatuses (Description, Notes) " +
                      "VALUES(@Description, @Notes);" +
                      "SELECT SCOPE_IDENTITY()";
            eMailTypeToInsert.Id = await _db.ExecuteScalarAsync<int>(sql, eMailTypeToInsert);

            //reretrieve MemberStatus to get rowversion
            var insertedEmail = await _db.GetAsync<MemberStatus>(eMailTypeToInsert.Id);
            eMailTypeToInsert.RowVersion = insertedEmail.RowVersion;

            return eMailTypeToInsert;
        }

        public async Task<MemberStatus> Update(MemberStatus eMailTypeToUpdate)
        {
            var sql = "UPDATE MemberStatuses " +
                      "SET Description = @Description, " +
                      "Notes = @Notes " +
                      "OUTPUT inserted.RowVersion " +
                      "WHERE Id = @Id AND RowVersion = @RowVersion ";

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql, eMailTypeToUpdate);
            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            eMailTypeToUpdate.RowVersion = rowVersion;

            return eMailTypeToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<MemberStatus>(new MemberStatus() {Id = id});
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
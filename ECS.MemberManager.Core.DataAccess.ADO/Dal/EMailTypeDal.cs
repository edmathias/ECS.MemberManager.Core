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
    public class EMailTypeDal : IEMailTypeDal
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public EMailTypeDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");

            _db = new SqlConnection(cnxnString);
        }

        public EMailTypeDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }

        public async Task<List<EMailType>> Fetch()
        {
            var eMailTypes = await _db.GetAllAsync<EMailType>();
            return eMailTypes.ToList();
        }

        public async Task<EMailType> Fetch(int id)
        {
            return await _db.GetAsync<EMailType>(id);
        }

        public async Task<EMailType> Insert(EMailType eMailTypeToInsert)
        {
            var sql = "INSERT INTO EMailTypes (Description, Notes) " +
                      "VALUES(@Description, @Notes);" +
                      "SELECT SCOPE_IDENTITY()";
            eMailTypeToInsert.Id = await _db.ExecuteScalarAsync<int>(sql, eMailTypeToInsert);

            //reretrieve EMailType to get rowversion
            var insertedEmail = await _db.GetAsync<EMailType>(eMailTypeToInsert.Id);
            eMailTypeToInsert.RowVersion = insertedEmail.RowVersion;

            return eMailTypeToInsert;
        }

        public async Task<EMailType> Update(EMailType eMailTypeToUpdate)
        {
            var sql = "UPDATE EMailTypes " +
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
            await _db.DeleteAsync<EMailType>(new EMailType() {Id = id});
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
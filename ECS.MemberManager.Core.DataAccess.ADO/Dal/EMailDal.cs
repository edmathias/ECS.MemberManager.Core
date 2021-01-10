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
    public class EMailDal : IEMailDal
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public EMailDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");

            _db = new SqlConnection(cnxnString);
        }

        public EMailDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }

        public async Task<List<EMail>> Fetch()
        {
            var eMailTypes =await _db.GetAllAsync<EMail>();
            return eMailTypes.ToList();
        }
        public async Task<EMail> Fetch(int id)
        {
            return await _db.GetAsync<EMail>(id);
        }

        public async Task<EMail> Insert(EMail eMailToInsert)
        {
            var sql = "INSERT INTO EMails (EMailTypeId,EMailAddress,LastUpdatedBy,LastUpdatedDate,Notes) " +
                      "VALUES(@EMailTypeId,@EMailAddress,@LastUpdatedBy,@LastUpdatedDate,@Notes);" +
                      "SELECT SCOPE_IDENTITY()";

            eMailToInsert.Id = await _db.ExecuteScalarAsync<int>(sql, eMailToInsert);

            var insertedEmail = await _db.GetAsync<EMail>(eMailToInsert.Id);
            eMailToInsert.RowVersion = insertedEmail.RowVersion;

            return eMailToInsert;
        }

        public async Task<EMail> Update(EMail eMailToUpdate)
        {
            var sql = "UPDATE EMails " +
                      "SET EMailTypeId=@EMailTypeId," +
                      "EMailAddress=@EMailAddress," +
                      "LastUpdatedBy=@LastUpdatedBy," +
                      "LastUpdatedDate=@LastUpdatedDate," +
                      "Notes=@Notes " +
                      "OUTPUT inserted.RowVersion " +
                      "WHERE Id = @Id AND RowVersion = @RowVersion ";

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql, eMailToUpdate);
            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            eMailToUpdate.RowVersion = rowVersion;

            return eMailToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<EMail>(new EMail() {Id = id});
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
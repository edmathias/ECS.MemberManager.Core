using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
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

        public List<EMail> Fetch()
        {
            return _db.GetAll<EMail>().ToList();
        }

        public EMail Fetch(int id)
        {
            return _db.Get<EMail>(id);
        }

        public EMail Insert(EMail eMailToInsert)
        {
            var sql = "INSERT INTO EMails (EMailTypeId,EMailAddress,LastUpdatedBy,LastUpdatedDate,Notes) " +
                      "VALUES(@EMailTypeId,@EMailAddress,@LastUpdatedBy,@LastUpdatedDate,@Notes);" +
                      "SELECT SCOPE_IDENTITY()";

            eMailToInsert.Id = _db.ExecuteScalar<int>(sql, eMailToInsert);

            var insertedEmail = _db.Get<EMail>(eMailToInsert.Id);
            eMailToInsert.RowVersion = insertedEmail.RowVersion;
            
            return eMailToInsert;
        }

        public EMail Update(EMail eMailToUpdate)
        {
            var sql = "UPDATE EMails " +
                      "SET EMailTypeId=@EMailTypeId," +
                      "EMailAddress=@EMailAddress," +
                      "LastUpdatedBy=@LastUpdatedBy," +
                      "LastUpdatedDate=@LastUpdatedDate," +
                      "Notes=@Notes "+
                      "OUTPUT inserted.RowVersion " +
                      "WHERE Id = @Id AND RowVersion = @RowVersion ";
            
            var rowVersion = _db.ExecuteScalar<byte[]>(sql,eMailToUpdate);
            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            eMailToUpdate.RowVersion = rowVersion;
            
            return eMailToUpdate;
        }

        public void Delete(int id)
        {
            _db.Delete<EMail>(new EMail() {Id = id});
        }
        
        public void Dispose()
        {
            _db.Dispose(); 
        }
    }
}
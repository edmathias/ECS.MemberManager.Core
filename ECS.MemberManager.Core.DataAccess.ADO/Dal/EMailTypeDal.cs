using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
        public List<EMailType> Fetch()
        {
            return _db.GetAll<EMailType>().ToList();
        }

        public EMailType Fetch(int id)
        {
            return _db.Get<EMailType>(id);
        }

        public EMailType Insert(EMailType eMailTypeToInsert)
        {
            var sql = "INSERT INTO EMailTypes (Description, Notes) " +
                      "VALUES(@Description, @Notes);" +
                      "SELECT SCOPE_IDENTITY()";
            eMailTypeToInsert.Id = _db.ExecuteScalar<int>(sql,eMailTypeToInsert);

            //reretrieve EMailType to get rowversion
            var insertedEmail = _db.Get<EMailType>(eMailTypeToInsert.Id);
            eMailTypeToInsert.RowVersion = insertedEmail.RowVersion;
            
            return eMailTypeToInsert;
        }

        public EMailType Update(EMailType eMailTypeToUpdate)
        {
            var sql = "UPDATE EMailTypes " +
                      "SET Description = @Description, " +
                      "Notes = @Notes " +
                      "OUTPUT inserted.RowVersion " +
                      "WHERE Id = @Id AND RowVersion = @RowVersion ";

            var rowVersion = _db.ExecuteScalar<byte[]>(sql,eMailTypeToUpdate);
            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            eMailTypeToUpdate.RowVersion = rowVersion;
            
            return eMailTypeToUpdate;
        }

        public void Delete(int id)
        {
            _db.Delete<EMailType>(new EMailType() {Id = id});
        }
        
        public void Dispose()
        {
            _db.Dispose(); 
        }

  
    }
}
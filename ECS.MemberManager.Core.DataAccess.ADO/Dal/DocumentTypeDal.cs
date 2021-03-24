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
    public class DocumentTypeDal : IDocumentTypeDal
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public DocumentTypeDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");

            _db = new SqlConnection(cnxnString);
        }

        public DocumentTypeDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }

        public async Task<List<DocumentType>> Fetch()
        {
            var documentTypeTypes = await _db.GetAllAsync<DocumentType>();
            return documentTypeTypes.ToList();
        }

        public async Task<DocumentType> Fetch(int id)
        {
            return await _db.GetAsync<DocumentType>(id);
        }

        public async Task<DocumentType> Insert(DocumentType documentTypeToInsert)
        {
            var sql = "INSERT INTO DocumentTypes (Description,LastUpdatedBy,LastUpdatedDate,Notes) " +
                      "VALUES(@Description,@LastUpdatedBy,@LastUpdatedDate,@Notes);" +
                      "SELECT SCOPE_IDENTITY()";

            documentTypeToInsert.Id = await _db.ExecuteScalarAsync<int>(sql, documentTypeToInsert);

            var insertedEmail = await _db.GetAsync<DocumentType>(documentTypeToInsert.Id);
            documentTypeToInsert.RowVersion = insertedEmail.RowVersion;

            return documentTypeToInsert;
        }

        public async Task<DocumentType> Update(DocumentType documentTypeToUpdate)
        {
            var sql = "UPDATE DocumentTypes " +
                      "SET Description = @Description," +
                      "LastUpdatedBy=@LastUpdatedBy," +
                      "LastUpdatedDate=@LastUpdatedDate," +
                      "Notes=@Notes " +
                      "OUTPUT inserted.RowVersion " +
                      "WHERE Id = @Id AND RowVersion = @RowVersion ";

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql, documentTypeToUpdate);
            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            documentTypeToUpdate.RowVersion = rowVersion;

            return documentTypeToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<DocumentType>(new DocumentType() {Id = id});
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
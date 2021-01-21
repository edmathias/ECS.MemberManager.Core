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
    public class TitleDal : ITitleDal
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public TitleDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");
            
            _db = new SqlConnection(cnxnString);
        }

        public TitleDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }
        public async Task<List<Title>> Fetch()
        {
            var titles =await _db.GetAllAsync<Title>();
            return titles.ToList();
        }

        public async Task<Title> Fetch(int id)
        {
            return await _db.GetAsync<Title>(id);
        }

        public async Task<Title> Insert(Title titleToInsert)
        {
            var sql = "INSERT INTO Titles (Abbreviation,Description,DisplayOrder ) " +
                      "VALUES(@Abbreviation, @Description, @DisplayOrder );" +
                      "SELECT SCOPE_IDENTITY()";
            titleToInsert.Id = await _db.ExecuteScalarAsync<int>(sql,titleToInsert);

            //reretrieve Title to get rowversion
            var insertedOffice = await _db.GetAsync<Title>(titleToInsert.Id);
            titleToInsert.RowVersion = insertedOffice.RowVersion;
            
            return titleToInsert;
        }

        public async Task<Title> Update(Title titleToUpdate)
        {
            var sql = "UPDATE Titles " +
                      "SET Description = @Description, " +
                      "Abbreviation = @Abbreviation, " +
                      "DisplayOrder = @DisplayOrder " +
                      "OUTPUT inserted.RowVersion " +
                      "WHERE Id = @Id AND RowVersion = @RowVersion ";

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql,titleToUpdate);
            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            titleToUpdate.RowVersion = rowVersion;
            
            return titleToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<Title>(new Title() {Id = id});
        }
        
        public void Dispose()
        {
            _db.Dispose(); 
        }
    }
}
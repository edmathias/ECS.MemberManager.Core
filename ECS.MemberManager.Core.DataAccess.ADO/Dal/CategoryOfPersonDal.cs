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
    public class CategoryOfPersonDal : IDal<CategoryOfPerson> 
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public CategoryOfPersonDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");

            _db = new SqlConnection(cnxnString);
        }

        public CategoryOfPersonDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }

        public async Task<List<CategoryOfPerson>> Fetch()
        {
            var category = await _db.GetAllAsync<CategoryOfPerson>();
            return category.ToList();
        }

        public async Task<CategoryOfPerson> Fetch(int id)
        {
            return await _db.GetAsync<CategoryOfPerson>(id);
        }

        public async Task<CategoryOfPerson> Insert(CategoryOfPerson categoryToInsert)
        {
            var sql = "INSERT INTO CategoryOfPersons (Category, DisplayOrder) " +
                      "VALUES(@Category, @DisplayOrder);" +
                      "SELECT SCOPE_IDENTITY()";
            categoryToInsert.Id = await _db.ExecuteScalarAsync<int>(sql, categoryToInsert);

            var insertedEmail = await _db.GetAsync<CategoryOfPerson>(categoryToInsert.Id);
            categoryToInsert.RowVersion = insertedEmail.RowVersion;

            return categoryToInsert;
        }

        public async Task<CategoryOfPerson> Update(CategoryOfPerson categoryToUpdate)
        {
            var sql = "UPDATE CategoryOfPersons " +
                      "SET Category = @Category, " +
                      "DisplayOrder = @DisplayOrder " +
                      "OUTPUT inserted.RowVersion " +
                      "WHERE Id = @Id AND RowVersion = @RowVersion ";

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql, categoryToUpdate);
            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            categoryToUpdate.RowVersion = rowVersion;

            return categoryToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<CategoryOfPerson>(new CategoryOfPerson() {Id = id});
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
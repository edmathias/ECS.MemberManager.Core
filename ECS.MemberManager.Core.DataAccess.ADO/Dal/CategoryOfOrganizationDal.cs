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
    public class CategoryOfOrganizationDal : IDal<CategoryOfOrganization>
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public CategoryOfOrganizationDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");

            _db = new SqlConnection(cnxnString);
        }

        public CategoryOfOrganizationDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }

        public async Task<List<CategoryOfOrganization>> Fetch()
        {
            var category = await _db.GetAllAsync<CategoryOfOrganization>();
            return category.ToList();
        }

        public async Task<CategoryOfOrganization> Fetch(int id)
        {
            return await _db.GetAsync<CategoryOfOrganization>(id);
        }

        public async Task<CategoryOfOrganization> Insert(CategoryOfOrganization categoryToInsert)
        {
            var sql = "INSERT INTO CategoryOfOrganizations (Category, DisplayOrder) " +
                      "VALUES(@Category, @DisplayOrder);" +
                      "SELECT SCOPE_IDENTITY()";
            categoryToInsert.Id = await _db.ExecuteScalarAsync<int>(sql, categoryToInsert);

            var insertedEmail = await _db.GetAsync<CategoryOfOrganization>(categoryToInsert.Id);
            categoryToInsert.RowVersion = insertedEmail.RowVersion;

            return categoryToInsert;
        }

        public async Task<CategoryOfOrganization> Update(CategoryOfOrganization categoryToUpdate)
        {
            var sql = "UPDATE CategoryOfOrganizations " +
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
            await _db.DeleteAsync<CategoryOfOrganization>(new CategoryOfOrganization() {Id = id});
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
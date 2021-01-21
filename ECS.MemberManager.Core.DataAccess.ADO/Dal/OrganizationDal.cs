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
    public class OrganizationDal : IOrganizationDal
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public OrganizationDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");
            
            _db = new SqlConnection(cnxnString);
        }

        public OrganizationDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }
        
        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task<List<Organization>> Fetch()
        {
            var titles =await _db.GetAllAsync<Organization>();
            return titles.ToList();
        }

        public async Task<Organization> Fetch(int id)
        {
            return await _db.GetAsync<Organization>(id);
        }

        public async Task<Organization> Insert(Organization organizationToInsert)
        {
            var sql = "INSERT INTO dbo.Organizations (Name, OrganizationTypeId, DateOfFirstContact, LastUpdatedBy, LastUpdatedDate, Notes) "+
                        "SELECT @Name, @OrganizationTypeId, @DateOfFirstContact, @LastUpdatedBy, @LastUpdatedDate, @Notes " +
                        "SELECT SCOPE_IDENTITY()";
            organizationToInsert.Id = await _db.ExecuteScalarAsync<int>(sql,organizationToInsert);

            //reretrieve organization to get rowversion
            var insertedOffice = await _db.GetAsync<Organization>(organizationToInsert.Id);
            organizationToInsert.RowVersion = insertedOffice.RowVersion;
            
            return organizationToInsert;            
        }

        public async Task<Organization> Update(Organization organizationToUpdate)
        {
            var sql = "UPDATE dbo.Organizations " +
            "SET Name = @Name, "+
            "OrganizationTypeId = @OrganizationTypeId, "+
            "DateOfFirstContact = @DateOfFirstContact, "+
            "LastUpdatedBy = @LastUpdatedBy, "+
            "LastUpdatedDate = @LastUpdatedDate, "+
            "Notes = @Notes " +
            "OUTPUT inserted.RowVersion " +
            "WHERE  Id = @Id AND RowVersion = @RowVersion ";

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql,organizationToUpdate);
            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            organizationToUpdate.RowVersion = rowVersion;
            
            return organizationToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<Organization>(new () {Id = id});
        }
    }
}
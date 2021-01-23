﻿using System.Collections.Generic;
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
    public class OrganizationTypeDal : IOrganizationTypeDal
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public OrganizationTypeDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");
            
            _db = new SqlConnection(cnxnString);
        }

        public OrganizationTypeDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }
        public async Task<List<OrganizationType>> Fetch()
        {
            var organizations =await _db.GetAllAsync<OrganizationType>();
            return organizations.ToList();
        }

        public async Task<OrganizationType> Fetch(int id)
        {
            return await _db.GetAsync<OrganizationType>(id);
        }

        public async Task<OrganizationType> Insert(OrganizationType organizationToInsert)
        {
            var sql = "INSERT INTO OrganizationTypes (CategoryOfOrganizationId, Name, Notes) " +
                            "SELECT @CategoryOfOrganizationId, @Name, @Notes " +
                            "SELECT SCOPE_IDENTITY()";
            organizationToInsert.Id = await _db.ExecuteScalarAsync<int>(sql,organizationToInsert);

            //reretrieve OrganizationType to get rowversion
            var insertedOrganizationType = await _db.GetAsync<OrganizationType>(organizationToInsert.Id);
            organizationToInsert.RowVersion = insertedOrganizationType.RowVersion;
            
            return organizationToInsert;
        }

        public async Task<OrganizationType> Update(OrganizationType organizationToUpdate)
        {
            var sql = "UPDATE dbo.OrganizationTypes " +
                "SET CategoryOfOrganizationId = @CategoryOfOrganizationId, "+
                "Name = @Name, "+
                "Notes = @Notes " +
                "OUTPUT inserted.RowVersion " +
                "WHERE Id = @Id AND RowVersion = @RowVersion ";

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql,organizationToUpdate);
            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            organizationToUpdate.RowVersion = rowVersion;
            
            return organizationToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<OrganizationType>(new () {Id = id});
        }
        
        public void Dispose()
        {
            _db.Dispose(); 
        }
    }
}
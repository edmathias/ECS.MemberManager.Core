﻿using System.Collections.Generic;
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
    public class CategoryOfOrganizationDal : ICategoryOfOrganizationDal
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
        public List<CategoryOfOrganization> Fetch()
        {
            return _db.GetAll<CategoryOfOrganization>().ToList();
        }

        public CategoryOfOrganization Fetch(int id)
        {
            return _db.Get<CategoryOfOrganization>(id);
        }

        public CategoryOfOrganization Insert(CategoryOfOrganization eMailTypeToInsert)
        {
            var sql = "INSERT INTO CategoryOfOrganizations (Category, DisplayOrder) " +
                      "VALUES(@Category, @Notes);" +
                      "SELECT SCOPE_IDENTITY()";
            eMailTypeToInsert.Id = _db.ExecuteScalar<int>(sql,eMailTypeToInsert);

            //reretrieve CategoryOfOrganization to get rowversion
            var insertedEmail = _db.Get<CategoryOfOrganization>(eMailTypeToInsert.Id);
            eMailTypeToInsert.RowVersion = insertedEmail.RowVersion;
            
            return eMailTypeToInsert;
        }

        public CategoryOfOrganization Update(CategoryOfOrganization categoryOfOrganizationToUpdate)
        {
            var sql = "UPDATE CategoryOfOrganizations " +
                      "SET Category = @Category, " +
                      "DisplayOrder = @DisplayOrder " +
                      "OUTPUT inserted.RowVersion " +
                      "WHERE Id = @Id AND RowVersion = @RowVersion ";

            var rowVersion = _db.ExecuteScalar<byte[]>(sql,categoryOfOrganizationToUpdate);
            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            categoryOfOrganizationToUpdate.RowVersion = rowVersion;
            
            return categoryOfOrganizationToUpdate;
        }

        public void Delete(int id)
        {
            _db.Delete<CategoryOfOrganization>(new CategoryOfOrganization() {Id = id});
        }
        
        public void Dispose()
        {
            _db.Dispose(); 
        }

  
    }
}
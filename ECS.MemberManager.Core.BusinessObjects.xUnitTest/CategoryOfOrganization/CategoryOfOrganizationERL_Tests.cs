﻿using System;
using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfOrganizationERL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public CategoryOfOrganizationERL_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");
            
            if(testLibrary == "Mock")
                MockDb.ResetMockDb();
            else
            {
                if (!IsDatabaseBuilt)
                {
                    var adoDb = new ADODb();
                    adoDb.BuildMemberManagerADODb();
                    IsDatabaseBuilt = true;
                }
            }
        }

        [Fact]
        private async void CategoryOfOrganizationERL_TestNewCategoryOfOrganizationERL()
        {
            var categoryOfOrganizationEdit = await CategoryOfOrganizationERL.NewCategoryOfOrganizationERL();

            Assert.NotNull(categoryOfOrganizationEdit);
            Assert.IsType<CategoryOfOrganizationERL>(categoryOfOrganizationEdit);
        }
        
        [Fact]
        private async void CategoryOfOrganizationERL_TestGetCategoryOfOrganizationERL()
        {
            var categoryOfOrganizationEdit = 
                await CategoryOfOrganizationERL.GetCategoryOfOrganizationERL();

            Assert.NotNull(categoryOfOrganizationEdit);
            Assert.Equal(3, categoryOfOrganizationEdit.Count);
        }
        
        [Fact]
        private async void CategoryOfOrganizationERL_TestDeleteCategoryOfOrganizationERL()
        {
            const int ID_TO_DELETE = 99;
            var categoryList = 
                await CategoryOfOrganizationERL.GetCategoryOfOrganizationERL();
            var listCount = categoryList.Count;
            var categoryToDelete = categoryList.First(cl => cl.Id == ID_TO_DELETE);
            // remove is deferred delete
            var isDeleted = categoryList.Remove(categoryToDelete); 

            var categoryOfOrganizationListAfterDelete = await categoryList.SaveAsync();

            Assert.NotNull(categoryOfOrganizationListAfterDelete);
            Assert.IsType<CategoryOfOrganizationERL>(categoryOfOrganizationListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,categoryOfOrganizationListAfterDelete.Count);
        }

        [Fact]
        private async void CategoryOfOrganizationERL_TestUpdateCategoryOfOrganizationERL()
        {
            const int ID_TO_UPDATE = 1;
            
            var categoryList = 
                await CategoryOfOrganizationERL.GetCategoryOfOrganizationERL();
            var countBeforeUpdate = categoryList.Count;
            var categoryOfOrganizationToUpdate = categoryList.First(cl => cl.Id == ID_TO_UPDATE);
            categoryOfOrganizationToUpdate.Category = "Updated category";
            
            var updatedList = await categoryList.SaveAsync();
            
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void CategoryOfOrganizationERL_TestAddCategoryOfOrganizationERL()
        {
            var categoryList = 
                await CategoryOfOrganizationERL.GetCategoryOfOrganizationERL();
            var countBeforeAdd = categoryList.Count;
            
            var categoryOfOrganizationToAdd = categoryList.AddNew();
            BuildCategoryOfOrganization(categoryOfOrganizationToAdd);

            var updatedCategoryList = await categoryList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedCategoryList.Count);
        }

        private void BuildCategoryOfOrganization(CategoryOfOrganizationEC categoryToBuild)
        {
            categoryToBuild.Category = "test";
            categoryToBuild.DisplayOrder = 1;
        }
        
    }
}
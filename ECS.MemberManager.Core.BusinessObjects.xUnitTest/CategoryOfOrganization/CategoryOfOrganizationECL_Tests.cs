using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfOrganizationECL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public CategoryOfOrganizationECL_Tests()
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
        private async void CategoryOfOrganizationECL_TestNewCategoryOfOrganizationECL()
        {
            var categoryOfOrganizationErl = await CategoryOfOrganizationECL.NewCategoryOfOrganizationECL();

            Assert.NotNull(categoryOfOrganizationErl);
            Assert.IsType<CategoryOfOrganizationECL>(categoryOfOrganizationErl);
        }
        
        [Fact]
        private async void CategoryOfOrganizationECL_TestGetCategoryOfOrganizationECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();
            var categoryOfOrganizations = await dal.Fetch();

            var categoryOfOrganizationECL = await CategoryOfOrganizationECL.GetCategoryOfOrganizationECL(categoryOfOrganizations);

            Assert.NotNull(categoryOfOrganizationECL);
            Assert.Equal(3, categoryOfOrganizationECL.Count);
        }
        
        [Fact]
        private async void CategoryOfOrganizationECL_TestDeleteCategoryOfOrganizationEditChildEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();
            var categoryOfOrganizations = await dal.Fetch();

            var categoryOfOrganizationErl = await CategoryOfOrganizationECL.GetCategoryOfOrganizationECL(categoryOfOrganizations);
            var listCount = categoryOfOrganizationErl.Count;
            var categoryOfOrganizationToDelete = categoryOfOrganizationErl.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = categoryOfOrganizationErl.Remove(categoryOfOrganizationToDelete); 

            var categoryOfOrganizationListAfterDelete = await categoryOfOrganizationErl.SaveAsync();

            Assert.NotNull(categoryOfOrganizationListAfterDelete);
            Assert.IsType<CategoryOfOrganizationECL>(categoryOfOrganizationListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,categoryOfOrganizationListAfterDelete.Count);
        }

        [Fact]
        private async void CategoryOfOrganizationECL_TestUpdateCategoryOfOrganizationEditChildEntry()
        {
            const int idToUpdate = 1;
            
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();
            var categoryOfOrganizations = await dal.Fetch();
            
            var categoryOfOrganizationECL = await CategoryOfOrganizationECL.GetCategoryOfOrganizationECL(categoryOfOrganizations);
            var countBeforeUpdate = categoryOfOrganizationECL.Count;
            var categoryOfOrganizationToUpdate = categoryOfOrganizationECL.First(a => a.Id == idToUpdate);
            categoryOfOrganizationToUpdate.Category = "This was updated";

            var updatedList = await categoryOfOrganizationECL.SaveAsync();
            
            Assert.Equal("This was updated",updatedList.First(a => a.Id == idToUpdate).Category);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void CategoryOfOrganizationECL_TestAddCategoryOfOrganizationEditChildEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();
            var categoryOfOrganizations = await dal.Fetch();

            var categoryOfOrganizationECL = await CategoryOfOrganizationECL.GetCategoryOfOrganizationECL(categoryOfOrganizations);
            var countBeforeAdd = categoryOfOrganizationECL.Count;
            
            var categoryOfOrganizationToAdd = categoryOfOrganizationECL.AddNew();
            BuildValidCategoryOfOrganization(categoryOfOrganizationToAdd);

            var updatedList = await categoryOfOrganizationECL.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedList.Count);
        }

        private void BuildValidCategoryOfOrganization(CategoryOfOrganizationEC categoryOfOrganization)
        {
            categoryOfOrganization.Category = "org category";
            categoryOfOrganization.DisplayOrder = 1;
        }
        
 
    }
}
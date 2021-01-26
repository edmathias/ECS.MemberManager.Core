using System;
using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.ADO;
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
            var categoryOfOrganizationEdit = await CategoryOfOrganizationERL.NewCategoryOfOrganizationECL();

            Assert.NotNull(categoryOfOrganizationEdit);
            Assert.IsType<CategoryOfOrganizationERL>(categoryOfOrganizationEdit);
        }
        
        [Fact]
        private async void CategoryOfOrganizationECL_TestGetCategoryOfOrganizationECL()
        {
            var categoryOfOrganizations = MockDb.CategoryOfOrganizations.ToList();
            var categoryOfOrganizationEdit = 
                await CategoryOfOrganizationERL.GetCategoryOfOrganizationECL(categoryOfOrganizations);

            Assert.NotNull(categoryOfOrganizationEdit);
            Assert.Equal(3, categoryOfOrganizationEdit.Count);
        }
        
        [Fact]
        private async void CategoryOfOrganizationECL_TestDeleteCategoryOfOrganizationEditChildEntry()
        {
            var categoryOfOrganizations = MockDb.CategoryOfOrganizations.ToList();
            var categoryOfOrganizationEdit = 
                await CategoryOfOrganizationERL.GetCategoryOfOrganizationECL(categoryOfOrganizations);
            var listCount = categoryOfOrganizationEdit.Count;
            var categoryOfOrganizationToDelete = categoryOfOrganizationEdit.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = categoryOfOrganizationEdit.Remove(categoryOfOrganizationToDelete); 

            var categoryOfOrganizationListAfterDelete = await categoryOfOrganizationEdit.SaveAsync();

            Assert.NotNull(categoryOfOrganizationListAfterDelete);
            Assert.IsType<CategoryOfOrganizationERL>(categoryOfOrganizationListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,categoryOfOrganizationListAfterDelete.Count);
        }

        [Fact]
        private async void CategoryOfOrganizationECL_TestUpdateCategoryOfOrganizationEditChildEntry()
        {
            const int idToUpdate = 1;
            var categoryOfOrganizations = MockDb.CategoryOfOrganizations.ToList();
            
            var categoryOfOrganizationEditList = 
                await CategoryOfOrganizationERL.GetCategoryOfOrganizationECL(categoryOfOrganizations);
            var countBeforeUpdate = categoryOfOrganizationEditList.Count;
            var categoryOfOrganizationToUpdate = categoryOfOrganizationEditList.First(a => a.Id == idToUpdate);
            categoryOfOrganizationToUpdate.Category = "Updated category";
            
            var updatedList = await categoryOfOrganizationEditList.SaveAsync();
            
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void CategoryOfOrganizationECL_TestAddCategoryOfOrganizationEditChildEntry()
        {
            var categoryOfOrganizations = MockDb.CategoryOfOrganizations.ToList();
            var categoryOfOrganizationEditList = 
                await CategoryOfOrganizationERL.GetCategoryOfOrganizationECL(categoryOfOrganizations);
            var countBeforeAdd = categoryOfOrganizationEditList.Count;
            
            var categoryOfOrganizationToAdd = categoryOfOrganizationEditList.AddNew();
            BuildCategoryOfOrganization(categoryOfOrganizationToAdd);

            var updatedCategoryOfOrganizationECL = await categoryOfOrganizationEditList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedCategoryOfOrganizationECL.Count);
        }

        private void BuildCategoryOfOrganization(CategoryOfOrganizationEC categoryToBuild)
        {
            categoryToBuild.Category = "test";
            categoryToBuild.DisplayOrder = 1;
        }
        
    }
}
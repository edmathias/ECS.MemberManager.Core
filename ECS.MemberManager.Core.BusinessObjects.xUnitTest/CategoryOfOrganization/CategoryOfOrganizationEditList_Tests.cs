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
    public class CategoryOfOrganizationEditList_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public CategoryOfOrganizationEditList_Tests()
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
        private async void CategoryOfOrganizationEditList_TestNewEMailList()
        {
            var categoryOfOrganizationEdit = await CategoryOfOrganizationEditList.NewCategoryOfOrganizationEditList();

            Assert.NotNull(categoryOfOrganizationEdit);
            Assert.IsType<CategoryOfOrganizationEditList>(categoryOfOrganizationEdit);
        }
        
        [Fact]
        private async void CategoryOfOrganizationEditList_TestGetCategoryOfOrganizationEditList()
        {
            var categoryOfOrganizationEdit = await CategoryOfOrganizationEditList.GetCategoryOfOrganizationEditList();

            Assert.NotNull(categoryOfOrganizationEdit);
            Assert.Equal(3, categoryOfOrganizationEdit.Count);
        }
        
        [Fact]
        private async void CategoryOfOrganizationEditList_TestDeleteEMailsEntry()
        {
            var categoryOfOrganizationEdit = await CategoryOfOrganizationEditList.GetCategoryOfOrganizationEditList();
            var listCount = categoryOfOrganizationEdit.Count;
            var categoryOfOrganizationToDelete = categoryOfOrganizationEdit.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = categoryOfOrganizationEdit.Remove(categoryOfOrganizationToDelete); 

            var categoryOfOrganizationListAfterDelete = await categoryOfOrganizationEdit.SaveAsync();

            Assert.NotNull(categoryOfOrganizationListAfterDelete);
            Assert.IsType<CategoryOfOrganizationEditList>(categoryOfOrganizationListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,categoryOfOrganizationListAfterDelete.Count);
        }

        [Fact]
        private async void CategoryOfOrganizationEditList_TestUpdateEMailsEntry()
        {
            const int idToUpdate = 1;
            
            var categoryOfOrganizationEditList = await CategoryOfOrganizationEditList.GetCategoryOfOrganizationEditList();
            var countBeforeUpdate = categoryOfOrganizationEditList.Count;
            var categoryOfOrganizationToUpdate = categoryOfOrganizationEditList.First(a => a.Id == idToUpdate);
            categoryOfOrganizationToUpdate.Category = "Updated category";
            
            var updatedList = await categoryOfOrganizationEditList.SaveAsync();
            
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void CategoryOfOrganizationEditList_TestAddEMailsEntry()
        {
            var categoryOfOrganizationEditList = await CategoryOfOrganizationEditList.GetCategoryOfOrganizationEditList();
            var countBeforeAdd = categoryOfOrganizationEditList.Count;
            
            var categoryOfOrganizationToAdd = categoryOfOrganizationEditList.AddNew();
            BuildCategoryOfOrganization(categoryOfOrganizationToAdd);

            var updatedEMailsList = await categoryOfOrganizationEditList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedEMailsList.Count);
        }

        private void BuildCategoryOfOrganization(CategoryOfOrganizationEdit categoryToBuild)
        {
            categoryToBuild.Category = "test";
            categoryToBuild.DisplayOrder = 1;
        }
        
    }
}
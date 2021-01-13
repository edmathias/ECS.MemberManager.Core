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
    public class CategoryOfPersonEditList_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public CategoryOfPersonEditList_Tests()
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
        private async void CategoryOfPersonEditList_TestNewCategoryOfPersonEditList()
        {
            var categoryOfPersonEdit = await CategoryOfPersonEditList.NewCategoryOfPersonEditList();

            Assert.NotNull(categoryOfPersonEdit);
            Assert.IsType<CategoryOfPersonEditList>(categoryOfPersonEdit);
        }
        
        [Fact]
        private async void CategoryOfPersonEditList_TestGetCategoryOfPersonEditList()
        {
            var categoryOfPersonEdit = await CategoryOfPersonEditList.GetCategoryOfPersonEditList();

            Assert.NotNull(categoryOfPersonEdit);
            Assert.Equal(3, categoryOfPersonEdit.Count);
        }
        
        [Fact]
        private async void CategoryOfPersonEditList_TestDeleteCategoryOfPersonEditEntry()
        {
            var categoryOfPersonEdit = await CategoryOfPersonEditList.GetCategoryOfPersonEditList();
            var listCount = categoryOfPersonEdit.Count;
            var categoryOfPersonToDelete = categoryOfPersonEdit.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = categoryOfPersonEdit.Remove(categoryOfPersonToDelete); 

            var categoryOfPersonListAfterDelete = await categoryOfPersonEdit.SaveAsync();

            Assert.NotNull(categoryOfPersonListAfterDelete);
            Assert.IsType<CategoryOfPersonEditList>(categoryOfPersonListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,categoryOfPersonListAfterDelete.Count);
        }

        [Fact]
        private async void CategoryOfPersonEditList_TestUpdateCategoryOfPersonEditEntry()
        {
            const int idToUpdate = 1;
            
            var categoryOfPersonEditList = await CategoryOfPersonEditList.GetCategoryOfPersonEditList();
            var countBeforeUpdate = categoryOfPersonEditList.Count;
            var categoryOfPersonToUpdate = categoryOfPersonEditList.First(a => a.Id == idToUpdate);
            categoryOfPersonToUpdate.Category = "Updated category";
            
            var updatedList = await categoryOfPersonEditList.SaveAsync();
            
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void CategoryOfPersonEditList_TestAddCategoryOfPersonEditEntry()
        {
            var categoryOfPersonEditList = await CategoryOfPersonEditList.GetCategoryOfPersonEditList();
            var countBeforeAdd = categoryOfPersonEditList.Count;
            
            var categoryOfPersonToAdd = categoryOfPersonEditList.AddNew();
            BuildCategoryOfPerson(categoryOfPersonToAdd);

            var updatedCategoryOfPersonEditList = await categoryOfPersonEditList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedCategoryOfPersonEditList.Count);
        }

        private void BuildCategoryOfPerson(CategoryOfPersonEdit categoryToBuild)
        {
            categoryToBuild.Category = "test";
            categoryToBuild.DisplayOrder = 1;
        }
        
    }
}
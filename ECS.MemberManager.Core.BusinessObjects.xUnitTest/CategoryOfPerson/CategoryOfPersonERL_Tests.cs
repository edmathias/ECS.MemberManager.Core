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
    public class CategoryOfPersonERL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public CategoryOfPersonERL_Tests()
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
        private async void CategoryOfPersonERL_TestNewCategoryOfPersonERL()
        {
            var categoryOfPersonEdit = await CategoryOfPersonERL.NewCategoryOfPersonERL();

            Assert.NotNull(categoryOfPersonEdit);
            Assert.IsType<CategoryOfPersonERL>(categoryOfPersonEdit);
        }
        
        [Fact]
        private async void CategoryOfPersonERL_TestGetCategoryOfPersonERL()
        {
            var categoryOfPersonEdit = 
                await CategoryOfPersonERL.GetCategoryOfPersonERL();

            Assert.NotNull(categoryOfPersonEdit);
            Assert.Equal(3, categoryOfPersonEdit.Count);
        }
        
        [Fact]
        private async void CategoryOfPersonERL_TestDeleteCategoryOfPersonERL()
        {
            const int ID_TO_DELETE = 99;
            var categoryList = 
                await CategoryOfPersonERL.GetCategoryOfPersonERL();
            var listCount = categoryList.Count;
            var categoryToDelete = categoryList.First(cl => cl.Id == ID_TO_DELETE);
            // remove is deferred delete
            var isDeleted = categoryList.Remove(categoryToDelete); 

            var categoryOfPersonListAfterDelete = await categoryList.SaveAsync();

            Assert.NotNull(categoryOfPersonListAfterDelete);
            Assert.IsType<CategoryOfPersonERL>(categoryOfPersonListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,categoryOfPersonListAfterDelete.Count);
        }

        [Fact]
        private async void CategoryOfPersonERL_TestUpdateCategoryOfPersonERL()
        {
            const int ID_TO_UPDATE = 1;
            var categoryOfPersons = MockDb.CategoryOfPersons.ToList();
            
            var categoryList = 
                await CategoryOfPersonERL.GetCategoryOfPersonERL();
            var countBeforeUpdate = categoryList.Count;
            var categoryOfPersonToUpdate = categoryList.First(cl => cl.Id == ID_TO_UPDATE);
            categoryOfPersonToUpdate.Category = "Updated category";
            
            var updatedList = await categoryList.SaveAsync();
            
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void CategoryOfPersonERL_TestAddCategoryOfPersonERL()
        {
            var categoryList = 
                await CategoryOfPersonERL.GetCategoryOfPersonERL();
            var countBeforeAdd = categoryList.Count;
            
            var categoryOfPersonToAdd = categoryList.AddNew();
            BuildCategoryOfPerson(categoryOfPersonToAdd);

            var updatedCategoryList = await categoryList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedCategoryList.Count);
        }

        private void BuildCategoryOfPerson(CategoryOfPersonEC categoryToBuild)
        {
            categoryToBuild.Category = "test";
            categoryToBuild.DisplayOrder = 1;
        }
        
    }
}
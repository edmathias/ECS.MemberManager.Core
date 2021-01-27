using System;
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
    public class CategoryOfPersonECL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public CategoryOfPersonECL_Tests()
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
        private async void CategoryOfPersonECL_TestNewCategoryOfPersonECL()
        {
            var categoryOfPersonEdit = await CategoryOfPersonECL.NewCategoryOfPersonECL();

            Assert.NotNull(categoryOfPersonEdit);
            Assert.IsType<CategoryOfPersonECL>(categoryOfPersonEdit);
        }
        
        [Fact]
        private async void CategoryOfPersonECL_TestGetCategoryOfPersonECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfPersonDal>();
            var childData = await dal.Fetch();
            var categoryOfPersonEdit = await CategoryOfPersonECL.GetCategoryOfPersonECL(childData);

            Assert.NotNull(categoryOfPersonEdit);
            Assert.Equal(3, categoryOfPersonEdit.Count);
        }
        
        [Fact]
        private async void CategoryOfPersonECL_TestDeleteCategoryOfPersonEditEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfPersonDal>();
            var childData = await dal.Fetch();

            var categoryOfPersonEdit = await CategoryOfPersonECL.GetCategoryOfPersonECL(childData);
            var listCount = categoryOfPersonEdit.Count;
            var categoryOfPersonToDelete = categoryOfPersonEdit.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = categoryOfPersonEdit.Remove(categoryOfPersonToDelete); 

            var categoryOfPersonListAfterDelete = await categoryOfPersonEdit.SaveAsync();

            Assert.NotNull(categoryOfPersonListAfterDelete);
            Assert.IsType<CategoryOfPersonECL>(categoryOfPersonListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,categoryOfPersonListAfterDelete.Count);
        }

        [Fact]
        private async void CategoryOfPersonECL_TestUpdateCategoryOfPersonEditEntry()
        {
            const int idToUpdate = 1;
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfPersonDal>();
            var childData = await dal.Fetch();
            
            var categoryOfPersonECL = await CategoryOfPersonECL.GetCategoryOfPersonECL(childData);
            var countBeforeUpdate = categoryOfPersonECL.Count;
            var categoryOfPersonToUpdate = categoryOfPersonECL.First(a => a.Id == idToUpdate);
            categoryOfPersonToUpdate.Category = "Updated category";
            
            var updatedList = await categoryOfPersonECL.SaveAsync();
            
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void CategoryOfPersonECL_TestAddCategoryOfPersonEditEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfPersonDal>();
            var childData = await dal.Fetch();
            
            var categoryOfPersonECL = await CategoryOfPersonECL.GetCategoryOfPersonECL(childData);
            var countBeforeAdd = categoryOfPersonECL.Count;
            
            var categoryOfPersonToAdd = categoryOfPersonECL.AddNew();
            BuildCategoryOfPerson(categoryOfPersonToAdd);

            var updatedCategoryOfPersonECL = await categoryOfPersonECL.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedCategoryOfPersonECL.Count);
        }

        private void BuildCategoryOfPerson(CategoryOfPersonEC categoryToBuild)
        {
            categoryToBuild.Category = "test";
            categoryToBuild.DisplayOrder = 1;
        }
    }
}
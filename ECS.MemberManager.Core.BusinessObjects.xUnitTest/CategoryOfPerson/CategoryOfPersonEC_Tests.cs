using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfPersonEC_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public CategoryOfPersonEC_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");

            if (testLibrary == "Mock")
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
        public async Task TestCategoryOfPersonEC_NewCategoryOfPersonEC()
        {
            var category = await CategoryOfPersonEC.NewCategoryOfPersonEC();

            Assert.NotNull(category);
            Assert.IsType<CategoryOfPersonEC>(category);
            Assert.False(category.IsValid);
        }
        
        [Fact]
        public async Task TestCategoryOfPersonEC_GetCategoryOfPersonEC()
        {
            var categoryOfPersonToLoad = BuildCategoryOfPerson();
            var categoryOfPerson = await CategoryOfPersonEC.GetCategoryOfPersonEC(categoryOfPersonToLoad);

            Assert.NotNull(categoryOfPerson);
            Assert.IsType<CategoryOfPersonEC>(categoryOfPerson);
            Assert.Equal(categoryOfPersonToLoad.Id,categoryOfPerson.Id);
            Assert.Equal(categoryOfPersonToLoad.Category,categoryOfPerson.Category);
            Assert.Equal(categoryOfPersonToLoad.DisplayOrder,categoryOfPerson.DisplayOrder);
            Assert.True(categoryOfPerson.IsValid);
        }

        [Fact]
        public async Task TestCategoryOfPersonEC_CategoryRequired()
        {
            var categoryToTest = BuildCategoryOfPerson();
            var category = await CategoryOfPersonEC.GetCategoryOfPersonEC(categoryToTest);
            var isObjectValidInit = category.IsValid;
            category.Category = string.Empty;

            Assert.NotNull(category);
            Assert.True(isObjectValidInit);
            Assert.False(category.IsValid);
            Assert.Equal("Category",category.BrokenRulesCollection[0].Property);
        }

        
        private CategoryOfPerson BuildCategoryOfPerson()
        {
            var categoryToBuild = new CategoryOfPerson();
            categoryToBuild.Id = 1;
            categoryToBuild.Category = "test";
            categoryToBuild.DisplayOrder = 1;

            return categoryToBuild;
        }        
    }
}
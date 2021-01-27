using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfOrganizationER_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public CategoryOfOrganizationER_Tests()
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
        public async void TestCategoryOfOrganizationER_Get()
        {
            var categoryOfOrganization = await CategoryOfOrganizationER.GetCategoryOfOrganizationER(1);
    
            Assert.NotNull(categoryOfOrganization);
            Assert.Equal(1, categoryOfOrganization.Id);
            Assert.True(categoryOfOrganization.IsValid);
        }

        [Fact]
        public async void TestCategoryOfOrganizationER_GetNewObject()
        {
            var categoryOfOrganization = await CategoryOfOrganizationER.NewCategoryOfOrganizationER();

            Assert.NotNull(categoryOfOrganization);
            Assert.False(categoryOfOrganization.IsValid);
        }

        [Fact]
        public async void TestCategoryOfOrganizationER_UpdateExistingObjectInDatabase()
        {
            var categoryOfOrganization = await CategoryOfOrganizationER.GetCategoryOfOrganizationER(1);
            categoryOfOrganization.DisplayOrder = 2;
            
            var result = await categoryOfOrganization.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal(2,result.DisplayOrder);
        }

        [Fact]
        public async Task TestCategoryOfOrganizationER_InsertNewObjectIntoDatabase()
        {
            var categoryOfOrganization = await CategoryOfOrganizationER.NewCategoryOfOrganizationER();
            categoryOfOrganization.Category = "Category 1";

            var savedCategoryOfOrganization = await categoryOfOrganization.SaveAsync();
           
            Assert.NotNull(savedCategoryOfOrganization);
            Assert.IsType<CategoryOfOrganizationER>(savedCategoryOfOrganization);
            Assert.True( savedCategoryOfOrganization.Id > 0 );
        }

        [Fact]
        public async Task TestCategoryOfOrganizationER_DeleteObjectFromDatabase()
        {
            var categoryToDelete = await CategoryOfOrganizationER.GetCategoryOfOrganizationER(99);
            
            await CategoryOfOrganizationER.DeleteCategoryOfOrganizationER(categoryToDelete.Id);
            
            var categoryToCheck = await Assert.ThrowsAsync<Csla.DataPortalException>
                (() => CategoryOfOrganizationER.GetCategoryOfOrganizationER(categoryToDelete.Id));        
        }
        
        // test invalid state 
        [Fact]
        public async Task TestCategoryOfOrganizationER_CategoryRequired() 
        {
            var categoryOfOrganization = await CategoryOfOrganizationER.NewCategoryOfOrganizationER();
            categoryOfOrganization.Category = "Valid category";
            categoryOfOrganization.DisplayOrder = 1;
            var isObjectValidInit = categoryOfOrganization.IsValid;
            categoryOfOrganization.Category = String.Empty;

            Assert.NotNull(categoryOfOrganization);
            Assert.True(isObjectValidInit);
            Assert.False(categoryOfOrganization.IsValid);
        }
       
        [Fact]
        public async Task TestCategoryOfOrganizationER_CategoryExceedsMaxLengthOf35()
        {
            var categoryOfOrganization = await CategoryOfOrganizationER.NewCategoryOfOrganizationER();
            categoryOfOrganization.Category = "valid category";
            Assert.True(categoryOfOrganization.IsValid);

            categoryOfOrganization.Category =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(categoryOfOrganization);
            Assert.False(categoryOfOrganization.IsValid);
            Assert.Equal("The field Category must be a string or array type with a maximum length of '35'.",
                categoryOfOrganization.BrokenRulesCollection[0].Description);
 
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestCategoryOfOrganizationER_TestInvalidSave()
        {
            var categoryOfOrganization = await CategoryOfOrganizationER.NewCategoryOfOrganizationER();
            categoryOfOrganization.Category = String.Empty;
            CategoryOfOrganizationER savedCategoryOfOrganization = null;
                
            Assert.False(categoryOfOrganization.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedCategoryOfOrganization = categoryOfOrganization.Save());
        }
    }
}
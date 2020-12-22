using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfOrganizationER_Tests
    {
        public CategoryOfOrganizationER_Tests()
        {
            MockDb.ResetMockDb();
        }
        
       [Fact]
        public async void TestCategoryOfOrganizationER_Get()
        {
            var categoryOfOrganization = await CategoryOfOrganizationER.GetCategoryOfOrganization(1);

            var compareCategory = MockDb.CategoryOfOrganizations.First(dt => dt.Id == 1);
            Assert.True(categoryOfOrganization.IsValid);
            Assert.Equal(compareCategory.Category,categoryOfOrganization.Category);
            Assert.Equal(compareCategory.DisplayOrder,categoryOfOrganization.DisplayOrder);
        }

        [Fact]
        public async void TestCategoryOfOrganizationER_GetNewObject()
        {
            var categoryOfOrganization = await CategoryOfOrganizationER.NewCategoryOfOrganization();

            Assert.NotNull(categoryOfOrganization);
            Assert.False(categoryOfOrganization.IsValid);
        }

        [Fact]
        public async void TestCategoryOfOrganizationER_UpdateExistingObjectInDatabase()
        {
            var categoryOfOrganization = await CategoryOfOrganizationER.GetCategoryOfOrganization(1);
            categoryOfOrganization.DisplayOrder = 2;
            
            var result = await categoryOfOrganization.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal(2,result.DisplayOrder);
        }

        [Fact]
        public async Task TestCategoryOfOrganizationER_InsertNewObjectIntoDatabase()
        {
            var categoryOfOrganization = await CategoryOfOrganizationER.NewCategoryOfOrganization();
            categoryOfOrganization.Category = "Category 1";

            var savedCategoryOfOrganization = await categoryOfOrganization.SaveAsync();
           
            Assert.NotNull(savedCategoryOfOrganization);
            Assert.IsType<CategoryOfOrganizationER>(savedCategoryOfOrganization);
            Assert.True( savedCategoryOfOrganization.Id > 0 );
        }

        [Fact]
        public async Task TestCategoryOfOrganizationER_DeleteObjectFromDatabase()
        {
            int beforeCount = MockDb.CategoryOfOrganizations.Count();

            await CategoryOfOrganizationER.DeleteCategoryOfOrganization(99);
            
            Assert.NotEqual(MockDb.CategoryOfOrganizations.Count(),beforeCount);
        }
        
        // test invalid state 
        [Fact]
        public async Task TestCategoryOfOrganizationER_CategoryRequired() 
        {
            var categoryOfOrganization = await CategoryOfOrganizationER.NewCategoryOfOrganization();
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
            var categoryOfOrganization = await CategoryOfOrganizationER.NewCategoryOfOrganization();
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
            var categoryOfOrganization = await CategoryOfOrganizationER.NewCategoryOfOrganization();
            categoryOfOrganization.Category = String.Empty;
            CategoryOfOrganizationER savedCategoryOfOrganization = null;
                
            Assert.False(categoryOfOrganization.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedCategoryOfOrganization = categoryOfOrganization.Save());
        }
    }
}
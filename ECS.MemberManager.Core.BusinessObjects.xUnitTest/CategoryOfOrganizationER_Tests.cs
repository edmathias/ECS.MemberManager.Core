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
       [Fact]
        public async void TestCategoryOfOrganizationER_Get()
        {
            var documentType = await CategoryOfOrganizationER.GetCategoryOfOrganization(1);

            Assert.Equal(1,documentType.Id);
            Assert.True(documentType.IsValid);
        }

        [Fact]
        public async void TestCategoryOfOrganizationER_GetNewObject()
        {
            var documentType = await CategoryOfOrganizationER.NewCategoryOfOrganization();

            Assert.NotNull(documentType);
            Assert.False(documentType.IsValid);
        }

        [Fact]
        public async void TestCategoryOfOrganizationER_UpdateExistingObjectInDatabase()
        {
            var documentType = await CategoryOfOrganizationER.GetCategoryOfOrganization(1);
            documentType.DisplayOrder = 2;
            
            var result = await documentType.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal(2,result.DisplayOrder);
        }

        [Fact]
        public async Task TestCategoryOfOrganizationER_InsertNewObjectIntoDatabase()
        {
            var documentType = await CategoryOfOrganizationER.NewCategoryOfOrganization();
            documentType.Category = "Category 1";

            var savedCategoryOfOrganization = await documentType.SaveAsync();
           
            Assert.NotNull(savedCategoryOfOrganization);
            Assert.IsType<CategoryOfOrganizationER>(savedCategoryOfOrganization);
            Assert.True( savedCategoryOfOrganization.Id > 0 );
        }

        [Fact]
        public async Task TestCategoryOfOrganizationER_DeleteObjectFromDatabase()
        {
            int beforeCount = MockDb.CategoryOfOrganizations.Count();

            await CategoryOfOrganizationER.DeleteCategoryOfOrganization(1);
            
            Assert.NotEqual(MockDb.CategoryOfOrganizations.Count(),beforeCount);
        }
        
        // test invalid state 
        [Fact]
        public async Task TestCategoryOfOrganizationER_CategoryRequired() 
        {
            var documentType = await CategoryOfOrganizationER.NewCategoryOfOrganization();
            documentType.Category = "Valid category";
            documentType.DisplayOrder = 1;
            var isObjectValidInit = documentType.IsValid;
            documentType.Category = String.Empty;

            Assert.NotNull(documentType);
            Assert.True(isObjectValidInit);
            Assert.False(documentType.IsValid);
        }
       
        [Fact]
        public async Task TestCategoryOfOrganizationER_CategoryExceedsMaxLengthOf35()
        {
            var documentType = await CategoryOfOrganizationER.NewCategoryOfOrganization();
            documentType.Category = "valid category";
            Assert.True(documentType.IsValid);

            documentType.Category =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(documentType);
            Assert.False(documentType.IsValid);
            Assert.Equal("The field Category must be a string or array type with a maximum length of '35'.",
                documentType.BrokenRulesCollection[0].Description);
 
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestCategoryOfOrganizationER_TestInvalidSave()
        {
            var documentType = await CategoryOfOrganizationER.NewCategoryOfOrganization();
            documentType.Category = String.Empty;
            CategoryOfOrganizationER savedCategoryOfOrganization = null;
                
            Assert.False(documentType.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedCategoryOfOrganization = documentType.Save());
        }
    }
}
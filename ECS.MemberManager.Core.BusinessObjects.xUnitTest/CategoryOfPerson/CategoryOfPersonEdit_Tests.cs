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
    public class CategoryOfPersonEdit_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public CategoryOfPersonEdit_Tests()
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
        public async void TestCategoryOfPersonEdit_Get()
        {
            var categoryOfPerson = await CategoryOfPersonEdit.GetCategoryOfPersonEdit(1);

            Assert.Equal(1, categoryOfPerson.Id);
            Assert.True(categoryOfPerson.IsValid);
        }

        [Fact]
        public async void TestCategoryOfPersonEdit_GetNewObject()
        {
            var categoryOfPerson = await CategoryOfPersonEdit.NewCategoryOfPersonEdit();

            Assert.NotNull(categoryOfPerson);
            Assert.False(categoryOfPerson.IsValid);
        }

        [Fact]
        public async void TestCategoryOfPersonEdit_UpdateExistingObjectInDatabase()
        {
            var categoryOfPerson = await CategoryOfPersonEdit.GetCategoryOfPersonEdit(1);
            categoryOfPerson.DisplayOrder = 2;
            
            var result = await categoryOfPerson.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal(2,result.DisplayOrder);
        }

        [Fact]
        public async Task TestCategoryOfPersonEdit_InsertNewObjectIntoDatabase()
        {
            var categoryOfPerson = await CategoryOfPersonEdit.NewCategoryOfPersonEdit();
            categoryOfPerson.Category = "Category 1";

            var savedCategoryOfPerson = await categoryOfPerson.SaveAsync();
           
            Assert.NotNull(savedCategoryOfPerson);
            Assert.IsType<CategoryOfPersonEdit>(savedCategoryOfPerson);
            Assert.True( savedCategoryOfPerson.Id > 0 );
        }

        [Fact]
        public async Task TestCategoryOfPersonEdit_DeleteObjectFromDatabase()
        {
            var categoryToDelete = await CategoryOfPersonEdit.GetCategoryOfPersonEdit(99);
            
            await CategoryOfPersonEdit.DeleteCategoryOfPersonEdit(categoryToDelete.Id);
            
            var categoryToCheck = await Assert.ThrowsAsync<Csla.DataPortalException>
                (() => CategoryOfPersonEdit.GetCategoryOfPersonEdit(categoryToDelete.Id));        
        }
        
        // test invalid state 
        [Fact]
        public async Task TestCategoryOfPersonEdit_CategoryRequired() 
        {
            var categoryOfPerson = await CategoryOfPersonEdit.NewCategoryOfPersonEdit();
            categoryOfPerson.Category = "Valid category";
            categoryOfPerson.DisplayOrder = 1;
            var isObjectValidInit = categoryOfPerson.IsValid;
            categoryOfPerson.Category = String.Empty;

            Assert.NotNull(categoryOfPerson);
            Assert.True(isObjectValidInit);
            Assert.False(categoryOfPerson.IsValid);
        }
       
        [Fact]
        public async Task TestCategoryOfPersonEdit_CategoryExceedsMaxLengthOf35()
        {
            var categoryOfPerson = await CategoryOfPersonEdit.NewCategoryOfPersonEdit();
            categoryOfPerson.Category = "valid category";
            Assert.True(categoryOfPerson.IsValid);

            categoryOfPerson.Category =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(categoryOfPerson);
            Assert.False(categoryOfPerson.IsValid);
            Assert.Equal("The field Category must be a string or array type with a maximum length of '35'.",
                categoryOfPerson.BrokenRulesCollection[0].Description);
 
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestCategoryOfPersonEdit_TestInvalidSave()
        {
            var categoryOfPerson = await CategoryOfPersonEdit.NewCategoryOfPersonEdit();
            categoryOfPerson.Category = String.Empty;
            CategoryOfPersonEdit savedCategoryOfPerson = null;
                
            Assert.False(categoryOfPerson.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedCategoryOfPerson = categoryOfPerson.Save());
        }
    }
}
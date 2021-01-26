﻿using System;
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
    public class CategoryOfPersonER_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public CategoryOfPersonER_Tests()
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
        public async void TestCategoryOfPersonER_GetCategoryOfPersonER()
        {
            var categoryOfPerson = await CategoryOfPersonER.GetCategoryOfPersonER(1);

            Assert.Equal(1, categoryOfPerson.Id);
            Assert.True(categoryOfPerson.IsValid);
        }

        [Fact]
        public async void TestCategoryOfPersonER_GetNewObject()
        {
            var categoryOfPerson = await CategoryOfPersonER.NewCategoryOfPersonER();

            Assert.NotNull(categoryOfPerson);
            Assert.False(categoryOfPerson.IsValid);
        }

        [Fact]
        public async void TestCategoryOfPersonER_UpdateExistingObjectInDatabase()
        {
            var categoryOfPerson = await CategoryOfPersonER.GetCategoryOfPersonER(1);
            categoryOfPerson.DisplayOrder = 2;
            
            var result = await categoryOfPerson.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal(2,result.DisplayOrder);
        }

        [Fact]
        public async Task TestCategoryOfPersonER_InsertNewObjectIntoDatabase()
        {
            var categoryOfPerson = await CategoryOfPersonER.NewCategoryOfPersonER();
            categoryOfPerson.Category = "Category 1";

            var savedCategoryOfPerson = await categoryOfPerson.SaveAsync();
           
            Assert.NotNull(savedCategoryOfPerson);
            Assert.IsType<CategoryOfPersonER>(savedCategoryOfPerson);
            Assert.True( savedCategoryOfPerson.Id > 0 );
        }

        [Fact]
        public async Task TestCategoryOfPersonER_DeleteObjectFromDatabase()
        {
            var categoryToDelete = await CategoryOfPersonER.GetCategoryOfPersonER(99);
            
            await CategoryOfPersonER.DeleteCategoryOfPersonER(categoryToDelete.Id);
            
            var categoryToCheck = await Assert.ThrowsAsync<Csla.DataPortalException>
                (() => CategoryOfPersonER.GetCategoryOfPersonER(categoryToDelete.Id));        
        }
        
        // test invalid state 
        [Fact]
        public async Task TestCategoryOfPersonER_CategoryRequired() 
        {
            var categoryOfPerson = await CategoryOfPersonER.NewCategoryOfPersonER();
            categoryOfPerson.Category = "Valid category";
            categoryOfPerson.DisplayOrder = 1;
            var isObjectValidInit = categoryOfPerson.IsValid;
            categoryOfPerson.Category = String.Empty;

            Assert.NotNull(categoryOfPerson);
            Assert.True(isObjectValidInit);
            Assert.False(categoryOfPerson.IsValid);
        }
       
        [Fact]
        public async Task TestCategoryOfPersonER_CategoryExceedsMaxLengthOf35()
        {
            var categoryOfPerson = await CategoryOfPersonER.NewCategoryOfPersonER();
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
        public async Task TestCategoryOfPersonER_TestInvalidSave()
        {
            var categoryOfPerson = await CategoryOfPersonER.NewCategoryOfPersonER();
            categoryOfPerson.Category = String.Empty;
            CategoryOfPersonER savedCategoryOfPerson = null;
                
            Assert.False(categoryOfPerson.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedCategoryOfPerson = categoryOfPerson.Save());
        }
    }
}
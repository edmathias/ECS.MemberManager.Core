using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.Test
{
    public class CategoryOfPersonER_Tests
    {
       [Fact]
        public async Task TestCategoryOfPersonER_Get()
        {
            var person = await CategoryOfPersonER.GetCategoryOfPerson(1);

            Assert.Equal(person.Id, 1);
            Assert.True(person.IsValid);
        }

        [Fact]
        public async Task TestCategoryOfPersonER_GetNewObject()
        {
            var person = await CategoryOfPersonER.NewCategoryOfPerson();

            Assert.NotNull(person);
            Assert.IsType(typeof(CategoryOfPersonER),person);
        }

        [Fact]
        public async Task TestCategoryOfPersonER_UpdateExistingObjectInDatabase()
        {
            var person = await CategoryOfPersonER.GetCategoryOfPerson(1);
            person.DisplayOrder = 2;
            
            var result = await person.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal(result.DisplayOrder,2);
        }

        [Fact]
        public async Task TestCategoryOfPersonER_InsertNewObjectIntoDatabase()
        {
            var person = await CategoryOfPersonER.NewCategoryOfPerson();
            person.Category = "Category 1";

            var savedCategoryOfPerson = await person.SaveAsync();
           
            Assert.IsType<CategoryOfPersonER>(savedCategoryOfPerson );
            Assert.True( savedCategoryOfPerson.Id > 0 );
        }

        [Fact]
        public async Task TestCategoryOfPersonER_DeleteObjectFromDatabase()
        {
            int beforeCount = MockDb.CategoryOfPersons.Count();

            await CategoryOfPersonER.DeleteCategoryOfPerson(1);
            
            Assert.NotEqual(beforeCount,MockDb.CategoryOfPersons.Count());
        }
        
        // test invalid state 
        [Fact]
        public async Task TestCategoryOfPersonER_CategoryRequired() 
        {
            var person = await CategoryOfPersonER.NewCategoryOfPerson();
            person.Category = "valid category";
            var isObjectValidInit = person.IsValid;
            person.Category = string.Empty;

            Assert.NotNull(person);
            Assert.True(isObjectValidInit);
            Assert.False(person.IsValid);
        }
       
        [Fact]
        public async Task TestCategoryOfPersonER_DescriptionExceedsMaxLengthOf35()
        {
            var person = await CategoryOfPersonER.NewCategoryOfPerson();
            person.Category = "valid category";
            
            var isInitialObjectValid = person.IsValid;

            person.Category =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(person);
            Assert.True(isInitialObjectValid);
            Assert.False(person.IsValid);
            Assert.Equal(person.BrokenRulesCollection[0].Description,
                "The field Category must be a string or array type with a maximum length of '35'.");
 
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestCategoryOfPersonER_TestInvalidSave()
        {
            var person = await CategoryOfPersonER.NewCategoryOfPerson();
            person.Category = String.Empty;
            CategoryOfPersonER savedCategoryOfPerson = null;
                
            Assert.False(person.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedCategoryOfPerson =  person.Save() );
        }
    }
}
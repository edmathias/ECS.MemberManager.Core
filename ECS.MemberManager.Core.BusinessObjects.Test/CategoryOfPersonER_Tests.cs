using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ECS.MemberManager.Core.BusinessObjects.Test
{
    [TestClass]
    public class CategoryOfPersonER_Tests
    {
       [TestMethod]
        public async Task TestCategoryOfPersonER_Get()
        {
            var person = await CategoryOfPersonER.GetCategoryOfPerson(1);

            Assert.AreEqual(person.Id, 1);
            Assert.IsTrue(person.IsValid);
        }

        [TestMethod]
        public async Task TestCategoryOfPersonER_GetNewObject()
        {
            var person = await CategoryOfPersonER.NewCategoryOfPerson();

            Assert.IsNotNull(person);
            Assert.IsInstanceOfType(person, typeof(CategoryOfPersonER));
        }

        [TestMethod]
        public async Task TestCategoryOfPersonER_UpdateExistingObjectInDatabase()
        {
            var person = await CategoryOfPersonER.GetCategoryOfPerson(1);
            person.DisplayOrder = 2;
            
            
            var result = person.Save();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.DisplayOrder,2);
        }

        [TestMethod]
        public async Task TestCategoryOfPersonER_InsertNewObjectIntoDatabase()
        {
            var person = await CategoryOfPersonER.NewCategoryOfPerson();
            person.Category = "Category 1";

            var savedCategoryOfPerson = person.Save();
           
            Assert.IsInstanceOfType(savedCategoryOfPerson, typeof(CategoryOfPersonER));
            Assert.IsTrue( savedCategoryOfPerson.Id > 0 );
        }

        [TestMethod]
        public async Task TestCategoryOfPersonER_DeleteObjectFromDatabase()
        {
            int beforeCount = MockDb.CategoryOfPersons.Count();

            await CategoryOfPersonER.DeleteCategoryOfPerson(1);
            
            Assert.AreNotEqual(beforeCount,MockDb.CategoryOfPersons.Count());
        }
        
        // test invalid state 
        [TestMethod]
        public async Task TestCategoryOfPersonER_CategoryRequired() 
        {
            var person = await CategoryOfPersonER.NewCategoryOfPerson();
            person.Category = "valid category";
            var isObjectValidInit = person.IsValid;
            person.Category = string.Empty;

            Assert.IsNotNull(person);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(person.IsValid);
        }
       
        [TestMethod]
        public async Task TestCategoryOfPersonER_DescriptionExceedsMaxLengthOf35()
        {
            var person = await CategoryOfPersonER.NewCategoryOfPerson();
            person.Category = "valid category";
            
            var isInitialObjectValid = person.IsValid;

            person.Category =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.IsNotNull(person);
            Assert.IsTrue(isInitialObjectValid);
            Assert.IsFalse(person.IsValid);
            Assert.AreEqual(person.BrokenRulesCollection[0].Description,
                "The field Category must be a string or array type with a maximum length of '35'.");
 
        }        
        // test exception if attempt to save in invalid state

        [TestMethod]
        public async Task TestCategoryOfPersonER_TestInvalidSave()
        {
            var person = await CategoryOfPersonER.NewCategoryOfPerson();
            person.Category = String.Empty;
            CategoryOfPersonER savedCategoryOfPerson = null;
                
            Assert.IsFalse(person.IsValid);
            Assert.ThrowsException<ValidationException>(() => savedCategoryOfPerson =  person.Save() );
        }
    }
}
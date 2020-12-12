using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;

namespace ECS.MemberManager.Core.BusinessObjects.Test
{
    /// <summary>
    /// Summary description for JustMockTest
    /// </summary>
    [TestClass]
    public class PersonER_Tests 
    {

        [TestMethod]
        public async Task TestPersonER_Get()
        {
            var person = await PersonER.GetPerson(1);
 
            Assert.AreEqual(person.Id, 1);
            Assert.IsTrue(person.IsValid);
        }


        [TestMethod]
        public async Task TestPersonER_GetNewObject()
        {
            var person = await PersonER.NewPerson();

            Assert.IsNotNull(person);
            Assert.IsFalse(person.IsValid);
        }

        [TestMethod]
        public async Task TestPersonER_UpdateExistingObjectInDatabase()
        {
            var person = await PersonER.GetPerson(1);
            person.Notes = "These are updated Notes";
            
            var result = person.Save();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Notes, "These are updated Notes");
        }

        [TestMethod]
        public async Task TestPersonER_InsertNewObjectIntoDatabase()
        {
            var person = await BuildValidPersonER();

            var savedPerson = await person.SaveAsync();

            Assert.IsNotNull(savedPerson);
            Assert.IsInstanceOfType(savedPerson, typeof(PersonER));
            Assert.IsTrue( savedPerson.Id > 0 );
        }

        [TestMethod]
        public async Task TestPersonER_DeleteObjectFromDatabase()
        {
            int beforeCount = MockDb.Persons.Count();

            await PersonER.DeletePerson(1);
            
            Assert.AreNotEqual(beforeCount,MockDb.Persons.Count());
        }
        
        // test invalid state 
        [TestMethod]
        public async Task TestPersonER_DescriptionRequired() 
        {
            var person = await PersonER.NewPerson();
            person.LastUpdatedBy = "edm";
            person.LastUpdatedDate = DateTime.Now;
            var isObjectValidInit = person.IsValid;

            Assert.IsNotNull(person);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(person.IsValid);
        }
       
         // test exception if attempt to save in invalid state

        [TestMethod]
        public async Task TestPersonER_TestInvalidSave()
        {
            var person = await PersonER.NewPerson();
            PersonER savedPerson = null;
                
            Assert.IsFalse(person.IsValid);
            Assert.ThrowsException<ValidationException>(() => savedPerson =  person.Save() );
        }

        private async Task<PersonER> BuildValidPersonER()
        {
            var personER = await PersonER.NewPerson();
            
            personER.LastUpdatedBy = "edm";
            personER.LastUpdatedDate = DateTime.Now;
            personER.Notes = "notes here";

            return personER;
        }
    }
}

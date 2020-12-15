using System;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PersonER_Tests 
    {
        [Fact]
        public async Task TestPersonER_Get()
        {
            var person = await PersonER.GetPerson(1);
 
            Assert.Equal(1, person.Id);
            Assert.True(person.IsValid);
            Assert.Equal(3,person.CategoryOfPersonList.Count);
        }


        [Fact]
        public async Task TestPersonER_GetNewObject()
        {
            var person = await PersonER.NewPerson();

            Assert.NotNull(person);
        }

        [Fact]
        public async Task TestPersonER_UpdateExistingObjectInDatabase()
        {
            var person = await PersonER.GetPerson(1);
            person.Notes = "These are updated Notes";
            
            var result = person.Save();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes",result.Notes );
        }

        [Fact]
        public async Task TestPersonER_InsertNewObjectIntoDatabase()
        {
            var person = await BuildValidPersonER();

            var savedPerson = await person.SaveAsync();

            Assert.NotNull(savedPerson);
            Assert.IsType<PersonER>(savedPerson);
            Assert.True( savedPerson.Id > 0 );
        }

        [Fact]
        public async Task TestPersonER_DeleteObjectFromDatabase()
        {
            int beforeCount = MockDb.Persons.Count();

            await PersonER.DeletePerson(99);
            
            Assert.NotEqual(beforeCount,MockDb.Persons.Count());
        }
        
        // test invalid state 
        [Fact]
        public async Task TestPersonER_LastNameRequired() 
        {
            var person = await PersonER.NewPerson();
            person.LastName = "lastname";
            person.LastUpdatedBy = "edm";
            person.LastUpdatedDate = DateTime.Now;
            var isObjectValidInit = person.IsValid;
            person.LastName = string.Empty;
            
            Assert.NotNull(person);
            Assert.True(isObjectValidInit);
            Assert.False(person.IsValid);
        }
       
         // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestPersonER_TestInvalidSave()
        {
            var person = await PersonER.NewPerson();
            person.LastName = "lastname";
            person.LastUpdatedBy = "edm";
            person.LastUpdatedDate = DateTime.Now;
            var isPersonValid = person.IsValid;

            person.LastName = string.Empty;
                
            Assert.True(isPersonValid);
            Assert.False(person.IsValid);
            Assert.Throws<ValidationException>(() => person.Save() );
        }

        private async Task<PersonER> BuildValidPersonER()
        {
            var personER = await PersonER.NewPerson();
            
            personER.LastUpdatedBy = "edm";
            personER.LastUpdatedDate = DateTime.Now;
            personER.Notes = "notes here";
            personER.Code = "n/a";
            personER.BirthDate = new SmartDate(DateTime.Now);
            personER.FirstName = "Joe";
            personER.LastName = "Insert";
            personER.DateOfFirstContact = new SmartDate();
            personER.LastUpdatedBy = "edm";
            personER.LastUpdatedDate = DateTime.Now;

            return personER;
        }
    }
}

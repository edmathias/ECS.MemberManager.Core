using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.BusinessObjects;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OfficeER_Tests 
    {
        public OfficeER_Tests()
        {
            MockDb.ResetMockDb();
        }
        
        [Fact]
        public async Task TestOfficeER_Get()
        {
            var office = await OfficeER.GetOffice(1);

            Assert.Equal(1, office.Id);
            Assert.True(office.IsValid);
        }

        [Fact]
        public async Task TestOfficeER_GetNewObject()
        {
            var office = await OfficeER.NewOffice();

            Assert.NotNull(office);
            Assert.False(office.IsValid);
        }

        [Fact]
        public async Task TestOfficeER_UpdateExistingObjectInDatabase()
        {
            var office = await OfficeER.GetOffice(1);
            office.Notes = "These are updated Notes";
            
            var result = office.Save();

            Assert.NotNull(result);
            Assert.Equal( "These are updated Notes", result.Notes);
        }

        [Fact]
        public async Task TestOfficeER_InsertNewObjectIntoDatabase()
        {
            var office = await OfficeER.NewOffice();
            office.Name = "President";
            office.Term = 12;
            office.CalendarPeriod = "months";
            office.ChosenHow = 1;
            office.Appointer = "not applicable";
            office.Notes = "This is the president";
            office.LastUpdatedBy = "edm";
            office.LastUpdatedDate = DateTime.Now;

            var savedOffice = office.Save();
           
            Assert.NotNull(savedOffice);
            Assert.IsType<OfficeER>(savedOffice);
            Assert.True( savedOffice.Id > 0 );
        }

        [Fact]
        public async Task TestOfficeER_DeleteObjectFromDatabase()
        {
            int beforeCount = MockDb.Offices.Count();

            await OfficeER.DeleteOffice(99);
            
            Assert.NotEqual(beforeCount,MockDb.Offices.Count());
        }
        
        // test invalid state 
        [Fact]
        public async Task TestOfficeER_RequiredFieldTests() 
        {
            var office = await OfficeER.NewOffice();
            office.Name = "President";
            office.Term = 12;
            office.CalendarPeriod = "months";
            office.ChosenHow = 1;
            office.Appointer = "not applicable";
            office.Notes = "This is the president";
            office.LastUpdatedBy = "edm";
            office.LastUpdatedDate = DateTime.Now;
            var isObjectValidInit = office.IsValid;
            
            // act
            office.Name = string.Empty;
            var brokenCount1 = office.BrokenRulesCollection.Count;
            office.LastUpdatedBy = String.Empty;
            var brokenCount2 = office.BrokenRulesCollection.Count;
            
            // assert
            Assert.NotNull(office);
            Assert.True(isObjectValidInit);
            Assert.False(office.IsValid);
            Assert.Equal(1, brokenCount1);
            Assert.Equal(2, brokenCount2);
        }
       
        [Fact]
        public async Task TestOfficeER_NameExceedsMaxLengthOf50()
        {
            var office = await OfficeER.NewOffice();
            office.LastUpdatedBy = "edm";
            office.LastUpdatedDate = DateTime.Now;
            office.Name = "valid length";
            Assert.True(office.IsValid);
            
            office.Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(office);
            Assert.False(office.IsValid);
            Assert.Equal("The field Name must be a string or array type with a maximum length of '50'.",
                office.BrokenRulesCollection[0].Description);
 
        }   
        
        [Fact]
        public async Task TestOfficeER_AppointerExceedsMaxLengthOf50()
        {
            var office = await OfficeER.NewOffice();
            office.Name = "Name";
            office.LastUpdatedBy = "edm";
            office.LastUpdatedDate = DateTime.Now;
            office.Appointer = "valid length";
            Assert.True(office.IsValid);
            
            office.Appointer = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                               "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                               "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                               "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(office);
            Assert.False(office.IsValid);
            Assert.Equal(
                "The field Appointer must be a string or array type with a maximum length of '50'.",
                office.BrokenRulesCollection[0].Description);
 
        }        
        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestOfficeER_TestInvalidSave()
        {
            var office = await OfficeER.NewOffice();
            office.Name = String.Empty;
                
            Assert.False(office.IsValid);
            Assert.Throws<ValidationException>(() => office.Save() );
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.BusinessObjects;
using ECS.MemberManager.Core.DataAccess.Mock;

namespace ECS.MemberManager.Core.BusinessObjects.Test
{
    /// <summary>
    /// Summary description for JustMockTest
    /// </summary>
    [TestClass]
    public class OfficeER_Tests 
    {

        [TestMethod]
        public async Task TestOfficeER_Get()
        {
            var office = await OfficeER.GetOffice(1);

            Assert.AreEqual(office.Id, 1);
            Assert.IsTrue(office.IsValid);
        }

        [TestMethod]
        public async Task TestOfficeER_GetNewObject()
        {
            var office = await OfficeER.NewOffice();

            Assert.IsNotNull(office);
            Assert.IsFalse(office.IsValid);
        }

        [TestMethod]
        public async Task TestOfficeER_UpdateExistingObjectInDatabase()
        {
            var office = await OfficeER.GetOffice(1);
            office.Notes = "These are updated Notes";
            
            var result = office.Save();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Notes, "These are updated Notes");
        }

        [TestMethod]
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
           
            Assert.IsNotNull(savedOffice);
            Assert.IsInstanceOfType(savedOffice, typeof(OfficeER));
            Assert.IsTrue( savedOffice.Id > 0 );
        }

        [TestMethod]
        public async Task TestOfficeER_DeleteObjectFromDatabase()
        {
            int beforeCount = MockDb.Offices.Count();

            await OfficeER.DeleteOffice(1);
            
            Assert.AreNotEqual(beforeCount,MockDb.Offices.Count());
        }
        
        // test invalid state 
        [TestMethod]
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
            Assert.IsNotNull(office);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(office.IsValid);
            Assert.AreEqual(brokenCount1,1);
            Assert.AreEqual(brokenCount2,2);
        }
       
        [TestMethod]
        public async Task TestOfficeER_NameExceedsMaxLengthOf50()
        {
            var office = await OfficeER.NewOffice();
            office.LastUpdatedBy = "edm";
            office.LastUpdatedDate = DateTime.Now;
            office.Name = "valid length";
            Assert.IsTrue(office.IsValid);
            
            office.Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.IsNotNull(office);
            Assert.IsFalse(office.IsValid);
            Assert.AreEqual(office.BrokenRulesCollection[0].Description,
                "The field Name must be a string or array type with a maximum length of '50'.");
 
        }   
        
        [TestMethod]
        public async Task TestOfficeER_AppointerExceedsMaxLengthOf50()
        {
            var office = await OfficeER.NewOffice();
            office.Name = "Name";
            office.LastUpdatedBy = "edm";
            office.LastUpdatedDate = DateTime.Now;
            office.Appointer = "valid length";
            Assert.IsTrue(office.IsValid);
            
            office.Appointer = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                               "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                               "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                               "Duis aute irure dolor in reprehenderit";

            Assert.IsNotNull(office);
            Assert.IsFalse(office.IsValid);
            Assert.AreEqual(office.BrokenRulesCollection[0].Description,
                "The field Appointer must be a string or array type with a maximum length of '50'.");
 
        }        
        
        // test exception if attempt to save in invalid state

        [TestMethod]
        public async Task TestOfficeER_TestInvalidSave()
        {
            var office = await OfficeER.NewOffice();
            office.Name = String.Empty;
            OfficeER savedOffice = null;
                
            Assert.IsFalse(office.IsValid);
            Assert.ThrowsException<ValidationException>(() => savedOffice =  office.Save() );
        }
    }
}

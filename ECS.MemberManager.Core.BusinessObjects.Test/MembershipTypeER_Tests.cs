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
    public class MembershipTypeER_Tests 
    {

        [TestMethod]
        public async Task TestMembershipTypeER_Get()
        {
            var membershipType = await MembershipTypeER.GetMembershipType(1);

            Assert.AreEqual(membershipType.Id, 1);
            Assert.IsTrue(membershipType.IsValid);
        }

        [TestMethod]
        public async Task TestMembershipTypeER_GetNewObject()
        {
            var membershipType = await MembershipTypeER.NewMembershipType();

            Assert.IsNotNull(membershipType);
            Assert.IsFalse(membershipType.IsValid);
        }

        [TestMethod]
        public async Task TestMembershipTypeER_UpdateExistingObjectInDatabase()
        {
            var membershipType = await MembershipTypeER.GetMembershipType(1);
            membershipType.Notes = "These are updated Notes";
            
            var result = membershipType.Save();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Notes, "These are updated Notes");
        }

        [TestMethod]
        public async Task TestMembershipTypeER_InsertNewObjectIntoDatabase()
        {
            var membershipType = await MembershipTypeER.NewMembershipType();
            membershipType.Description = "Standby";
            membershipType.Notes = "This person is on standby";
            membershipType.LastUpdatedBy = "edm";
            membershipType.LastUpdatedDate = DateTime.Now;

            var savedMembershipType = membershipType.Save();
           
            Assert.IsNotNull(savedMembershipType);
            Assert.IsInstanceOfType(savedMembershipType, typeof(MembershipTypeER));
            Assert.IsTrue( savedMembershipType.Id > 0 );
        }

        [TestMethod]
        public async Task TestMembershipTypeER_DeleteObjectFromDatabase()
        {
            int beforeCount = MockDb.MembershipTypes.Count();
            
            await MembershipTypeER.DeleteMembershipType(1);
            
            Assert.AreNotEqual(beforeCount,MockDb.MembershipTypes.Count());
        }
        
        // test invalid state 
        [TestMethod]
        public async Task TestMembershipTypeER_DescriptionRequired()
        {
            var membershipType = await MembershipTypeER.NewMembershipType();
            membershipType.Description = "make valid";
            membershipType.Level = 2;
            membershipType.LastUpdatedBy = "edm";
            membershipType.LastUpdatedDate = DateTime.Now;
            var isObjectValidInit = membershipType.IsValid;
            membershipType.Description = string.Empty;

            Assert.IsNotNull(membershipType);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(membershipType.IsValid);
        }
       
        [TestMethod]
        public async Task TestMembershipTypeER_DescriptionExceedsMaxLengthOf255()
        {
            var membershipType = await MembershipTypeER.NewMembershipType();
            membershipType.Level = 1;
            membershipType.LastUpdatedBy = "edm";
            membershipType.LastUpdatedDate = DateTime.Now;
            membershipType.Description = "valid length";
            Assert.IsTrue(membershipType.IsValid);
            
            membershipType.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.IsNotNull(membershipType);
            Assert.IsFalse(membershipType.IsValid);
            Assert.AreEqual(membershipType.BrokenRulesCollection[0].Description,
                "The field Description must be a string or array type with a maximum length of '50'.");
        }        
        // test exception if attempt to save in invalid state

        [TestMethod]
        public async Task TestMembershipTypeER_TestInvalidSave()
        {
            var membershipType = await MembershipTypeER.NewMembershipType();
            membershipType.Description = String.Empty;
            bool exceptionThrown = false;
            try
            {
                var savedMembershipType = await membershipType.SaveAsync();
            }
            catch (ValidationException)
            {
                exceptionThrown = true;
            }
            
            Assert.IsFalse(membershipType.IsValid);
            Assert.IsTrue(exceptionThrown);
        }
    }
}

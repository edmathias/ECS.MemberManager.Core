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
    public class PrivacyLevelER_Tests 
    {

        [TestMethod]
        public async Task TestPrivacyLevelER_Get()
        {
            var privacyLevel = await PrivacyLevelER.GetPrivacyLevel(1);

            Assert.AreEqual(privacyLevel.Id, 1);
            Assert.IsTrue(privacyLevel.IsValid);
        }

        [TestMethod]
        public async Task TestPrivacyLevelER_GetNewObject()
        {
            var privacyLevel = await PrivacyLevelER.NewPrivacyLevel();

            Assert.IsNotNull(privacyLevel);
            Assert.IsFalse(privacyLevel.IsValid);
        }

        [TestMethod]
        public async Task TestPrivacyLevelER_UpdateExistingObjectInDatabase()
        {
            var privacyLevel = await PrivacyLevelER.GetPrivacyLevel(1);
            privacyLevel.Notes = "These are updated Notes";
            
            var result = privacyLevel.Save();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Notes, "These are updated Notes");
        }

        [TestMethod]
        public async Task TestPrivacyLevelER_InsertNewObjectIntoDatabase()
        {
            var privacyLevel = await PrivacyLevelER.NewPrivacyLevel();
            privacyLevel.Description = "Standby";
            privacyLevel.Notes = "This person is on standby";

            var savedPrivacyLevel = privacyLevel.Save();
           
            Assert.IsNotNull(savedPrivacyLevel);
            Assert.IsInstanceOfType(savedPrivacyLevel, typeof(PrivacyLevelER));
            Assert.IsTrue( savedPrivacyLevel.Id > 0 );
        }

        [TestMethod]
        public async Task TestPrivacyLevelER_DeleteObjectFromDatabase()
        {
            int beforeCount = MockDb.PrivacyLevels.Count();

            await PrivacyLevelER.DeletePrivacyLevel(1);
            
            Assert.AreNotEqual(beforeCount,MockDb.PrivacyLevels.Count());
        }
        
        // test invalid state 
        [TestMethod]
        public async Task TestPrivacyLevelER_DescriptionRequired() 
        {
            var privacyLevel = await PrivacyLevelER.NewPrivacyLevel();
            privacyLevel.Description = "make valid";
            var isObjectValidInit = privacyLevel.IsValid;
            privacyLevel.Description = string.Empty;

            Assert.IsNotNull(privacyLevel);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(privacyLevel.IsValid);
        }
       
        [TestMethod]
        public async Task TestPrivacyLevelER_DescriptionExceedsMaxLengthOf255()
        {
            var privacyLevel = await PrivacyLevelER.NewPrivacyLevel();
            privacyLevel.Description = "valid length";
            Assert.IsTrue(privacyLevel.IsValid);
            
            privacyLevel.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.IsNotNull(privacyLevel);
            Assert.IsFalse(privacyLevel.IsValid);
            Assert.AreEqual(privacyLevel.BrokenRulesCollection[0].Description,
                "The field Description must be a string or array type with a maximum length of '50'.");
 
        }        
        // test exception if attempt to save in invalid state

        [TestMethod]
        public async Task TestPrivacyLevelER_TestInvalidSave()
        {
            var privacyLevel = await PrivacyLevelER.NewPrivacyLevel();
            privacyLevel.Description = String.Empty;
            PrivacyLevelER savedPrivacyLevel = null;
                
            Assert.IsFalse(privacyLevel.IsValid);
            Assert.ThrowsException<ValidationException>(() => savedPrivacyLevel =  privacyLevel.Save() );
        }
    }
}

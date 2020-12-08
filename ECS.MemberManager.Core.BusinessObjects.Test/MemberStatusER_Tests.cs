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
    public class MemberStatusER_Tests 
    {

        [TestMethod]
        public async Task TestMemberStatusER_Get()
        {
            var memberStatus = await MemberStatusER.GetMemberStatus(1);

            Assert.AreEqual(memberStatus.Id, 1);
            Assert.IsTrue(memberStatus.IsValid);
        }

        [TestMethod]
        public async Task TestMemberStatusER_New()
        {
            var memberStatus = await MemberStatusER.NewMemberStatus();

            Assert.IsNotNull(memberStatus);
            Assert.IsFalse(memberStatus.IsValid);
        }

        [TestMethod]
        public async Task TestMemberStatusER_Update()
        {
            var memberStatus = await MemberStatusER.GetMemberStatus(1);
            memberStatus.Notes = "These are updated Notes";
            
            var result = await memberStatus.SaveAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Notes, "These are updated Notes");
        }

        [TestMethod]
        public async Task TestMemberStatusER_Insert()
        {
            var memberStatus = await MemberStatusER.NewMemberStatus();
            memberStatus.Description = "Standby";
            memberStatus.Notes = "This person is on standby";

            var savedMemberStatus = await memberStatus.SaveAsync();
           
            Assert.IsNotNull(savedMemberStatus);
            Assert.IsInstanceOfType(savedMemberStatus, typeof(MemberStatusER));
            Assert.IsTrue( savedMemberStatus.Id > 0 );
        }

        [TestMethod]
        public async Task TestMemberStatusER_Delete()
        {
            int beforeCount = MockDb.MemberStatuses.Count();
            
            await MemberStatusER.DeleteMemberStatus(1);
            
            Assert.AreNotEqual(beforeCount,MockDb.MemberStatuses.Count());
        }
        
        // test invalid state 
        [TestMethod]
        public async Task TestMemberStatusER_DescriptionRequired()
        {
            var memberStatus = await MemberStatusER.NewMemberStatus();
            memberStatus.Description = "make valid";
            var isObjectValidInit = memberStatus.IsValid;
            memberStatus.Description = string.Empty;

            Assert.IsNotNull(memberStatus);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(memberStatus.IsValid);
 
        }
       
        [TestMethod]
        public async Task TestMemberStatusER_DescriptionExceedsMaxLengthOf50()
        {
            var memberStatus = await MemberStatusER.NewMemberStatus();
            memberStatus.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.IsNotNull(memberStatus);
            Assert.IsFalse(memberStatus.IsValid);
            Assert.AreEqual(memberStatus.BrokenRulesCollection[0].Description,
                "The field Description must be a string or array type with a maximum length of '50'.");
 
        }        
        // test exception if attempt to save in invalid state

        [TestMethod]
        public async Task TestMemberStatusER_TestInvalidSave()
        {
            var memberStatus = await MemberStatusER.NewMemberStatus();
            memberStatus.Description = String.Empty;
            bool exceptionThrown = false;
            try
            {
                var savedMemberStatus = await memberStatus.SaveAsync();
            }
            catch (ValidationException )
            {
                exceptionThrown = true;
            }
            
            Assert.IsFalse(memberStatus.IsValid);
            Assert.IsTrue(exceptionThrown);
        }
    }
}

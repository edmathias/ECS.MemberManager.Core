using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
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
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMemberStatusER_Get()
        {
            var memberStatus = MemberStatusER.GetMemberStatusER(1);

            Assert.AreEqual(memberStatus.Id, 1);
            Assert.IsTrue(memberStatus.IsValid);
        }

        [TestMethod]
        public void TestMemberStatusER_New()
        {
            var memberStatus = MemberStatusER.NewMemberStatusER();

            Assert.IsNotNull(memberStatus);
            Assert.IsFalse(memberStatus.IsValid);
        }

        [TestMethod]
        public void TestMemberStatusER_Update()
        {
            var memberStatus = MemberStatusER.GetMemberStatusER(1);
            memberStatus.Notes = "These are updated Notes";
            
            var result = memberStatus.Save();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Notes, "These are updated Notes");
        }

        [TestMethod]
        public void TestMemberStatusER_Insert()
        {
            var memberStatus = MemberStatusER.NewMemberStatusER();
            memberStatus.Description = "Standby";
            memberStatus.Notes = "This person is on standby";

            var savedMemberStatus = memberStatus.Save();
           
            Assert.IsNotNull(savedMemberStatus);
            Assert.IsInstanceOfType(savedMemberStatus, typeof(MemberStatusER));
            Assert.IsTrue( savedMemberStatus.Id > 0 );
        }

        [TestMethod]
        public void TestMemberStatusER_Delete()
        {
            int beforeCount = MockDb.MemberStatuses.Count();
            
            MemberStatusER.DeleteMemberStatusER(1);
            
            Assert.AreNotEqual(beforeCount,MockDb.MemberStatuses.Count());
        }
        
        // test invalid state 
        [TestMethod]
        public void TestMemberStatusER_DescriptionRequired()
        {
            var memberStatus = MemberStatusER.NewMemberStatusER();
            memberStatus.Description = "make valid";
            var isObjectValidInit = memberStatus.IsValid;
            memberStatus.Description = string.Empty;

            Assert.IsNotNull(memberStatus);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(memberStatus.IsValid);
 
        }
       
        [TestMethod]
        public void TestMemberStatusER_DescriptionExceedsMaxLengthOf255()
        {
            var memberStatus = MemberStatusER.NewMemberStatusER();
            memberStatus.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.IsNotNull(memberStatus);
            Assert.IsFalse(memberStatus.IsValid);
            Assert.AreEqual(memberStatus.BrokenRulesCollection[0].Description,
                "The field Description must be a string or array type with a maximum length of '255'.");
 
        }        
        // test exception if attempt to save in invalid state

        [TestMethod]
        public void TestMemberStatusER_TestInvalidSave()
        {
            var memberStatus = MemberStatusER.NewMemberStatusER();
            memberStatus.Description = String.Empty;
            MemberStatusER savedMemberStatus = null;
                
            
            Assert.IsFalse(memberStatus.IsValid);
            Assert.ThrowsException<ValidationException>(() => savedMemberStatus =  memberStatus.Save() );
        }
    }
}

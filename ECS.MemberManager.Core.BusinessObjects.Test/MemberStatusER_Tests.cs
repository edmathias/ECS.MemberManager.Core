using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECS.MemberManager.Core.BusinessObjects.MemberStatus;
using Telerik.JustMock;

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
            memberStatus.Description = string.Empty;
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
    }
}

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
    public class EMailTypeER_Tests 
    {
        [TestMethod]
        public void TestEMailTypeER_Get()
        {
            var eMailType = EMailTypeER.GetEMailTypeER(1);

            Assert.AreEqual(eMailType.Id, 1);
            Assert.IsTrue(eMailType.IsValid);
        }

        [TestMethod]
        public void TestEMailTypeER_New()
        {
            var eMailType = EMailTypeER.NewEMailTypeER();

            Assert.IsNotNull(eMailType);
            Assert.IsFalse(eMailType.IsValid);
        }

        [TestMethod]
        public void TestEMailTypeER_Update()
        {
            var eMailType = EMailTypeER.GetEMailTypeER(1);
            eMailType.Notes = "These are updated Notes";
            
            var result = eMailType.Save();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Notes, "These are updated Notes");
        }

        [TestMethod]
        public void TestEMailTypeER_Insert()
        {
            var eMailType = EMailTypeER.NewEMailTypeER();
            eMailType.Description = "Standby";
            eMailType.Notes = "This person is on standby";

            var savedEMailType = eMailType.Save();
           
            Assert.IsNotNull(savedEMailType);
            Assert.IsInstanceOfType(savedEMailType, typeof(EMailTypeER));
            Assert.IsTrue( savedEMailType.Id > 0 );
        }

        [TestMethod]
        public void TestEMailTypeER_Delete()
        {
            int beforeCount = MockDb.EMailTypes.Count();
            
            EMailTypeER.DeleteEMailTypeER(1);
            
            Assert.AreNotEqual(beforeCount,MockDb.EMailTypes.Count());
        }
        
        // test invalid state 
        [TestMethod]
        public void TestEMailTypeER_DescriptionRequired()
        {
            var eMailType = EMailTypeER.NewEMailTypeER();
            eMailType.Description = "make valid";
            var isObjectValidInit = eMailType.IsValid;
            eMailType.Description = string.Empty;

            Assert.IsNotNull(eMailType);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(eMailType.IsValid);
 
        }
       
        [TestMethod]
        public void TestEMailTypeER_DescriptionExceedsMaxLengthOf255()
        {
            var eMailType = EMailTypeER.NewEMailTypeER();
            eMailType.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.IsNotNull(eMailType);
            Assert.IsFalse(eMailType.IsValid);
            Assert.AreEqual(eMailType.BrokenRulesCollection[0].Description,
                "The field Description must be a string or array type with a maximum length of '255'.");
 
        }        
        // test exception if attempt to save in invalid state

        [TestMethod]
        public void TestEMailTypeER_TestInvalidSave()
        {
            var eMailType = EMailTypeER.NewEMailTypeER();
            eMailType.Description = String.Empty;
            EMailTypeER savedEMailType = null;
                
            
            Assert.IsFalse(eMailType.IsValid);
            Assert.ThrowsException<ValidationException>(() => savedEMailType =  eMailType.Save() );
        }
    }
}

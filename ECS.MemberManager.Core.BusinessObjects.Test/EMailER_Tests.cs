using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects.Test
{
    [TestClass]
    public class EMailER_Tests 
    {
        [TestMethod]
        public async Task TestEMailER_Get()
        {
            var eMail = await EMailER.GetEMail(1);

            Assert.AreEqual(eMail.Id, 1);
            Assert.IsTrue(eMail.IsValid);
        }

        [TestMethod]
        public async Task TestEMailER_New()
        {
            var eMail = await EMailER.NewEMail();

            Assert.IsNotNull(eMail);
            Assert.IsFalse(eMail.IsValid);
        }

        [TestMethod]
        public async Task TestEMailER_Update()
        {
            var eMail = await EMailER.GetEMail(1);
            eMail.Notes = "These are updated Notes";
            
            var result = eMail.Save();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Notes, "These are updated Notes");
        }

        [TestMethod]
        public async Task TestEMailER_Insert()
        {
            var eMail = await EMailER.NewEMail();
            eMail.EMailAddress = "billg@microsoft.com";
            eMail.LastUpdatedBy = "edm";
            eMail.LastUpdatedDate = DateTime.Now;
            eMail.Notes = "This person is on standby";
            var eMailTypeDto = new EMailType()
            {
                Id = 4,
                Description = "secondary email",
                Notes = "nothing to see here"
            };
            eMail.EMailType = await DataPortal.FetchChildAsync<EMailTypeROC>(eMailTypeDto);
            var savedEMail = eMail.Save();
           
            Assert.IsNotNull(savedEMail);
            Assert.IsInstanceOfType(savedEMail, typeof(EMailER));
            Assert.IsTrue( savedEMail.Id > 0 );
        }

        [TestMethod]
        public async Task TestEMailER_Delete()
        {
            int beforeCount = MockDb.EMails.Count();
            
            await EMailER.DeleteEMail(1);
            
            Assert.AreNotEqual(beforeCount,MockDb.EMails.Count());
        }
        
        // test invalid state 
        [TestMethod]
        public async Task TestEMailER_EMailAddressRequired()
        {
            var eMail = await EMailER.NewEMail();
            eMail.EMailAddress = "make valid";
            eMail.EMailType = await DataPortal.CreateChildAsync<EMailTypeROC>();
            eMail.LastUpdatedDate = DateTime.Now;
            eMail.LastUpdatedBy = "edm";
            var isObjectValidInit = eMail.IsValid;
            eMail.EMailAddress = string.Empty;

            Assert.IsNotNull(eMail);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(eMail.IsValid);
 
        }
    
        // test invalid state 
        [TestMethod]
        public async Task TestEMailER_EMailTypeRequired()
        {
            var eMail = await EMailER.NewEMail();
            eMail.EMailAddress = "make valid";
            eMail.LastUpdatedDate = DateTime.Now;
            eMail.LastUpdatedBy = "edm";
            eMail.EMailType = await DataPortal.CreateChildAsync<EMailTypeROC>();
            var isObjectValidInit = eMail.IsValid;
            eMail.EMailType = null;

            Assert.IsNotNull(eMail);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(eMail.IsValid);
            Assert.AreEqual("The EMailType field is required.",eMail.GetBrokenRules()[0].Description);
 
        }
        
        // test exception if attempt to save in invalid state

        [TestMethod]
        public async Task TestEMailER_TestInvalidSave()
        {
            var eMail = await EMailER.NewEMail();
            eMail.EMailAddress = String.Empty;
            EMailER savedEMail = null;
            
            Assert.IsFalse(eMail.IsValid);
            Assert.ThrowsException<ValidationException>(() => savedEMail =  eMail.Save() );
        }
    }
}

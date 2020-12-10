using System;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ECS.MemberManager.Core.BusinessObjects.Test
{
    [TestClass]
    public class PhoneER_Tests
    {
        [TestMethod]
        public async Task TestPhoneER_Get()
        {
            var phone = await PhoneER.GetPhone(1);

            Assert.AreEqual(phone.Id, 1);
            Assert.IsTrue(phone.IsValid);
        }

        [TestMethod]
        public async Task TestPhoneER_New()
        {
            var phone = await PhoneER.NewPhone();

            Assert.IsNotNull(phone);
            Assert.IsFalse(phone.IsValid);
        }

        [TestMethod]
        public async Task TestPhoneER_Update()
        {
            var phone = await PhoneER.GetPhone(1);
            phone.Notes = "These are updated Notes";
            
            var result = phone.Save();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Notes, "These are updated Notes");
        }

        [TestMethod]
        public async Task TestPhoneER_Insert()
        {
            var phone = await PhoneER.NewPhone();
            
            BuildValidPhone(phone);

            var savedPhone = await phone.SaveAsync();
           
            Assert.IsNotNull(savedPhone);
            Assert.IsInstanceOfType(savedPhone, typeof(PhoneER));
            Assert.IsTrue( savedPhone.Id > 0 );
        }

        [TestMethod]
        public async Task TestPhoneER_Delete()
        {
            int beforeCount = MockDb.Phones.Count();
            
            await PhoneER.DeletePhone(1);
            
            Assert.AreNotEqual(beforeCount,MockDb.Phones.Count());
        }
        
        // test invalid state 
        [TestMethod]
        public async Task TestPhoneER_PhoneAreaCodeRequired()
        {
            var phone = await PhoneER.NewPhone();
            BuildValidPhone(phone);
            var isObjectValidInit = phone.IsValid;
            phone.AreaCode = string.Empty;

            Assert.IsNotNull(phone);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(phone.IsValid);
        }

        [TestMethod]
        public async Task TestPhoneER_TestInvalidSave()
        {
            var phone = await PhoneER.NewPhone();
            PhoneER savedPhone;
            
            Assert.IsFalse(phone.IsValid);
            Assert.ThrowsException<ValidationException>(() => savedPhone =  phone.Save() );
        }   
        
        private static void BuildValidPhone(PhoneER phone)
        {
            phone.AreaCode = "303";
            phone.Extension = "111";
            phone.Number = "555-2368";
            phone.PhoneType = "mobile";
            phone.DisplayOrder = 1;
            phone.LastUpdatedBy = "edm";
            phone.LastUpdatedDate = DateTime.Now;
            phone.Notes = "This person is on standby";
        }
    }
}
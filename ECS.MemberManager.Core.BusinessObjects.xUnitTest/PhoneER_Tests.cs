using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PhoneER_Tests
    {
        [Fact]
        public async Task TestPhoneER_Get()
        {
            var phone = await PhoneER.GetPhone(1);

            Assert.Equal(1,phone.Id);
            Assert.True(phone.IsValid);
        }

        [Fact]
        public async Task TestPhoneER_New()
        {
            var phone = await PhoneER.NewPhone();

            Assert.NotNull(phone);
            Assert.False(phone.IsValid);
        }

        [Fact]
        public async Task TestPhoneER_Update()
        {
            var phone = await PhoneER.GetPhone(1);
            phone.Notes = "These are updated Notes";
            
            var result = phone.Save();

            Assert.NotNull(result);
            Assert.Equal( "These are updated Notes",result.Notes);
        }

        [Fact]
        public async Task TestPhoneER_Insert()
        {
            var phone = await PhoneER.NewPhone();
            
            BuildValidPhone(phone);

            var savedPhone = await phone.SaveAsync();
           
            Assert.NotNull(savedPhone);
            Assert.IsType<PhoneER>(savedPhone);
            Assert.True( savedPhone.Id > 0 );
        }

        [Fact]
        public async Task TestPhoneER_Delete()
        {
            int beforeCount = MockDb.Phones.Count();
            
            await PhoneER.DeletePhone(1);
            
            Assert.NotEqual(beforeCount,MockDb.Phones.Count());
        }
        
        // test invalid state 
        [Fact]
        public async Task TestPhoneER_PhoneAreaCodeRequired()
        {
            var phone = await PhoneER.NewPhone();
            BuildValidPhone(phone);
            var isObjectValidInit = phone.IsValid;
            phone.AreaCode = string.Empty;

            Assert.NotNull(phone);
            Assert.True(isObjectValidInit);
            Assert.False(phone.IsValid);
        }

        [Fact]
        public async Task TestPhoneER_TestInvalidSave()
        {
            var phone = await PhoneER.NewPhone();
            
            Assert.False(phone.IsValid);
            Assert.Throws<ValidationException>(() => phone.Save() );
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
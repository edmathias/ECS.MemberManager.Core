using System;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PhoneROC_Tests
    {
        [Fact]
        public async void PhoneROC_TestGetChild()
        {
            const int ID_VALUE = 999;

            var phoneToLoad = BuildPhone();
            phoneToLoad.Id = ID_VALUE;

            var phone = await PhoneROC.GetPhoneROC(phoneToLoad);

            Assert.NotNull(phone);
            Assert.IsType<PhoneROC>(phone);
            Assert.Equal(phoneToLoad.Id, phone.Id);
            Assert.Equal(phoneToLoad.PhoneType, phone.PhoneType);
            Assert.Equal(phoneToLoad.AreaCode, phone.AreaCode);
            Assert.Equal(phoneToLoad.Number, phone.Number);
            Assert.Equal(phoneToLoad.Extension, phone.Extension);
            Assert.Equal(phoneToLoad.DisplayOrder, phone.DisplayOrder);
            Assert.Equal(phoneToLoad.LastUpdatedBy, phone.LastUpdatedBy);
            Assert.Equal(new SmartDate(phoneToLoad.LastUpdatedDate), phone.LastUpdatedDate);
            Assert.Equal(phoneToLoad.Notes, phone.Notes);
            Assert.Equal(phoneToLoad.RowVersion, phone.RowVersion);
        }

        private Phone BuildPhone()
        {
            var phone = new Phone();
            phone.Id = 1;
            phone.PhoneType = "mobile";
            phone.AreaCode = "303";
            phone.Number = "555-2368";
            phone.Extension = "123";
            phone.DisplayOrder = 1;
            phone.LastUpdatedBy = "Hank";
            phone.LastUpdatedDate = DateTime.Now;
            phone.Notes = "notes for doctype";

            return phone;
        }
    }
}
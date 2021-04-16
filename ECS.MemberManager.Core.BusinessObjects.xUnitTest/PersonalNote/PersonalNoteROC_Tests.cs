using System;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PersonalNoteROC_Tests
    {
        [Fact]
        public async void PersonalNoteROC_TestGetChild()
        {
            const int ID_VALUE = 999;

            var phoneToLoad = BuildPersonalNote();
            phoneToLoad.Id = ID_VALUE;

            var phone = await PersonalNoteROC.GetPersonalNoteROC(phoneToLoad);

            Assert.NotNull(phone);
            Assert.IsType<PersonalNoteROC>(phone);
            Assert.Equal(phoneToLoad.Id, phone.Id);
            Assert.Equal(phoneToLoad.LastUpdatedBy, phone.LastUpdatedBy);
            Assert.Equal(new SmartDate(phoneToLoad.LastUpdatedDate), phone.LastUpdatedDate);
            Assert.Equal(phoneToLoad.RowVersion, phone.RowVersion);
        }

        private PersonalNote BuildPersonalNote()
        {
            var phone = new PersonalNote();
            phone.Id = 1;
            phone.LastUpdatedBy = "Hank";
            phone.LastUpdatedDate = DateTime.Now;

            return phone;
        }
    }
}
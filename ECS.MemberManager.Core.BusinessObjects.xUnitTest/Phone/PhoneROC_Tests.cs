using System;
using System.Linq;
using Csla;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PhoneROC_Tests : CslaBaseTest
    {
        [Fact]
        public async void PhoneROC_TestGetChild()
        {
            const int ID_VALUE = 99;

            var personalNoteToLoad = MockDb.Phones.FirstOrDefault(pn => pn.Id == 1);

            var personalNote = await PhoneROC.GetPhoneROC(personalNoteToLoad);

            Assert.NotNull(personalNote);
            Assert.IsType<PhoneROC>(personalNote);
            Assert.Equal(personalNoteToLoad.Id, personalNote.Id);
            Assert.Equal(personalNoteToLoad.LastUpdatedBy, personalNote.LastUpdatedBy);
            Assert.Equal(new SmartDate(personalNoteToLoad.LastUpdatedDate), personalNote.LastUpdatedDate);
            Assert.Equal(personalNoteToLoad.RowVersion, personalNote.RowVersion);
        }

    }
}
using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PersonalNoteRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void PersonalNoteRORL_TestGetPersonalNoteRORL()
        {
            var phoneTypeInfoList = await PersonalNoteRORL.GetPersonalNoteRORL();

            Assert.NotNull(phoneTypeInfoList);
            Assert.True(phoneTypeInfoList.IsReadOnly);
            Assert.Equal(3, phoneTypeInfoList.Count);
        }
    }
}
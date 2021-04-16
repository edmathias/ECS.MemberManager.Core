using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PhoneRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void PhoneRORL_TestGetPhoneRORL()
        {
            var personalNoteTypeInfoList = await PhoneRORL.GetPhoneRORL();

            Assert.NotNull(personalNoteTypeInfoList);
            Assert.True(personalNoteTypeInfoList.IsReadOnly);
            Assert.Equal(3, personalNoteTypeInfoList.Count);
        }
    }
}
using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PrivacyLevelRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void PrivacyLevelRORL_TestGetPrivacyLevelRORL()
        {
            var privacyLevelTypeInfoList = await PrivacyLevelRORL.GetPrivacyLevelRORL();

            Assert.NotNull(privacyLevelTypeInfoList);
            Assert.True(privacyLevelTypeInfoList.IsReadOnly);
            Assert.Equal(3, privacyLevelTypeInfoList.Count);
        }
    }
}
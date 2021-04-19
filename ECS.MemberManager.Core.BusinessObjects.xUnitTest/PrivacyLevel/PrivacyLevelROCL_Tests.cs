using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PrivacyLevelROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void PrivacyLevelInfoList_TestGetPrivacyLevelInfoList()
        {
            var childData = MockDb.PrivacyLevels;

            var privacyLevelInfoList = await PrivacyLevelROCL.GetPrivacyLevelROCL(childData);

            Assert.NotNull(privacyLevelInfoList);
            Assert.True(privacyLevelInfoList.IsReadOnly);
            Assert.Equal(3, privacyLevelInfoList.Count);
        }
    }
}
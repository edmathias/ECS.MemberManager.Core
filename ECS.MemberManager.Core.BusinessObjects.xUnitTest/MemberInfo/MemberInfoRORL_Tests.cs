using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MemberInfoRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void MemberInfoRORL_TestGetMemberInfoRORL()
        {
            var eMailTypeInfoList = await MemberInfoRORL.GetMemberInfoRORL();

            Assert.NotNull(eMailTypeInfoList);
            Assert.True(eMailTypeInfoList.IsReadOnly);
            Assert.Equal(3, eMailTypeInfoList.Count);
        }
    }
}
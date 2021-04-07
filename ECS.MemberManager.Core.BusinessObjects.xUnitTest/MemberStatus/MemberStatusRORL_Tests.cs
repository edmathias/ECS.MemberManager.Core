using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MemberStatusRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void MemberStatusRORL_TestGetMemberStatusRORL()
        {
            var memberStatusTypeInfoList = await MemberStatusRORL.GetMemberStatusRORL();

            Assert.NotNull(memberStatusTypeInfoList);
            Assert.True(memberStatusTypeInfoList.IsReadOnly);
            Assert.Equal(3, memberStatusTypeInfoList.Count);
        }
    }
}
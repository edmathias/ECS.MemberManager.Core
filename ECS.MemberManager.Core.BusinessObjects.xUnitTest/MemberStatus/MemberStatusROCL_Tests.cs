using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MemberStatusROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void MemberStatusInfoList_TestGetMemberStatusInfoList()
        {
            var childData = MockDb.MemberStatuses;
            var memberStatusInfoList = await MemberStatusROCL.GetMemberStatusROCL(childData);

            Assert.NotNull(memberStatusInfoList);
            Assert.True(memberStatusInfoList.IsReadOnly);
            Assert.Equal(3, memberStatusInfoList.Count);
        }
    }
}
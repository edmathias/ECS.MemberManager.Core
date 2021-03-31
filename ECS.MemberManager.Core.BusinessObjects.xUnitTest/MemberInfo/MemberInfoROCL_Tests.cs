using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MemberInfoROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void MemberInfoInfoList_TestGetMemberInfoInfoList()
        {
            var childData = MockDb.MemberInfo ;

            var memberInfoTypeInfoList = await MemberInfoROCL.GetMemberInfoROCL(childData);

            Assert.NotNull(memberInfoTypeInfoList);
            Assert.True(memberInfoTypeInfoList.IsReadOnly);
            Assert.Equal(3, memberInfoTypeInfoList.Count);
        }
    }
}
using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MembershipTypeROCL_Tests
    {
        [Fact]
        private async void MembershipTypeInfoList_TestGetMembershipTypeInfoList()
        {
            var childData = MockDb.MembershipTypes;

            var membershipTypeInfoList = await MembershipTypeROCL.GetMembershipTypeROCL(childData);

            Assert.NotNull(membershipTypeInfoList);
            Assert.True(membershipTypeInfoList.IsReadOnly);
            Assert.Equal(3, membershipTypeInfoList.Count);
        }
    }
}
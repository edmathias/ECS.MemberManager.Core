using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MembershipTypeRORL_Tests
    {
        [Fact]
        private async void MembershipTypeRORL_TestGetMembershipTypeRORL()
        {
            var membershipTypeInfoList = await MembershipTypeRORL.GetMembershipTypeRORL();

            Assert.NotNull(membershipTypeInfoList);
            Assert.True(membershipTypeInfoList.IsReadOnly);
            Assert.Equal(3, membershipTypeInfoList.Count);
        }
    }
}
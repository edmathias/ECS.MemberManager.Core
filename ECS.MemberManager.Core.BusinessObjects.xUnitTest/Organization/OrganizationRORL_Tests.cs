using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OrganizationRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void OrganizationRORL_TestGetOrganizationRORL()
        {
            var data = MockDb.Organizations;

            var organizationList = await OrganizationRORL.GetOrganizationRORL();

            Assert.NotNull(organizationList);
            Assert.True(organizationList.IsReadOnly);
            Assert.Equal(3, organizationList.Count);
        }
    }
}
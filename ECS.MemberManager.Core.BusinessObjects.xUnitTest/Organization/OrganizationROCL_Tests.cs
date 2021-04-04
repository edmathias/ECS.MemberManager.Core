using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OrganizationROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void OrganizationROCL_TestGetOrganizationROCL()
        {
            var data = MockDb.Organizations;

            var organizationList = await OrganizationROCL.GetOrganizationROCL(data);

            Assert.NotNull(organizationList);
            Assert.True(organizationList.IsReadOnly);
            Assert.Equal(3, organizationList.Count);
        }
    }
}
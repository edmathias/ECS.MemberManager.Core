using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OrganizationROR_Tests : CslaBaseTest
    {
        [Fact]
        public async Task OrganizationROR_Get()
        {
            var organization = await OrganizationROR.GetOrganizationROR(1);

            Assert.NotNull(organization);
            Assert.IsType<OrganizationROR>(organization);
            Assert.Equal(1, organization.Id);
            Assert.True(organization.IsValid);
        }
    }
}
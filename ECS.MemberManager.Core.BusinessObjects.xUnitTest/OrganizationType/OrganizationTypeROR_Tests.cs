using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OrganizationTypeROR_Tests : CslaBaseTest
    {
        [Fact]
        public async void OrganizationTypeROR_Get()
        {
            var organizationType = await OrganizationTypeROR.GetOrganizationTypeROR(1);

            Assert.NotNull(organizationType);
            Assert.Equal(1, organizationType.Id);
        }
    }
}
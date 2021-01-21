using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OrganizationInfo_Tests
    {
        [Fact]
        public async void OrganizationInfo_TestGetById()
        {
            var organizationInfo = await OrganizationInfo.GetOrganizationInfo(1);
            
            Assert.NotNull(organizationInfo);
            Assert.IsType<OrganizationInfo>(organizationInfo);
            Assert.Equal(1, organizationInfo.Id);
        }

        [Fact]
        public async void OrganizationInfo_TestGetChild()
        {
            const int ID_VALUE = 999;
            
            var organization = new Organization()
            {
                Id = ID_VALUE,
            };

            var organizationInfo = await OrganizationInfo.GetOrganizationInfo(organization);
            
            Assert.NotNull(organizationInfo);
            Assert.IsType<OrganizationInfo>(organizationInfo);
            Assert.Equal(ID_VALUE, organizationInfo.Id);

        }
    }
}
using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OrganizationTypeInfo_Tests
    {
        [Fact]
        public async void OrganizationTypeInfo_TestGetById()
        {
            var organizationTypeInfo = await OrganizationTypeInfo.GetOrganizationTypeInfo(1);
            
            Assert.NotNull(organizationTypeInfo);
            Assert.IsType<OrganizationTypeInfo>(organizationTypeInfo);
            Assert.Equal(1, organizationTypeInfo.Id);
        }

        [Fact]
        public async void OrganizationTypeInfo_TestGetChild()
        {
            const int ID_VALUE = 999;
            
            var organizationType = new OrganizationType()
            {
                Id = ID_VALUE,
                Name = "organization type description",
                Notes = "organization type notes",
                CategoryOfOrganizationId = 1
            };

            var organizationTypeInfo = await OrganizationTypeInfo.GetOrganizationTypeInfo(organizationType);
            
            Assert.NotNull(organizationTypeInfo);
            Assert.IsType<OrganizationTypeInfo>(organizationTypeInfo);
            Assert.Equal(ID_VALUE, organizationTypeInfo.Id);
            Assert.Equal("organization type description",organizationTypeInfo.Name);
            Assert.Equal("organization type notes",organizationTypeInfo.Notes);
            Assert.NotNull(organizationTypeInfo.CategoryOfOrganization);

        }
    }
}
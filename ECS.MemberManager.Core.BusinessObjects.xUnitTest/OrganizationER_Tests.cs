using System;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OrganizationER_Tests
    {
        [Fact]
        public async Task TestOrganizationER_Get()
        {
            var organization = await OrganizationER.GetOrganization(1);

            Assert.Equal(1,organization.Id);
            Assert.True(organization.IsValid);
        }
        
        [Fact]
        public async Task TestOrganizationER_New()
        {
            var organizationType = await OrganizationER.NewOrganization();

            Assert.NotNull(organizationType);
            Assert.False(organizationType.IsValid);
        }

        [Fact]
        public async Task TestOrganizationER_Update()
        {
            var organization = await OrganizationER.GetOrganization(1);
            organization.Notes = "These are updated Notes";
            
            var result = organization.Save();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes",result.Notes);
        }

        [Fact]
        public async Task TestOrganizationER_Insert()
        {
            var organization = await OrganizationER.NewOrganization();
            organization.Name = "Organization name";
            organization.Notes = "notes for org";

            var savedOrganization = organization.Save();
           
            Assert.NotNull(savedOrganization);
            Assert.IsType<OrganizationER>(savedOrganization);
            Assert.True( savedOrganization.Id > 0 );
        }

        [Fact]
        public async Task TestOrganizationER_Delete()
        {
            int beforeCount = MockDb.Organizations.Count();
            
            await OrganizationER.DeleteOrganization(99);
            
            Assert.NotEqual(beforeCount,MockDb.Organizations.Count());
        }
        
        // test invalid state 
        [Fact]
        public async Task TestOrganizationER_DescriptionRequired()
        {
            var organization = await OrganizationER.NewOrganization();
            organization.Name = "make valid";
            var isObjectValidInit = organization.IsValid;
            organization.Name = string.Empty;

            Assert.NotNull(organization);
            Assert.True(isObjectValidInit);
            Assert.False(organization.IsValid);
 
        }
       
        [Fact]
        public async Task TestOrganizationER_DescriptionExceedsMaxLengthOf50()
        {
            var organization = await OrganizationER.NewOrganization();
            organization.Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(organization);
            Assert.False(organization.IsValid);
            Assert.Equal("The field Name must be a string or array type with a maximum length of '50'.",
                organization.BrokenRulesCollection[0].Description);
 
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestOrganizationER_TestInvalidSave()
        {
            var organization = await OrganizationER.NewOrganization();
            OrganizationER savedOrganization = null;
            
            Assert.False(organization.IsValid);
            Assert.Throws<ValidationException>(() => savedOrganization =  organization.Save() );
        }

    }
}
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OrganizationEC_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public OrganizationEC_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");

            if (testLibrary == "Mock")
                MockDb.ResetMockDb();
            else
            {
                if (!IsDatabaseBuilt)
                {
                    var adoDb = new ADODb();
                    adoDb.BuildMemberManagerADODb();
                    IsDatabaseBuilt = true;
                }
            }
        }

        [Fact]
        public async Task OrganizationEC_Get()
        {
            
            var organization = await OrganizationEC.GetOrganizationEC(BuildValidOrganization());

            Assert.NotNull(organization);
            Assert.IsType<OrganizationEC>(organization);
            Assert.Equal(1, organization.Id);
            Assert.True(organization.IsValid);
        }

        [Fact]
        public async Task OrganizationEC_New()
        {
            var organization = await OrganizationEC.NewOrganizationEC();

            Assert.NotNull(organization);
            Assert.False(organization.IsValid);
        }

        [Fact]
        public async Task OrganizationEC_NameRequired()
        {
            var organization = await OrganizationEC.GetOrganizationEC(BuildValidOrganization());
            
            var isObjectValidInit = organization.IsValid;
            organization.Name = string.Empty;

            Assert.NotNull(organization);
            Assert.True(isObjectValidInit);
            Assert.False(organization.IsValid);
            Assert.Equal("Name", organization.BrokenRulesCollection[0].Property);
            Assert.Equal("Name required", organization.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task OrganizationEC_NameCanNotExceed50Characters()
        {
            var organization = await OrganizationEC.GetOrganizationEC(BuildValidOrganization());
            var isObjectValidInit = organization.IsValid;
            
            organization.Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(organization);
            Assert.False(organization.IsValid);
            Assert.Equal("Name", organization.BrokenRulesCollection[0].Property);
            Assert.Equal("Name can not exceed 50 characters", organization.BrokenRulesCollection[0].Description);
        }
        
        [Fact]
        public async Task OrganizationEC_LastUpdatedByRequired()
        {
            var organization = await OrganizationEC.GetOrganizationEC(BuildValidOrganization());
            
            var isObjectValidInit = organization.IsValid;
            organization.LastUpdatedBy = String.Empty;

            Assert.NotNull(organization);
            Assert.True(isObjectValidInit);
            Assert.False(organization.IsValid);
            Assert.Equal("LastUpdatedBy", organization.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy required", organization.BrokenRulesCollection[0].Description);
        }
       
        [Fact]
        public async Task OrganizationEC_LastUpdatedByCanNotExceed255Characters()
        {
            var organization = await OrganizationEC.GetOrganizationEC(BuildValidOrganization());
            var isObjectValidInit = organization.IsValid;
            
            organization.LastUpdatedBy = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(organization);
            Assert.False(organization.IsValid);
            Assert.Equal("LastUpdatedBy", organization.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy can not exceed 255 characters", organization.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task OrganizationEC_LastUpdatedDateRequired()
        {
            var organization = await OrganizationEC.GetOrganizationEC(BuildValidOrganization());
            
            var isObjectValidInit = organization.IsValid;
            organization.LastUpdatedDate = DateTime.MinValue;

            Assert.NotNull(organization);
            Assert.True(isObjectValidInit);
            Assert.False(organization.IsValid);
            Assert.Equal("LastUpdatedDate", organization.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedDate required", organization.BrokenRulesCollection[0].Description);
        }
        
        [Fact]
        public async Task OrganizationEC_OrganizationTypeRequired()
        {
            var organization = await OrganizationEC.GetOrganizationEC(BuildValidOrganization());
            
            var isObjectValidInit = organization.IsValid;
            organization.OrganizationType = null;

            Assert.NotNull(organization);
            Assert.True(isObjectValidInit);
            Assert.False(organization.IsValid);
            Assert.Equal("OrganizationType", organization.BrokenRulesCollection[0].Property);
            Assert.Equal("OrganizationType required", organization.BrokenRulesCollection[0].Description);
        }


        
        private Organization BuildValidOrganization()
        {
            var organization = new Organization()
            {
                Id = 1,
                Name = "organization name",
                OrganizationType = new OrganizationType() {Id = 1},
                Notes = "notes for org",
                LastUpdatedBy = "edm",
                LastUpdatedDate = DateTime.Now,
                DateOfFirstContact = DateTime.Now
            };

            return organization;
        }
    }
}
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
    public class OrganizationER_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public OrganizationER_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");
            
            if(testLibrary == "Mock")
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
        public async Task OrganizationER_Get()
        {
            var organization = await OrganizationER.GetOrganizationER(1);

            Assert.NotNull(organization);
            Assert.IsType<OrganizationER>(organization);
            Assert.Equal(1, organization.Id);
            Assert.True(organization.IsValid);
        }

        [Fact]
        public async Task OrganizationER_New()
        {
            var organization = await OrganizationER.NewOrganizationER();

            Assert.NotNull(organization);
            Assert.False(organization.IsValid);
        }

        [Fact]
        public async void OrganizationER_Update()
        {
            var organization = await OrganizationER.GetOrganizationER(1);
            var notesUpdate = $"These are updated description.";
            organization.Notes = notesUpdate;

            var result = await organization.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal(notesUpdate, result.Notes);
        }

        [Fact]
        public async void OrganizationER_Insert()
        {
            var organization = await OrganizationER.NewOrganizationER();
            await BuildValidOrganization(organization);

            var savedOrganization = await organization.SaveAsync();

            Assert.NotNull(savedOrganization);
            Assert.IsType<OrganizationER>(savedOrganization);
            Assert.True(savedOrganization.Id > 0);
            Assert.NotNull(savedOrganization.RowVersion);
        }

        [Fact]
        public async Task OrganizationER_Delete()
        {
            await OrganizationER.DeleteOrganizationER(99);

            var organizationToCheck = await Assert.ThrowsAsync<Csla.DataPortalException>
                (() => OrganizationER.GetOrganizationER(99));
        }

        [Fact]
        public async Task OrganizationER_NameRequired()
        {
            var organization = await OrganizationER.NewOrganizationER();
            await BuildValidOrganization(organization);
            var isObjectValidInit = organization.IsValid;
            organization.Name = string.Empty;

            Assert.NotNull(organization);
            Assert.True(isObjectValidInit);
            Assert.False(organization.IsValid);
            Assert.Equal("Name",organization.BrokenRulesCollection[0].Property);
            Assert.Equal("Name required",organization.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task OrganizationER_NameExceedsMaxLengthOf50()
        {
            var organization = await OrganizationER.NewOrganizationER();
            await BuildValidOrganization(organization);
            organization.Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                    "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                    "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                    "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(organization);
            Assert.False(organization.IsValid);
            Assert.Equal("Name",organization.BrokenRulesCollection[0].Property);
            Assert.Equal("Name can not exceed 50 characters",organization.BrokenRulesCollection[0].Description);
        }
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task OrganizationER_TestInvalidSave()
        {
            var organization = await OrganizationER.NewOrganizationER();
            organization.Name = String.Empty;

            Assert.False(organization.IsValid);
            await Assert.ThrowsAsync<ValidationException>(() => organization.SaveAsync());
        }
    
        [Fact]
        public async Task OrganizationER_TestSaveOutOfOrder()
        {
            var organization1 = await OrganizationER.GetOrganizationER(1);
            var organization2 = await OrganizationER.GetOrganizationER(1);
            organization1.Notes = "set up timestamp issue";  // turn on IsDirty
            organization2.Notes = "set up timestamp issue";

            var organization2_2 = await organization2.SaveAsync();
            
            Assert.NotEqual(organization2_2.RowVersion, organization1.RowVersion);
            Assert.Equal("set up timestamp issue",organization2_2.Notes);
            await Assert.ThrowsAsync<DataPortalException>(() => organization1.SaveAsync());
        }

        [Fact]
        public async Task OrganizationER_TestSubsequentSaves()
        {
            var organization = await OrganizationER.GetOrganizationER(1);
            organization.Notes = "set up timestamp issue";  // turn on IsDirty

            var organization2 = await organization.SaveAsync();
            var rowVersion1 = organization2.RowVersion;
            organization2.Notes = "another timestamp trigger";

            var organization3 = await organization2.SaveAsync();
            
            Assert.NotEqual(organization2.RowVersion, organization3.RowVersion);
        }
        
        [Fact]
        public async Task OrganizationER_InvalidGet()
        {
            await Assert.ThrowsAsync<DataPortalException>(() => OrganizationER.GetOrganizationER(999));
        }

        private async Task BuildValidOrganization(OrganizationER organization)
        {
            organization.Name = "organization name";
            organization.OrganizationType = await OrganizationTypeEC.GetOrganizationTypeEC(new OrganizationType() {Id = 1});
            organization.Notes = "notes for org";
            organization.LastUpdatedBy = "edm";
            organization.LastUpdatedDate = DateTime.Now;
            organization.DateOfFirstContact = DateTime.Now;
        }
    }
}
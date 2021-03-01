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
    public class OrganizationROC_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public OrganizationROC_Tests()
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
        public async Task OrganizationROC_Get()
        {
            var validOrganization = BuildValidOrganization();
            var organization = await OrganizationROC.GetOrganizationROC(validOrganization);

            Assert.NotNull(organization);
            Assert.IsType<OrganizationROC>(organization);
            Assert.Equal(validOrganization.Id, organization.Id);
            Assert.Equal(validOrganization.Name, organization.Name);
            Assert.Equal(validOrganization.OrganizationType.Id, organization.OrganizationType.Id);
            Assert.Equal(validOrganization.Notes, organization.Notes);
            Assert.Equal(validOrganization.LastUpdatedBy, organization.LastUpdatedBy);
            Assert.Equal(new SmartDate(validOrganization.LastUpdatedDate), organization.LastUpdatedDate);
            Assert.Equal(new SmartDate(validOrganization.DateOfFirstContact), organization.DateOfFirstContact);
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
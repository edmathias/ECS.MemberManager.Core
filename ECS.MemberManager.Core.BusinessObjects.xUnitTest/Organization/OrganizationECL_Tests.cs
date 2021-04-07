using System;
using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OrganizationECL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public OrganizationECL_Tests()
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
        private async void OrganizationECL_TestNewOrganizationList()
        {
            var organizationEdit = await OrganizationECL.NewOrganizationECL();

            Assert.NotNull(organizationEdit);
            Assert.IsType<OrganizationECL>(organizationEdit);
        }

        [Fact]
        private async void OrganizationECL_TestGetOrganizationECL()
        {
            var data = MockDb.Organizations;

            var organizationEdit = await OrganizationECL.GetOrganizationECL(data);

            Assert.NotNull(organizationEdit);
            Assert.Equal(3, organizationEdit.Count);
        }


        private async Task BuildOrganization(OrganizationEC organizationToBuild)
        {
            organizationToBuild.Name = "organization name";
            organizationToBuild.OrganizationType =
                await OrganizationTypeEC.GetOrganizationTypeEC(new OrganizationType() {Id = 1});
            organizationToBuild.Notes = "notes for org";
            organizationToBuild.LastUpdatedBy = "edm";
            organizationToBuild.LastUpdatedDate = DateTime.Now;
            organizationToBuild.DateOfFirstContact = DateTime.Now;
        }
    }
}
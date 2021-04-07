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
    public class OrganizationTypeECL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public OrganizationTypeECL_Tests()
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
        private async void OrganizationTypeECL_TestNewOrganizationTypeECL()
        {
            var organizationTypeECL = await OrganizationTypeECL.NewOrganizationTypeECL();

            Assert.NotNull(organizationTypeECL);
            Assert.IsType<OrganizationTypeECL>(organizationTypeECL);
        }

        [Fact]
        private async void OrganizationTypeECL_TestGetOrganizationTypeECL()
        {
            var organizationTypes = MockDb.OrganizationTypes;

            var organizationTypeECL = await OrganizationTypeECL.GetOrganizationTypeECL(organizationTypes);

            Assert.NotNull(organizationTypeECL);
            Assert.Equal(3, organizationTypeECL.Count);
        }

        private async Task BuildValidOrganizationType(OrganizationTypeEC organizationType)
        {
            organizationType.Name = "org name";
            organizationType.Notes = "org notes";
            organizationType.CategoryOfOrganization = await CategoryOfOrganizationEC.GetCategoryOfOrganizationEC(
                new CategoryOfOrganization
                {
                    Id = 1,
                    Category = "category name",
                    DisplayOrder = 1
                });
        }
    }
}
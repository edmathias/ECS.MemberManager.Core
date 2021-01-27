using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfOrganizationRORL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public CategoryOfOrganizationRORL_Tests()
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
        private async void AddressRORL_TestGetAddressRORL()
        {
            var categoryOfOrganizationTypeInfoList = await AddressRORL.GetAddressRORL();
            
            Assert.NotNull(categoryOfOrganizationTypeInfoList);
            Assert.True(categoryOfOrganizationTypeInfoList.IsReadOnly);
            Assert.Equal(3, categoryOfOrganizationTypeInfoList.Count);
        }
    }
}
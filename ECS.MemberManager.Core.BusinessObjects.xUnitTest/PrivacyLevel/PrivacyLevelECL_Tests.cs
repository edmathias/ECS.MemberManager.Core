using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PrivacyLevelECL_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PrivacyLevelECL_Tests()
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
        private async void PrivacyLevelECL_TestPrivacyLevelECL()
        {
            var privacyLevelEdit = await PrivacyLevelECL.NewPrivacyLevelECL();

            Assert.NotNull(privacyLevelEdit);
            Assert.IsType<PrivacyLevelECL>(privacyLevelEdit);
        }

        
        [Fact]
        private async void PrivacyLevelECL_TestGetPrivacyLevelECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPrivacyLevelDal>();
            var childData = await dal.Fetch();
            
            var listToTest = await PrivacyLevelECL.GetPrivacyLevelECL(childData);
            
            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
        
        private void BuildPrivacyLevel(PrivacyLevelEC privacyLevel)
        {
            privacyLevel.Description = "doc type description";
            privacyLevel.Notes = "document type notes";
        }
        
    }
}

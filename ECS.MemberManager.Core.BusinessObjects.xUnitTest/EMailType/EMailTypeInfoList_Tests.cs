using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EMailTypeInfoList_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EMailTypeInfoList_Tests()
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
        private async void EMailTypeInfoList_TestNewEMailTypeInfoList()
        {
            var eMailTypeInfoList = await EMailTypeInfoList.NewEMailTypeInfoList();

            Assert.NotNull(eMailTypeInfoList);
            Assert.IsType<EMailTypeInfoList>(eMailTypeInfoList);
            Assert.Empty(eMailTypeInfoList);
        }

        [Fact]
        private async void EMailTypeInfoList_TestGetEMailTypeInfoList()
        {
            var eMailTypeInfoList = await EMailTypeInfoList.GetEMailTypeInfoList();
            
            Assert.NotNull(eMailTypeInfoList);
            Assert.True(eMailTypeInfoList.IsReadOnly);
            Assert.Equal(3, eMailTypeInfoList.Count);
        }
      
    }
}
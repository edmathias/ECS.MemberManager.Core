using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PrivacyLevelEditList_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PrivacyLevelEditList_Tests()
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
        private async void PrivacyLevelEditList_TestNewPrivacyLevelEditList()
        {
            var privacyLevelErl = await PrivacyLevelEditList.NewPrivacyLevelEditList();

            Assert.NotNull(privacyLevelErl);
            Assert.IsType<PrivacyLevelEditList>(privacyLevelErl);
        }
        
        [Fact]
        private async void PrivacyLevelEditList_TestGetPrivacyLevelEditList()
        {
            var privacyLevelErl = await PrivacyLevelEditList.GetPrivacyLevelEditList();

            Assert.NotNull(privacyLevelErl);
            Assert.Equal(3, privacyLevelErl.Count);
        }
        
        [Fact]
        private async void PrivacyLevelEditList_TestDeletePrivacyLevelsEntry()
        {
            var privacyLevelErl = await PrivacyLevelEditList.GetPrivacyLevelEditList();
            var listCount = privacyLevelErl.Count;
            var privacyLevelToDelete = privacyLevelErl.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = privacyLevelErl.Remove(privacyLevelToDelete); 

            var privacyLevelListAfterDelete = await privacyLevelErl.SaveAsync();

            Assert.NotNull(privacyLevelListAfterDelete);
            Assert.IsType<PrivacyLevelEditList>(privacyLevelListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,privacyLevelListAfterDelete.Count);
        }

        [Fact]
        private async void PrivacyLevelEditList_TestUpdatePrivacyLevelsEntry()
        {
            const int idToUpdate = 1;
            
            var privacyLevelEditList = await PrivacyLevelEditList.GetPrivacyLevelEditList();
            var countBeforeUpdate = privacyLevelEditList.Count;
            var privacyLevelToUpdate = privacyLevelEditList.First(a => a.Id == idToUpdate);
            privacyLevelToUpdate.Description = "This was updated";

            var updatedList = await privacyLevelEditList.SaveAsync();
            
            Assert.Equal("This was updated",updatedList.First(a => a.Id == idToUpdate).Description);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void PrivacyLevelEditList_TestAddPrivacyLevelsEntry()
        {
            var privacyLevelEditList = await PrivacyLevelEditList.GetPrivacyLevelEditList();
            var countBeforeAdd = privacyLevelEditList.Count;
            
            var privacyLevelToAdd = privacyLevelEditList.AddNew();
            BuildPrivacyLevel(privacyLevelToAdd);

            var updatedPrivacyLevelsList = await privacyLevelEditList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedPrivacyLevelsList.Count);
        }

        private void BuildPrivacyLevel(PrivacyLevelEdit privacyLevelToBuild)
        {
            privacyLevelToBuild.Notes = "member type notes";
            privacyLevelToBuild.Description = "member type";
        }
        
 
    }
}
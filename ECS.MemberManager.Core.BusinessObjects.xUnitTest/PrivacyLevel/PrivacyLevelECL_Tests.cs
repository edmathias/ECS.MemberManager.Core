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
        
        [Fact]
        private async void PrivacyLevelECL_TestDeletePrivacyLevelEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPrivacyLevelDal>();
            var childData = await dal.Fetch();
            
            var privacyLevelEditList = await PrivacyLevelECL.GetPrivacyLevelECL(childData);

            var privacyLevel = privacyLevelEditList.First(a => a.Id == 99);

            // remove is deferred delete
            privacyLevelEditList.Remove(privacyLevel); 

            var privacyLevelListAfterDelete = await privacyLevelEditList.SaveAsync();
            
            Assert.NotEqual(childData.Count,privacyLevelListAfterDelete.Count);
        }

        [Fact]
        private async void PrivacyLevelECL_TestUpdatePrivacyLevelEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPrivacyLevelDal>();
            var childData = await dal.Fetch();
            
            var privacyLevelList = await PrivacyLevelECL.GetPrivacyLevelECL(childData);
            var countBeforeUpdate = privacyLevelList.Count;
            var idToUpdate = privacyLevelList.Min(a => a.Id);
            var privacyLevelToUpdate = privacyLevelList.First(a => a.Id == idToUpdate);

            privacyLevelToUpdate.Description = "This was updated";
            await privacyLevelList.SaveAsync();

            var updatedList = await dal.Fetch();
            var updatedPrivacyLevelsList = await PrivacyLevelECL.GetPrivacyLevelECL(updatedList);
            
            Assert.Equal("This was updated",updatedPrivacyLevelsList.First(a => a.Id == idToUpdate).Description);
            Assert.Equal(countBeforeUpdate, updatedPrivacyLevelsList.Count);
        }

        [Fact]
        private async void PrivacyLevelECL_TestAddPrivacyLevelEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPrivacyLevelDal>();
            var childData = await dal.Fetch();

            var privacyLevelList = await PrivacyLevelECL.GetPrivacyLevelECL(childData);
            var countBeforeAdd = privacyLevelList.Count;
            
            var privacyLevelToAdd = privacyLevelList.AddNew();
            BuildPrivacyLevel(privacyLevelToAdd); 

            var privacyLevelEditList = await privacyLevelList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, privacyLevelEditList.Count);
        }

        private void BuildPrivacyLevel(PrivacyLevelEC privacyLevel)
        {
            privacyLevel.Description = "doc type description";
            privacyLevel.Notes = "document type notes";
        }
        
    }
}

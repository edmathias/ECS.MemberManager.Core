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
    public class MemberStatusECL_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public MemberStatusECL_Tests()
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
        private async void MemberStatusECL_TestMemberStatusECL()
        {
            var memberStatusEdit = await MemberStatusECL.NewMemberStatusECL();

            Assert.NotNull(memberStatusEdit);
            Assert.IsType<MemberStatusECL>(memberStatusEdit);
        }

        
        [Fact]
        private async void MemberStatusECL_TestGetMemberStatusECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberStatusDal>();
            var childData = await dal.Fetch();
            
            var listToTest = await MemberStatusECL.GetMemberStatusECL(childData);
            
            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
        
        [Fact]
        private async void MemberStatusECL_TestDeleteMemberStatusEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberStatusDal>();
            var childData = await dal.Fetch();
            
            var memberStatusEditList = await MemberStatusECL.GetMemberStatusECL(childData);

            var memberStatus = memberStatusEditList.First(a => a.Id == 99);

            // remove is deferred delete
            memberStatusEditList.Remove(memberStatus); 

            var memberStatusListAfterDelete = await memberStatusEditList.SaveAsync();
            
            Assert.NotEqual(childData.Count,memberStatusListAfterDelete.Count);
        }

        [Fact]
        private async void MemberStatusECL_TestUpdateMemberStatusEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberStatusDal>();
            var childData = await dal.Fetch();
            
            var memberStatusList = await MemberStatusECL.GetMemberStatusECL(childData);
            var countBeforeUpdate = memberStatusList.Count;
            var idToUpdate = memberStatusList.Min(a => a.Id);
            var memberStatusToUpdate = memberStatusList.First(a => a.Id == idToUpdate);

            memberStatusToUpdate.Description = "This was updated";
            await memberStatusList.SaveAsync();

            var updatedList = await dal.Fetch();
            var updatedMemberStatussList = await MemberStatusECL.GetMemberStatusECL(updatedList);
            
            Assert.Equal("This was updated",updatedMemberStatussList.First(a => a.Id == idToUpdate).Description);
            Assert.Equal(countBeforeUpdate, updatedMemberStatussList.Count);
        }

        [Fact]
        private async void MemberStatusECL_TestAddMemberStatusEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberStatusDal>();
            var childData = await dal.Fetch();

            var memberStatusList = await MemberStatusECL.GetMemberStatusECL(childData);
            var countBeforeAdd = memberStatusList.Count;
            
            var memberStatusToAdd = memberStatusList.AddNew();
            BuildMemberStatus(memberStatusToAdd); 

            var memberStatusEditList = await memberStatusList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, memberStatusEditList.Count);
        }

        private void BuildMemberStatus(MemberStatusEC memberStatus)
        {
            memberStatus.Description = "doc type description";
            memberStatus.Notes = "document type notes";
        }
        
    }
}

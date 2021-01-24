using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Xunit;
using DalManager = ECS.MemberManager.Core.DataAccess.ADO.DalManager;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MemberStatusEditList_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public MemberStatusEditList_Tests()
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
        private async void MemberStatusEditList_TestNewMemberStatusList()
        {
            var memberStatusEdit = await MemberStatusEditList.NewMemberStatusEditList();

            Assert.NotNull(memberStatusEdit);
            Assert.IsType<MemberStatusEditList>(memberStatusEdit);
        }
        
        [Fact]
        private async void MemberStatusEditList_TestGetMemberStatusEditList()
        {
            var memberStatusEdit = await MemberStatusEditList.GetMemberStatusEditList();

            Assert.NotNull(memberStatusEdit);
            Assert.Equal(3, memberStatusEdit.Count);
        }
        
        [Fact]
        private async void MemberStatusEditList_TestDeleteMemberStatus()
        {
            var memberStatusEditList = await MemberStatusEditList.GetMemberStatusEditList();
            var listCount = memberStatusEditList.Count;
            var memberStatusToDelete = memberStatusEditList.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = memberStatusEditList.Remove(memberStatusToDelete); 

            var memberStatusListAfterDelete = await memberStatusEditList.SaveAsync();

            Assert.NotNull(memberStatusListAfterDelete);
            Assert.IsType<MemberStatusEditList>(memberStatusListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,memberStatusListAfterDelete.Count);
        }

        [Fact]
        private async void MemberStatusEditList_TestUpdateMemberStatus()
        {
            const int idToUpdate = 1;
            
            var memberStatusEditList = await MemberStatusEditList.GetMemberStatusEditList();
            var countBeforeUpdate = memberStatusEditList.Count;
            var memberStatusToUpdate = memberStatusEditList.First(a => a.Id == idToUpdate);
            memberStatusToUpdate.Notes = "This was updated";

            var updatedList = await memberStatusEditList.SaveAsync();
            
            Assert.Equal("This was updated",updatedList.First(a => a.Id == idToUpdate).Notes);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void MemberStatusEditList_TestAddMemberStatusEntry()
        {
            var memberStatusEditList = await MemberStatusEditList.GetMemberStatusEditList();
            var countBeforeAdd = memberStatusEditList.Count;
            
            var memberStatusToAdd = memberStatusEditList.AddNew();
            BuildMemberStatus(memberStatusToAdd);

            var updatedMemberStatussList = await memberStatusEditList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedMemberStatussList.Count);
        }

        private void BuildMemberStatus(MemberStatusEdit memberStatusToBuild)
        {
            memberStatusToBuild.Description = "memberstatus description";
            memberStatusToBuild.Notes = "notes for memberstatus";
        }
    }
}
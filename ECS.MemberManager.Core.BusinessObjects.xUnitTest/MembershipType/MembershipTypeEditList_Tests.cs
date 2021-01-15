using System;
using System.IO;
using System.Linq;
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
    public class MembershipTypeEditList_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public MembershipTypeEditList_Tests()
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
        private async void MembershipTypeEditList_TestNewMembershipTypeList()
        {
            var membershipTypeEdit = await MembershipTypeEditList.NewMembershipTypeEditList();

            Assert.NotNull(membershipTypeEdit);
            Assert.IsType<MembershipTypeEditList>(membershipTypeEdit);
        }
        
        [Fact]
        private async void MembershipTypeEditList_TestGetMembershipTypeEditList()
        {
            var membershipTypeEdit = await MembershipTypeEditList.GetMembershipTypeEditList();

            Assert.NotNull(membershipTypeEdit);
            Assert.Equal(3, membershipTypeEdit.Count);
        }
        
        [Fact]
        private async void MembershipTypeEditList_TestDeleteMembershipTypesEntry()
        {
            var membershipTypeEditList = await MembershipTypeEditList.GetMembershipTypeEditList();
            var listCount = membershipTypeEditList.Count;
            var membershipTypeToDelete = membershipTypeEditList.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = membershipTypeEditList.Remove(membershipTypeToDelete); 

            var membershipTypeListAfterDelete = await membershipTypeEditList.SaveAsync();

            Assert.NotNull(membershipTypeListAfterDelete);
            Assert.IsType<MembershipTypeEditList>(membershipTypeListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,membershipTypeListAfterDelete.Count);
        }

        [Fact]
        private async void MembershipTypeEditList_TestUpdateMembershipTypesEntry()
        {
            const int idToUpdate = 1;
            
            var membershipTypeEditList = await MembershipTypeEditList.GetMembershipTypeEditList();
            var countBeforeUpdate = membershipTypeEditList.Count;
            var membershipTypeToUpdate = membershipTypeEditList.First(a => a.Id == idToUpdate);
            membershipTypeToUpdate.Description = "This was updated";

            var updatedList = await membershipTypeEditList.SaveAsync();
            
            Assert.Equal("This was updated",updatedList.First(a => a.Id == idToUpdate).Description);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void MembershipTypeEditList_TestAddMembershipTypesEntry()
        {
            var membershipTypeEditList = await MembershipTypeEditList.GetMembershipTypeEditList();
            var countBeforeAdd = membershipTypeEditList.Count;
            
            var membershipTypeToAdd = membershipTypeEditList.AddNew();
            BuildMembershipType(membershipTypeToAdd);

            var updatedMembershipTypesList = await membershipTypeEditList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedMembershipTypesList.Count);
        }

        private void BuildMembershipType(MembershipTypeEdit membershipTypeToBuild)
        {
            membershipTypeToBuild.Description = "membership type description";
            membershipTypeToBuild.Level = 1;
            membershipTypeToBuild.LastUpdatedBy = "edm";
            membershipTypeToBuild.LastUpdatedDate = DateTime.Now;
            membershipTypeToBuild.Notes = "membershipType type notes";
        }
        
 
    }
}
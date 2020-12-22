using System;
using System.Linq;
using Csla;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MembershipTypeERL_Tests
    {
        public MembershipTypeERL_Tests()
        {
            MockDb.ResetMockDb();
        }
        
        [Fact]
        private async void MembershipTypeERL_TestGetMembershipTypeList()
        {
            var listToTest = MockDb.MembershipTypes;
            
            var membershipTypeErl = await MembershipTypeERL.GetMembershipTypeList(listToTest);

            Assert.NotNull(membershipTypeErl);
            Assert.Equal(MockDb.MembershipTypes.Count, membershipTypeErl.Count);
        }
        
        [Fact]
        private async void MembershipTypeERL_TestDeleteMembershipTypesEntry()
        {
            var listToTest = MockDb.MembershipTypes;
            var listCount = listToTest.Count;
            
            var idToDelete = MockDb.MembershipTypes.Max(a => a.Id);
            var membershipTypeErl = await MembershipTypeERL.GetMembershipTypeList(listToTest);

            var membershipTypeToDelete = membershipTypeErl.First(a => a.Id == idToDelete);

            // remove is deferred delete
            membershipTypeErl.Remove(membershipTypeToDelete); 

            var membershipTypeListAfterDelete = await membershipTypeErl.SaveAsync();
            
            Assert.NotEqual(listCount,membershipTypeListAfterDelete.Count);
        }

        [Fact]
        private async void MembershipTypeERL_TestUpdateMembershipTypesEntry()
        {
            var membershipTypeList = await MembershipTypeERL.GetMembershipTypeList(MockDb.MembershipTypes);
            var countBeforeUpdate = membershipTypeList.Count;
            var idToUpdate = MockDb.MembershipTypes.Min(a => a.Id);
            var membershipTypeToUpdate = membershipTypeList.First(a => a.Id == idToUpdate);

            membershipTypeToUpdate.Description = "This was updated";
            await membershipTypeList.SaveAsync();
            
            var updatedMembershipTypesList = await MembershipTypeERL.GetMembershipTypeList(MockDb.MembershipTypes);
            
            Assert.Equal("This was updated",updatedMembershipTypesList.First(a => a.Id == idToUpdate).Description);
            Assert.Equal(countBeforeUpdate, updatedMembershipTypesList.Count);
        }

        [Fact]
        private async void MembershipTypeERL_TestAddMembershipTypesEntry()
        {
            var membershipTypeList = await MembershipTypeERL.GetMembershipTypeList(MockDb.MembershipTypes);
            var countBeforeAdd = membershipTypeList.Count;
            
            var membershipTypeToAdd = membershipTypeList.AddNew();
            BuildMembershipType(membershipTypeToAdd); 

            await membershipTypeList.SaveAsync();
            
            var updatedMembershipTypesList = await MembershipTypeERL.GetMembershipTypeList(MockDb.MembershipTypes);
            
            Assert.NotEqual(countBeforeAdd, updatedMembershipTypesList.Count);
        }

        private void BuildMembershipType(MembershipTypeEC membershipTypeToBuild)
        {
            membershipTypeToBuild.Level = 1;
            membershipTypeToBuild.Notes = "member type notes";
            membershipTypeToBuild.LastUpdatedBy = "edm";
            membershipTypeToBuild.LastUpdatedDate = new SmartDate();
            membershipTypeToBuild.Description = "member type";
        }
    }
}
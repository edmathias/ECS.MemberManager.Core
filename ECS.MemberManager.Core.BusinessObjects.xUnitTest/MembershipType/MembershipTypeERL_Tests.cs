using System;
using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MembershipTypeERL_Tests
    {
        [Fact]
        private async void MembershipTypeERL_TestNewMembershipTypeERL()
        {
            var membershipTypeEdit = await MembershipTypeERL.NewMembershipTypeERL();

            Assert.NotNull(membershipTypeEdit);
            Assert.IsType<MembershipTypeERL>(membershipTypeEdit);
        }

        [Fact]
        private async void MembershipTypeERL_TestGetMembershipTypeERL()
        {
            var membershipTypeEdit =
                await MembershipTypeERL.GetMembershipTypeERL();

            Assert.NotNull(membershipTypeEdit);
            Assert.Equal(3, membershipTypeEdit.Count);
        }

        [Fact]
        private async void MembershipTypeERL_TestDeleteMembershipTypeERL()
        {
            const int ID_TO_DELETE = 99;
            var membershipTypeList =
                await MembershipTypeERL.GetMembershipTypeERL();
            var listCount = membershipTypeList.Count;
            var membershipTypeToDelete = membershipTypeList.First(cl => cl.Id == ID_TO_DELETE);
            // remove is deferred delete
            var isDeleted = membershipTypeList.Remove(membershipTypeToDelete);

            var membershipTypeListAfterDelete = await membershipTypeList.SaveAsync();

            Assert.NotNull(membershipTypeListAfterDelete);
            Assert.IsType<MembershipTypeERL>(membershipTypeListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount, membershipTypeListAfterDelete.Count);
        }

        [Fact]
        private async void MembershipTypeERL_TestUpdateMembershipTypeERL()
        {
            const int ID_TO_UPDATE = 1;

            var membershipTypeList =
                await MembershipTypeERL.GetMembershipTypeERL();
            var countBeforeUpdate = membershipTypeList.Count;
            var membershipTypeToUpdate = membershipTypeList.First(cl => cl.Id == ID_TO_UPDATE);
            membershipTypeToUpdate.Notes = "Updated Notes";

            var updatedList = await membershipTypeList.SaveAsync();

            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void MembershipTypeERL_TestAddMembershipTypeERL()
        {
            var membershipTypeList =
                await MembershipTypeERL.GetMembershipTypeERL();
            var countBeforeAdd = membershipTypeList.Count;

            var membershipTypeToAdd = membershipTypeList.AddNew();
            BuildMembershipType(membershipTypeToAdd);

            var updatedMembershipTypeList = await membershipTypeList.SaveAsync();

            Assert.NotEqual(countBeforeAdd, updatedMembershipTypeList.Count);
        }

        private void BuildMembershipType(MembershipTypeEC membershipTypeToBuild)
        {
            membershipTypeToBuild.Description = "description for type";
            membershipTypeToBuild.Level = 1;
            membershipTypeToBuild.LastUpdatedBy = "test";
            membershipTypeToBuild.LastUpdatedDate = DateTime.Now;
            membershipTypeToBuild.Notes = "notes for doctype";
        }
    }
}
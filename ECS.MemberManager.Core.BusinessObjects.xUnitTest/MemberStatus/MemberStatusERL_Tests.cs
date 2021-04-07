using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MemberStatusERL_Tests : CslaBaseTest
    {
        [Fact]
        private async void MemberStatusERL_TestNewMemberStatusERL()
        {
            var memberStatusEdit = await MemberStatusERL.NewMemberStatusERL();

            Assert.NotNull(memberStatusEdit);
            Assert.IsType<MemberStatusERL>(memberStatusEdit);
        }

        [Fact]
        private async void MemberStatusERL_TestGetMemberStatusERL()
        {
            var memberStatusEdit =
                await MemberStatusERL.GetMemberStatusERL();

            Assert.NotNull(memberStatusEdit);
            Assert.Equal(3, memberStatusEdit.Count);
        }

        [Fact]
        private async void MemberStatusERL_TestDeleteMemberStatusERL()
        {
            const int ID_TO_DELETE = 99;
            var categoryList =
                await MemberStatusERL.GetMemberStatusERL();
            var listCount = categoryList.Count;
            var categoryToDelete = categoryList.First(cl => cl.Id == ID_TO_DELETE);
            // remove is deferred delete
            var isDeleted = categoryList.Remove(categoryToDelete);

            var memberStatusListAfterDelete = await categoryList.SaveAsync();

            Assert.NotNull(memberStatusListAfterDelete);
            Assert.IsType<MemberStatusERL>(memberStatusListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount, memberStatusListAfterDelete.Count);
        }

        [Fact]
        private async void MemberStatusERL_TestUpdateMemberStatusERL()
        {
            const int ID_TO_UPDATE = 1;
            const string NOTES_UPDATE = "Updated Notes";

            var categoryList =
                await MemberStatusERL.GetMemberStatusERL();
            var memberStatusToUpdate = categoryList.First(cl => cl.Id == ID_TO_UPDATE);
            memberStatusToUpdate.Notes = NOTES_UPDATE;

            var updatedList = await categoryList.SaveAsync();
            var updatedMemberStatus = updatedList.First(el => el.Id == ID_TO_UPDATE);

            Assert.NotNull(updatedList);
            Assert.NotNull(updatedMemberStatus);
            Assert.Equal(NOTES_UPDATE, updatedMemberStatus.Notes);
        }

        [Fact]
        private async void MemberStatusERL_TestAddMemberStatusERL()
        {
            var categoryList =
                await MemberStatusERL.GetMemberStatusERL();
            var countBeforeAdd = categoryList.Count;

            var memberStatusToAdd = categoryList.AddNew();
            BuildMemberStatus(memberStatusToAdd);

            var updatedCategoryList = await categoryList.SaveAsync();

            Assert.NotEqual(countBeforeAdd, updatedCategoryList.Count);
        }

        private void BuildMemberStatus(MemberStatusEC categoryToBuild)
        {
            categoryToBuild.Description = "description for doctype";
            categoryToBuild.Notes = "notes for doctype";
        }
    }
}
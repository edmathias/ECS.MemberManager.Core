using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PrivacyLevelERL_Tests : CslaBaseTest
    {
        [Fact]
        private async void PrivacyLevelERL_TestNewPrivacyLevelERL()
        {
            var privacyLevelEdit = await PrivacyLevelERL.NewPrivacyLevelERL();

            Assert.NotNull(privacyLevelEdit);
            Assert.IsType<PrivacyLevelERL>(privacyLevelEdit);
        }

        [Fact]
        private async void PrivacyLevelERL_TestGetPrivacyLevelERL()
        {
            var privacyLevelEdit =
                await PrivacyLevelERL.GetPrivacyLevelERL();

            Assert.NotNull(privacyLevelEdit);
            Assert.Equal(3, privacyLevelEdit.Count);
        }

        [Fact]
        private async void PrivacyLevelERL_TestDeletePrivacyLevelERL()
        {
            const int ID_TO_DELETE = 99;
            var categoryList =
                await PrivacyLevelERL.GetPrivacyLevelERL();
            var listCount = categoryList.Count;
            var categoryToDelete = categoryList.First(cl => cl.Id == ID_TO_DELETE);
            // remove is deferred delete
            var isDeleted = categoryList.Remove(categoryToDelete);

            var privacyLevelListAfterDelete = await categoryList.SaveAsync();

            Assert.NotNull(privacyLevelListAfterDelete);
            Assert.IsType<PrivacyLevelERL>(privacyLevelListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount, privacyLevelListAfterDelete.Count);
        }

        [Fact]
        private async void PrivacyLevelERL_TestUpdatePrivacyLevelERL()
        {
            const int ID_TO_UPDATE = 1;
            const string NOTES_UPDATE = "Updated Notes";

            var categoryList =
                await PrivacyLevelERL.GetPrivacyLevelERL();
            var privacyLevelToUpdate = categoryList.First(cl => cl.Id == ID_TO_UPDATE);
            privacyLevelToUpdate.Notes = NOTES_UPDATE;

            var updatedList = await categoryList.SaveAsync();
            var updatedPrivacyLevel = updatedList.First(el => el.Id == ID_TO_UPDATE);

            Assert.NotNull(updatedList);
            Assert.NotNull(updatedPrivacyLevel);
            Assert.Equal(NOTES_UPDATE, updatedPrivacyLevel.Notes);
        }

        [Fact]
        private async void PrivacyLevelERL_TestAddPrivacyLevelERL()
        {
            var categoryList =
                await PrivacyLevelERL.GetPrivacyLevelERL();
            var countBeforeAdd = categoryList.Count;

            var privacyLevelToAdd = categoryList.AddNew();
            BuildPrivacyLevel(privacyLevelToAdd);

            var updatedCategoryList = await categoryList.SaveAsync();

            Assert.NotEqual(countBeforeAdd, updatedCategoryList.Count);
        }

        private void BuildPrivacyLevel(PrivacyLevelEC categoryToBuild)
        {
            categoryToBuild.Description = "description for doctype";
            categoryToBuild.Notes = "notes for doctype";
        }
    }
}
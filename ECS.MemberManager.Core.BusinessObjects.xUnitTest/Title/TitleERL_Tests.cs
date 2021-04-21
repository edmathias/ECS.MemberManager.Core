using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TitleERL_Tests : CslaBaseTest
    {
        [Fact]
        private async void TitleERL_TestNewTitleERL()
        {
            var titleOfPersonEdit = await TitleERL.NewTitleERL();

            Assert.NotNull(titleOfPersonEdit);
            Assert.IsType<TitleERL>(titleOfPersonEdit);
        }

        [Fact]
        private async void TitleERL_TestGetTitleERL()
        {
            var titleOfPersonEdit =
                await TitleERL.GetTitleERL();

            Assert.NotNull(titleOfPersonEdit);
            Assert.Equal(3, titleOfPersonEdit.Count);
        }

        [Fact]
        private async void TitleERL_TestDeleteTitleERL()
        {
            const int ID_TO_DELETE = 99;
            var titleList =
                await TitleERL.GetTitleERL();
            var listCount = titleList.Count;
            var titleToDelete = titleList.First(cl => cl.Id == ID_TO_DELETE);
            // remove is deferred delete
            var isDeleted = titleList.Remove(titleToDelete);

            var titleOfPersonListAfterDelete = await titleList.SaveAsync();

            Assert.NotNull(titleOfPersonListAfterDelete);
            Assert.IsType<TitleERL>(titleOfPersonListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount, titleOfPersonListAfterDelete.Count);
        }

        [Fact]
        private async void TitleERL_TestUpdateTitleERL()
        {
            const int ID_TO_UPDATE = 1;
            const string DESCRIPTION_UPDATE = "Updated description";

            var titleList =
                await TitleERL.GetTitleERL();
            var titleOfPersonToUpdate = titleList.First(cl => cl.Id == ID_TO_UPDATE);
            titleOfPersonToUpdate.Description = DESCRIPTION_UPDATE;

            var updatedList = await titleList.SaveAsync();
            var updatedEMail = updatedList.First(el => el.Id == ID_TO_UPDATE);

            Assert.NotNull(updatedList);
            Assert.NotNull(updatedEMail);
            Assert.Equal(DESCRIPTION_UPDATE, updatedEMail.Description);
        }

        [Fact]
        private async void TitleERL_TestAddTitleERL()
        {
            var titleList =
                await TitleERL.GetTitleERL();
            var countBeforeAdd = titleList.Count;

            var titleOfPersonToAdd = titleList.AddNew();
            BuildTitle(titleOfPersonToAdd);

            var updatedCategoryList = await titleList.SaveAsync();

            Assert.NotEqual(countBeforeAdd, updatedCategoryList.Count);
        }

        private void BuildTitle(TitleEC titleToBuild)
        {
            titleToBuild.Abbreviation = "abbrev";
            titleToBuild.Description = "description for title";
            titleToBuild.DisplayOrder = 1;
        }
    }
}
using System.Linq;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfPersonERL_Tests : CslaBaseTest
    {
        [Fact]
        private async void CategoryOfPersonERL_TestNewCategoryOfPersonERL()
        {
            var categoryOfPersonEdit = await CategoryOfPersonERL.NewCategoryOfPersonERL();

            Assert.NotNull(categoryOfPersonEdit);
            Assert.IsType<CategoryOfPersonERL>(categoryOfPersonEdit);
        }

        [Fact]
        private async void CategoryOfPersonERL_TestGetCategoryOfPersonERL()
        {
            var categoryOfPersonEdit =
                await CategoryOfPersonERL.GetCategoryOfPersonERL();

            Assert.NotNull(categoryOfPersonEdit);
            Assert.Equal(3, categoryOfPersonEdit.Count);
        }

        [Fact]
        private async void CategoryOfPersonERL_TestDeleteCategoryOfPersonERL()
        {
            const int ID_TO_DELETE = 99;
            var categoryList =
                await CategoryOfPersonERL.GetCategoryOfPersonERL();
            var listCount = categoryList.Count;
            var categoryToDelete = categoryList.First(cl => cl.Id == ID_TO_DELETE);
            // remove is deferred delete
            var isDeleted = categoryList.Remove(categoryToDelete);

            var categoryOfPersonListAfterDelete = await categoryList.SaveAsync();

            Assert.NotNull(categoryOfPersonListAfterDelete);
            Assert.IsType<CategoryOfPersonERL>(categoryOfPersonListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount, categoryOfPersonListAfterDelete.Count);
        }

        [Fact]
        private async void CategoryOfPersonERL_TestUpdateCategoryOfPersonERL()
        {
            const int ID_TO_UPDATE = 1;

            var categoryList =
                await CategoryOfPersonERL.GetCategoryOfPersonERL();
            var countBeforeUpdate = categoryList.Count;
            var categoryOfPersonToUpdate = categoryList.First(cl => cl.Id == ID_TO_UPDATE);
            categoryOfPersonToUpdate.Category = "Updated category";

            var updatedList = await categoryList.SaveAsync();

            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void CategoryOfPersonERL_TestAddCategoryOfPersonERL()
        {
            var categoryList =
                await CategoryOfPersonERL.GetCategoryOfPersonERL();
            var countBeforeAdd = categoryList.Count;

            var categoryOfPersonToAdd = categoryList.AddNew();
            BuildCategoryOfPerson(categoryOfPersonToAdd);

            var updatedCategoryList = await categoryList.SaveAsync();

            Assert.NotEqual(countBeforeAdd, updatedCategoryList.Count);
        }

        private void BuildCategoryOfPerson(CategoryOfPersonEC categoryToBuild)
        {
            categoryToBuild.Category = "test";
            categoryToBuild.DisplayOrder = 1;
        }
    }
}
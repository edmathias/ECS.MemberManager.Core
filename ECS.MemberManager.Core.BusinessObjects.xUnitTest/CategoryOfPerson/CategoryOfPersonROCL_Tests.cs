using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfPersonROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void CategoryOfPersonROCL_TestGetCategoryOfPersonROCL()
        {
            var categoryList = MockDb.CategoryOfPersons;

            var categoryOfPersonInfoList = await CategoryOfPersonROCL.GetCategoryOfPersonROCL(categoryList);

            Assert.NotNull(categoryOfPersonInfoList);
            Assert.True(categoryOfPersonInfoList.IsReadOnly);
            Assert.Equal(3, categoryOfPersonInfoList.Count);
        }

        private CategoryOfPerson BuildCategoryOfPerson()
        {
            var categoryToBuild = new CategoryOfPerson();
            categoryToBuild.Category = "test";
            categoryToBuild.DisplayOrder = 1;

            return categoryToBuild;
        }
    }
}
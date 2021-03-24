using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfPersonEC_Tests : CslaBaseTest
    {
        [Fact]
        public async Task TestCategoryOfPersonEC_NewCategoryOfPersonEC()
        {
            var category = await CategoryOfPersonEC.NewCategoryOfPersonEC();

            Assert.NotNull(category);
            Assert.IsType<CategoryOfPersonEC>(category);
            Assert.False(category.IsValid);
        }

        [Fact]
        public async Task TestCategoryOfPersonEC_GetCategoryOfPersonEC()
        {
            var categoryOfPersonToLoad = BuildCategoryOfPerson();
            var categoryOfPerson = await CategoryOfPersonEC.GetCategoryOfPersonEC(categoryOfPersonToLoad);

            Assert.NotNull(categoryOfPerson);
            Assert.IsType<CategoryOfPersonEC>(categoryOfPerson);
            Assert.Equal(categoryOfPersonToLoad.Id, categoryOfPerson.Id);
            Assert.Equal(categoryOfPersonToLoad.Category, categoryOfPerson.Category);
            Assert.Equal(categoryOfPersonToLoad.DisplayOrder, categoryOfPerson.DisplayOrder);
            Assert.True(categoryOfPerson.IsValid);
        }

        [Fact]
        public async Task TestCategoryOfPersonEC_CategoryRequired()
        {
            var categoryToTest = BuildCategoryOfPerson();
            var category = await CategoryOfPersonEC.GetCategoryOfPersonEC(categoryToTest);
            var isObjectValidInit = category.IsValid;
            category.Category = string.Empty;

            Assert.NotNull(category);
            Assert.True(isObjectValidInit);
            Assert.False(category.IsValid);
            Assert.Equal("Category", category.BrokenRulesCollection[0].Property);
            Assert.Equal("Category required", category.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestCategoryOfPersonEC_CategoryExceedsMaxLengthOf50()
        {
            var categoryToTest = BuildCategoryOfPerson();
            var category = await CategoryOfPersonEC.GetCategoryOfPersonEC(categoryToTest);
            var isObjectValidInit = category.IsValid;
            category.Category =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(category);
            Assert.True(isObjectValidInit);
            Assert.False(category.IsValid);
            Assert.Equal("Category", category.BrokenRulesCollection[0].Property);
            Assert.Equal("Category can not exceed 50 characters", category.BrokenRulesCollection[0].Description);
        }

        private CategoryOfPerson BuildCategoryOfPerson()
        {
            var categoryToBuild = new CategoryOfPerson();
            categoryToBuild.Id = 1;
            categoryToBuild.Category = "test";
            categoryToBuild.DisplayOrder = 1;

            return categoryToBuild;
        }
    }
}
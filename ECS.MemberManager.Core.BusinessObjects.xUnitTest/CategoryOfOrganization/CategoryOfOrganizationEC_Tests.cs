using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfOrganizationEC_Tests : CslaBaseTest
    {
        [Fact]
        public async Task TestCategoryOfOrganizationEC_GetNewCategoryOfOrganizationEC()
        {
            var categoryOfOrganizationToLoad = BuildCategoryOfOrganization();
            var categoryOfOrganization =
                await CategoryOfOrganizationEC.GetCategoryOfOrganizationEC(categoryOfOrganizationToLoad);

            Assert.NotNull(categoryOfOrganization);
            Assert.IsType<CategoryOfOrganizationEC>(categoryOfOrganization);
            Assert.Equal(categoryOfOrganizationToLoad.Id, categoryOfOrganization.Id);
            Assert.Equal(categoryOfOrganizationToLoad.Category, categoryOfOrganization.Category);
            Assert.Equal(categoryOfOrganizationToLoad.DisplayOrder, categoryOfOrganization.DisplayOrder);
            Assert.True(categoryOfOrganization.IsValid);
        }

        [Fact]
        public async Task TestCategoryOfOrganizationEC_NewCategoryOfOrganizationEC()
        {
            var category = await CategoryOfOrganizationEC.NewCategoryOfOrganizationEC();

            Assert.NotNull(category);
            Assert.IsType<CategoryOfOrganizationEC>(category);
            Assert.False(category.IsValid);
        }

        [Fact]
        public async Task TestCategoryOfOrganizationEC_CategoryRequired()
        {
            var categoryToTest = BuildCategoryOfOrganization();
            var category = await CategoryOfOrganizationEC.GetCategoryOfOrganizationEC(categoryToTest);
            var isObjectValidInit = category.IsValid;
            category.Category = string.Empty;

            Assert.NotNull(category);
            Assert.True(isObjectValidInit);
            Assert.False(category.IsValid);
            Assert.Equal("Category", category.BrokenRulesCollection[0].Property);
            Assert.Equal("Category required", category.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task CategoryOfOrganizationEC_CategoryExceedsMaxLengthOf35()
        {
            var categoryToTest = BuildCategoryOfOrganization();
            var category = await CategoryOfOrganizationEC.GetCategoryOfOrganizationEC(categoryToTest);
            var isObjectValidInit = category.IsValid;
            category.Category = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(category);
            Assert.True(isObjectValidInit);
            Assert.False(category.IsValid);
            Assert.Equal("Category", category.BrokenRulesCollection[0].Property);
            Assert.Equal("Category can not exceed 35 characters", category.BrokenRulesCollection[0].Description);
        }

        private CategoryOfOrganization BuildCategoryOfOrganization()
        {
            var category = new CategoryOfOrganization()
            {
                Category = "category name 1",
                DisplayOrder = 0
            };

            return category;
        }
    }
}
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfOrganizationROC_Tests : CslaBaseTest
    {
        [Fact]
        public async void CategoryOfOrganizationROC_TestGetById()
        {
            var categoryToTest = BuildCategoryOfOrganization();
            var category = await CategoryOfOrganizationROC.GetCategoryOfOrganizationROC(categoryToTest);

            Assert.NotNull(category);
            Assert.IsType<CategoryOfOrganizationROC>(category);
            Assert.Equal(categoryToTest.Id, category.Id);
            Assert.Equal(categoryToTest.Category, category.Category);
            Assert.Equal(categoryToTest.DisplayOrder, category.DisplayOrder);
        }

        private CategoryOfOrganization BuildCategoryOfOrganization()
        {
            var category = new CategoryOfOrganization()
            {
                Id = 1,
                Category = "org category 1",
                DisplayOrder = 1
            };

            return category;
        }
    }
}
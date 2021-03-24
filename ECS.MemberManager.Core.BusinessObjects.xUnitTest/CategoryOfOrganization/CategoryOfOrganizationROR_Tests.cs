using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfOrganizationROR_Tests : CslaBaseTest
    {
        [Fact]
        public async void CategoryOfOrganizationInfo_TestGetById()
        {
            var categoryOfOrganizationInfo = await CategoryOfOrganizationROR.GetCategoryOfOrganizationROR(1);

            Assert.NotNull(categoryOfOrganizationInfo);
            Assert.IsType<CategoryOfOrganizationROR>(categoryOfOrganizationInfo);
            Assert.Equal(1, categoryOfOrganizationInfo.Id);
        }
    }
}
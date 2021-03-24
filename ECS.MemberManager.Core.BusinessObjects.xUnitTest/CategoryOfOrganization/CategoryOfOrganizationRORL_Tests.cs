using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfOrganizationRORL_Tests : CslaBaseTest
    {
        private async void CategoryOfOrganizationRORL_TestGetCategoryOfOrganizationRORL()
        {
            var categoryOfOrganizationTypeInfoList = await CategoryOfOrganizationRORL.GetCategoryOfOrganizationRORL();

            Assert.NotNull(categoryOfOrganizationTypeInfoList);
            Assert.True(categoryOfOrganizationTypeInfoList.IsReadOnly);
            Assert.Equal(3, categoryOfOrganizationTypeInfoList.Count);
        }
    }
}
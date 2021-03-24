using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfPersonRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void CategoryOfPersonRORL_TestGetCategoryOfPersonRORL()
        {
            var categoryOfPersonTypeInfoList = await CategoryOfPersonRORL.GetCategoryOfPersonRORL();

            Assert.NotNull(categoryOfPersonTypeInfoList);
            Assert.True(categoryOfPersonTypeInfoList.IsReadOnly);
            Assert.Equal(3, categoryOfPersonTypeInfoList.Count);
        }
    }
}
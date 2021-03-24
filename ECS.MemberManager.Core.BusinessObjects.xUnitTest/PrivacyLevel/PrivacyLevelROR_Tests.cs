using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PrivacyLevelROR_Tests
    {
        [Fact]
        public async void PrivacyLevelROR_TestGetById()
        {
            var category = await PrivacyLevelROR.GetPrivacyLevelROR(1);

            Assert.NotNull(category);
            Assert.IsType<PrivacyLevelROR>(category);
            Assert.Equal(1, category.Id);
        }
    }
}
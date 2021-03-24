using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EMailTypeROR_Tests
    {
        [Fact]
        public async void EMailTypeROR_TestGetById()
        {
            var category = await EMailTypeROR.GetEMailTypeROR(1);

            Assert.NotNull(category);
            Assert.IsType<EMailTypeROR>(category);
            Assert.Equal(1, category.Id);
        }
    }
}
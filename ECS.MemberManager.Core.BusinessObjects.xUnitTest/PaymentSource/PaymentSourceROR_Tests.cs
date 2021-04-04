using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentSourceROR_Tests : CslaBaseTest
    {
        [Fact]
        public async void PaymentSourceROR_TestGetById()
        {
            var category = await PaymentSourceROR.GetPaymentSourceROR(1);

            Assert.NotNull(category);
            Assert.IsType<PaymentSourceROR>(category);
            Assert.Equal(1, category.Id);
        }
    }
}
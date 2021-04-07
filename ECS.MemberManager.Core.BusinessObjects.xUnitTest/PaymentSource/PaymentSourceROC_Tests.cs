using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentSourceROC_Tests
    {
        [Fact]
        public async void PaymentSourceROC_TestGetChild()
        {
            const int ID_VALUE = 999;

            var buildPaymentSource = BuildPaymentSource();
            buildPaymentSource.Id = ID_VALUE;

            var paymentSource = await PaymentSourceROC.GetPaymentSourceROC(buildPaymentSource);

            Assert.NotNull(paymentSource);
            Assert.IsType<PaymentSourceROC>(paymentSource);
            Assert.Equal(paymentSource.Id, buildPaymentSource.Id);
            Assert.Equal(paymentSource.Description, buildPaymentSource.Description);
            Assert.Equal(paymentSource.Notes, buildPaymentSource.Notes);
            Assert.Equal(paymentSource.RowVersion, buildPaymentSource.RowVersion);
        }

        private PaymentSource BuildPaymentSource()
        {
            var paymentSource = new PaymentSource();
            paymentSource.Id = 1;
            paymentSource.Description = "test description";
            paymentSource.Notes = "notes for doctype";

            return paymentSource;
        }
    }
}
using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentSourceECL_Tests : CslaBaseTest
    {
        [Fact]
        private async void PaymentSourceECL_TestPaymentSourceECL()
        {
            var paymentSourceEdit = await PaymentSourceECL.NewPaymentSourceECL();

            Assert.NotNull(paymentSourceEdit);
            Assert.IsType<PaymentSourceECL>(paymentSourceEdit);
        }

        [Fact]
        private async void PaymentSourceECL_TestGetPaymentSourceECL()
        {
            var childData = MockDb.PaymentSources;

            var listToTest = await PaymentSourceECL.GetPaymentSourceECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }

        private void BuildPaymentSource(PaymentSourceEC paymentSource)
        {
            paymentSource.Description = "doc type description";
            paymentSource.Notes = "document type notes";
        }
    }
}
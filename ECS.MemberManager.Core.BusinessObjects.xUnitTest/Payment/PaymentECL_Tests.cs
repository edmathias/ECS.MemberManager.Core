using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentECL_Tests : CslaBaseTest
    {
        [Fact]
        private async void PaymentECL_TestNewPaymentList()
        {
            var paymentEdit = await PaymentECL.NewPaymentECL();

            Assert.NotNull(paymentEdit);
            Assert.IsType<PaymentECL>(paymentEdit);
        }

        [Fact]
        private async void PaymentECL_TestGetPaymentECL()
        {
            var data = MockDb.Payments;

            var paymentEdit = await PaymentECL.GetPaymentECL(data);

            Assert.NotNull(paymentEdit);
            Assert.Equal(3, paymentEdit.Count);
        }
    }
}
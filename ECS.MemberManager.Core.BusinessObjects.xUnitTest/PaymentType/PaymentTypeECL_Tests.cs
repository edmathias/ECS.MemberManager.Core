using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentTypeECL_Tests : CslaBaseTest
    {
        [Fact]
        private async void PaymentTypeECL_TestPaymentTypeECL()
        {
            var paymentTypeEdit = await PaymentTypeECL.NewPaymentTypeECL();

            Assert.NotNull(paymentTypeEdit);
            Assert.IsType<PaymentTypeECL>(paymentTypeEdit);
        }


        [Fact]
        private async void PaymentTypeECL_TestGetPaymentTypeECL()
        {
            var childData = MockDb.PaymentTypes;

            var listToTest = await PaymentTypeECL.GetPaymentTypeECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }

        private void BuildPaymentType(PaymentTypeEC paymentType)
        {
            paymentType.Description = "doc type description";
            paymentType.Notes = "document type notes";
        }
    }
}
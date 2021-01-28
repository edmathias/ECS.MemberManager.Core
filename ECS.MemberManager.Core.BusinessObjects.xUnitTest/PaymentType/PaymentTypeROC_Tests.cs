using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentTypeROC_Tests
    {
        [Fact]
        public async void PaymentTypeROC_TestGetChild()
        {
            const int ID_VALUE = 999;

            var buildPaymentType = BuildPaymentType();
            buildPaymentType.Id = ID_VALUE;

            var paymentType = await PaymentTypeROC.GetPaymentTypeROC(buildPaymentType);
            
            Assert.NotNull(paymentType);
            Assert.IsType<PaymentTypeROC>(paymentType);
            Assert.Equal(paymentType.Id, buildPaymentType.Id);
            Assert.Equal(paymentType.Description, buildPaymentType.Description);
            Assert.Equal(paymentType.Notes, buildPaymentType.Notes);
            Assert.Equal(paymentType.RowVersion, buildPaymentType.RowVersion);
        }

        private PaymentType BuildPaymentType()
        {
            var paymentType = new PaymentType();
            paymentType.Id = 1;
            paymentType.Description = "test description";
            paymentType.Notes = "notes for doctype";

            return paymentType;
        }        

    }
}
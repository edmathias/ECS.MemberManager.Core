using System;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentER_Tests 
    {
        public PaymentER_Tests()
        {
            MockDb.ResetMockDb();
        }

        [Fact]
        public async Task TestPaymentER_Get()
        {
            var payment = await PaymentER.GetPayment(1);
 
            Assert.Equal(1, payment.Id);
            Assert.True(payment.IsValid);
        }

        [Fact]
        public async Task TestPaymentER_GetNewObject()
        {
            var payment = await PaymentER.NewPayment();

            Assert.NotNull(payment);
            Assert.False(payment.IsValid);
        }

        [Fact]
        public async Task TestPaymentER_UpdateExistingObjectInDatabase()
        {
            var payment = await PaymentER.GetPayment(1);
            payment.Notes = "These are updated Notes";
            
            var result = payment.Save();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes",result.Notes );
        }

        [Fact]
        public async Task TestPaymentER_InsertNewObjectIntoDatabase()
        {
            var payment = await BuildValidPaymentER();

            var savedPayment = await payment.SaveAsync();

            Assert.NotNull(savedPayment);
            Assert.IsType<PaymentER>(savedPayment);
            Assert.True( savedPayment.Id > 0 );
        }

        [Fact]
        public async Task TestPaymentER_DeleteObjectFromDatabase()
        {
            int beforeCount = MockDb.Payments.Count();

            await PaymentER.DeletePayment(99);
            
            Assert.NotEqual(beforeCount,MockDb.Payments.Count());
        }
        
        // test invalid state 
        [Fact]
        public async Task TestPaymentER_PaymentDateRequired() 
        {
            var payment = await BuildValidPaymentER();
            payment.LastUpdatedBy = "edm";
            payment.LastUpdatedDate = DateTime.Now;
            var isObjectValidInit = payment.IsValid;
            payment.PaymentSource = await PaymentSourceEC.NewPaymentSource();
            payment.PaymentType = await PaymentTypeEC.NewPaymentType();
            payment.PaymentDate = null;
            
            Assert.NotNull(payment);
            Assert.True(isObjectValidInit);
            Assert.False(payment.IsValid);
        }
       
         // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestPaymentER_TestInvalidSave()
        {
            var payment = await PaymentER.NewPayment();
                
            Assert.False(payment.IsValid);
            Assert.Throws<ValidationException>(() => payment.Save() );
        }

        private async Task<PaymentER> BuildValidPaymentER()
        {
            var paymentER = await PaymentER.NewPayment();

            paymentER.Amount = 39.99d;
            paymentER.LastUpdatedBy = "edm";
            paymentER.LastUpdatedDate = DateTime.Now;
            paymentER.Notes = "notes here";
            paymentER.PaymentDate = DateTime.Now;
            paymentER.PaymentExpirationDate = DateTime.Now;
            paymentER.PaymentSource = await PaymentSourceEC.NewPaymentSource();
            paymentER.PaymentSource.Description = "Source 1";
            paymentER.PaymentSource.Notes = "source notes";
            paymentER.PaymentType = await PaymentTypeEC.NewPaymentType();
            paymentER.PaymentType.Description = "type description";
            paymentER.PaymentType.Notes = "notes for type";

            return paymentER;
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;

namespace ECS.MemberManager.Core.BusinessObjects.Test
{
    /// <summary>
    /// Summary description for JustMockTest
    /// </summary>
    [TestClass]
    public class PaymentER_Tests 
    {

        [TestMethod]
        public async Task TestPaymentER_Get()
        {
            var payment = await PaymentER.GetPayment(1);
 
            Assert.AreEqual(payment.Id, 1);
            Assert.IsTrue(payment.IsValid);
        }

        [TestMethod]
        public async Task TestPaymentER_GetNewObject()
        {
            var payment = await PaymentER.NewPayment();

            Assert.IsNotNull(payment);
            Assert.IsFalse(payment.IsValid);
        }

        [TestMethod]
        public async Task TestPaymentER_UpdateExistingObjectInDatabase()
        {
            var payment = await PaymentER.GetPayment(1);
            payment.Notes = "These are updated Notes";
            
            var result = payment.Save();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Notes, "These are updated Notes");
        }

        [TestMethod]
        public async Task TestPaymentER_InsertNewObjectIntoDatabase()
        {
            var payment = await BuildValidPaymentER();

            var savedPayment = await payment.SaveAsync();

            Assert.IsNotNull(savedPayment);
            Assert.IsInstanceOfType(savedPayment, typeof(PaymentER));
            Assert.IsTrue( savedPayment.Id > 0 );
        }

        [TestMethod]
        public async Task TestPaymentER_DeleteObjectFromDatabase()
        {
            int beforeCount = MockDb.Payments.Count();

            await PaymentER.DeletePayment(1);
            
            Assert.AreNotEqual(beforeCount,MockDb.Payments.Count());
        }
        
        // test invalid state 
        [TestMethod]
        public async Task TestPaymentER_DescriptionRequired() 
        {
            var payment = await PaymentER.NewPayment();
            payment.LastUpdatedBy = "edm";
            payment.LastUpdatedDate = DateTime.Now;
            var isObjectValidInit = payment.IsValid;

            Assert.IsNotNull(payment);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(payment.IsValid);
        }
       
         // test exception if attempt to save in invalid state

        [TestMethod]
        public async Task TestPaymentER_TestInvalidSave()
        {
            var payment = await PaymentER.NewPayment();
            PaymentER savedPayment = null;
                
            Assert.IsFalse(payment.IsValid);
            Assert.ThrowsException<ValidationException>(() => savedPayment =  payment.Save() );
        }

        private async Task<PaymentER> BuildValidPaymentER()
        {
            var paymentER = await PaymentER.NewPayment();

            paymentER.Amount = 39.99m;
            paymentER.LastUpdatedBy = "edm";
            paymentER.LastUpdatedDate = DateTime.Now;
            paymentER.Notes = "notes here";
            paymentER.PaymentDate = DateTime.Now;
            paymentER.PaymentExpirationDate = DateTime.Now;
            paymentER.PaymentSource = await PaymentSourceER.NewPaymentSource();
            paymentER.PaymentSource.Description = "Source 1";
            paymentER.PaymentSource.Notes = "source notes";
            paymentER.PaymentType = await PaymentTypeER.NewPaymentType();
            paymentER.PaymentType.Description = "type description";
            paymentER.PaymentType.Notes = "notes for type";

            return paymentER;
        }
    }
}

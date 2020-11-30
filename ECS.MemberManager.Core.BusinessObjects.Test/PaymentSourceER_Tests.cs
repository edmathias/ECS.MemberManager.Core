using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.BusinessObjects.PaymentSource;
using ECS.MemberManager.Core.DataAccess.Mock;

namespace ECS.MemberManager.Core.BusinessObjects.Test
{
    /// <summary>
    /// Summary description for JustMockTest
    /// </summary>
    [TestClass]
    public class PaymentSourceER_Tests 
    {
        [TestMethod]
        public async Task TestPaymentSourceER_Get()
        {
            var paymentSource = await PaymentSourceER.GetPaymentSource(1);

            Assert.AreEqual(paymentSource.Id, 1);
            Assert.IsTrue(paymentSource.IsValid);
        }

        [TestMethod]
        public async Task TestPaymentSourceER_New()
        {
            var paymentSource = await PaymentSourceER.NewPaymentSource();

            Assert.IsNotNull(paymentSource);
            Assert.IsFalse(paymentSource.IsValid);
        }

        [TestMethod]
        public async Task TestPaymentSourceER_Update()
        {
            var paymentSource = await PaymentSourceER.GetPaymentSource(1);
            paymentSource.Notes = "These are updated Notes";
            
            var result = paymentSource.Save();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Notes, "These are updated Notes");
        }

        [TestMethod]
        public async Task TestPaymentSourceER_Insert()
        {
            var paymentSource = await PaymentSourceER.NewPaymentSource();
            paymentSource.Description = "Standby";
            paymentSource.Notes = "This person is on standby";

            var savedPaymentSource = paymentSource.Save();
           
            Assert.IsNotNull(savedPaymentSource);
            Assert.IsInstanceOfType(savedPaymentSource, typeof(PaymentSourceER));
            Assert.IsTrue( savedPaymentSource.Id > 0 );
        }

        [TestMethod]
        public async Task TestPaymentSourceER_Delete()
        {
            int beforeCount = MockDb.PaymentSources.Count();
            
            await PaymentSourceER.DeletePaymentSource(1);
            
            Assert.AreNotEqual(beforeCount,MockDb.PaymentSources.Count());
        }
        
        // test invalid state 
        [TestMethod]
        public async Task TestPaymentSourceER_DescriptionRequired()
        {
            var paymentSource = await PaymentSourceER.NewPaymentSource();
            paymentSource.Description = "make valid";
            var isObjectValidInit = paymentSource.IsValid;
            paymentSource.Description = string.Empty;

            Assert.IsNotNull(paymentSource);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(paymentSource.IsValid);
 
        }
       
        [TestMethod]
        public async Task TestPaymentSourceER_DescriptionExceedsMaxLengthOf255()
        {
            var paymentSource = await PaymentSourceER.NewPaymentSource();
            paymentSource.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.IsNotNull(paymentSource);
            Assert.IsFalse(paymentSource.IsValid);
            Assert.AreEqual(paymentSource.BrokenRulesCollection[0].Description,
                "The field Description must be a string or array type with a maximum length of '255'.");
 
        }        
        // test exception if attempt to save in invalid state

        [TestMethod]
        public async Task TestPaymentSourceER_TestInvalidSave()
        {
            var paymentSource = await PaymentSourceER.NewPaymentSource();
            paymentSource.Description = String.Empty;
            PaymentSourceER savedPaymentSource = null;
            
            Assert.IsFalse(paymentSource.IsValid);
            Assert.ThrowsException<ValidationException>(() => savedPaymentSource =  paymentSource.Save() );
        }
    }
}

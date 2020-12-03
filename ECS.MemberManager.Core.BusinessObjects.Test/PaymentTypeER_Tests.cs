using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.BusinessObjects;
using ECS.MemberManager.Core.DataAccess.Mock;

namespace ECS.MemberManager.Core.BusinessObjects.Test
{
    /// <summary>
    /// Summary description for JustMockTest
    /// </summary>
    [TestClass]
    public class PaymentTypeER_Tests 
    {
        [TestMethod]
        public async Task TestPaymentTypeER_Get()
        {
            var paymentType = await PaymentTypeER.GetPaymentType(1);

            Assert.AreEqual(paymentType.Id, 1);
            Assert.IsTrue(paymentType.IsValid);
        }

        [TestMethod]
        public async Task TestPaymentTypeER_New()
        {
            var paymentType = await PaymentTypeER.NewPaymentType();

            Assert.IsNotNull(paymentType);
            Assert.IsFalse(paymentType.IsValid);
        }

        [TestMethod]
        public async Task TestPaymentTypeER_Update()
        {
            var paymentType = await PaymentTypeER.GetPaymentType(1);
            paymentType.Notes = "These are updated Notes";
            
            var result = paymentType.Save();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Notes, "These are updated Notes");
        }

        [TestMethod]
        public async Task TestPaymentTypeER_Insert()
        {
            var paymentType = await PaymentTypeER.NewPaymentType();
            paymentType.Description = "Standby";
            paymentType.Notes = "This person is on standby";

            var savedPaymentType = paymentType.Save();
           
            Assert.IsNotNull(savedPaymentType);
            Assert.IsInstanceOfType(savedPaymentType, typeof(PaymentTypeER));
            Assert.IsTrue( savedPaymentType.Id > 0 );
        }

        [TestMethod]
        public async Task TestPaymentTypeER_Delete()
        {
            int beforeCount = MockDb.PaymentTypes.Count();
            
            await PaymentTypeER.DeletePaymentType(1);
            
            Assert.AreNotEqual(beforeCount,MockDb.PaymentTypes.Count());
        }
        
        // test invalid state 
        [TestMethod]
        public async Task TestPaymentTypeER_DescriptionRequired()
        {
            var paymentType = await PaymentTypeER.NewPaymentType();
            paymentType.Description = "make valid";
            var isObjectValidInit = paymentType.IsValid;
            paymentType.Description = string.Empty;

            Assert.IsNotNull(paymentType);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(paymentType.IsValid);
 
        }
       
        [TestMethod]
        public async Task TestPaymentTypeER_DescriptionExceedsMaxLengthOf50()
        {
            var paymentType = await PaymentTypeER.NewPaymentType();
            paymentType.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.IsNotNull(paymentType);
            Assert.IsFalse(paymentType.IsValid);
            Assert.AreEqual(paymentType.BrokenRulesCollection[0].Description,
                "The field Description must be a string or array type with a maximum length of '50'.");
 
        }        
        // test exception if attempt to save in invalid state

        [TestMethod]
        public async Task TestPaymentTypeER_TestInvalidSave()
        {
            var paymentType = await PaymentTypeER.NewPaymentType();
            paymentType.Description = String.Empty;
            PaymentTypeER savedPaymentType = null;
            
            Assert.IsFalse(paymentType.IsValid);
            Assert.ThrowsException<ValidationException>(() => savedPaymentType =  paymentType.Save() );
        }
    }
}

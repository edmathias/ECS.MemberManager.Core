using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentTypeER_Tests 
    {
        public PaymentTypeER_Tests()
        {
            MockDb.ResetMockDb();
        }
        
        [Fact]
        public async Task TestPaymentTypeER_Get()
        {
            var paymentType = await PaymentTypeER.GetPaymentType(1);

            Assert.Equal(1, paymentType.Id);
            Assert.True(paymentType.IsValid);
        }

        [Fact]
        public async Task TestPaymentTypeER_New()
        {
            var paymentType = await PaymentTypeER.NewPaymentType();

            Assert.NotNull(paymentType);
            Assert.False(paymentType.IsValid);
        }

        [Fact]
        public async Task TestPaymentTypeER_Update()
        {
            var paymentType = await PaymentTypeER.GetPaymentType(1);
            paymentType.Notes = "These are updated Notes";
            
            var result = paymentType.Save();

            Assert.NotNull(result);
            Assert.Equal( "These are updated Notes",result.Notes);
        }

        [Fact]
        public async Task TestPaymentTypeER_Insert()
        {
            var paymentType = await PaymentTypeER.NewPaymentType();
            paymentType.Description = "Standby";
            paymentType.Notes = "This person is on standby";

            var savedPaymentType = paymentType.Save();
           
            Assert.NotNull(savedPaymentType);
            Assert.IsType<PaymentTypeER>(savedPaymentType);
            Assert.True( savedPaymentType.Id > 0 );
        }

        [Fact]
        public async Task TestPaymentTypeER_Delete()
        {
            int beforeCount = MockDb.PaymentTypes.Count();
            
            await PaymentTypeER.DeletePaymentType(99);
            
            Assert.NotEqual(beforeCount,MockDb.PaymentTypes.Count());
        }
        
        // test invalid state 
        [Fact]
        public async Task TestPaymentTypeER_DescriptionRequired()
        {
            var paymentType = await PaymentTypeER.NewPaymentType();
            paymentType.Description = "make valid";
            var isObjectValidInit = paymentType.IsValid;
            paymentType.Description = string.Empty;

            Assert.NotNull(paymentType);
            Assert.True(isObjectValidInit);
            Assert.False(paymentType.IsValid);
 
        }
       
        [Fact]
        public async Task TestPaymentTypeER_DescriptionExceedsMaxLengthOf50()
        {
            var paymentType = await PaymentTypeER.NewPaymentType();
            paymentType.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(paymentType);
            Assert.False(paymentType.IsValid);
            Assert.Equal("The field Description must be a string or array type with a maximum length of '50'.",
                paymentType.BrokenRulesCollection[0].Description);
 
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestPaymentTypeER_TestInvalidSave()
        {
            var paymentType = await PaymentTypeER.NewPaymentType();
            paymentType.Description = String.Empty;
            
            Assert.False(paymentType.IsValid);
            Assert.Throws<ValidationException>(() => paymentType.Save() );
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentSourceER_Tests 
    {
        public PaymentSourceER_Tests()
        {
            MockDb.ResetMockDb();
        }
        
        [Fact]
        public async Task TestPaymentSourceER_Get()
        {
            var paymentSource = await PaymentSourceER.GetPaymentSource(1);

            Assert.Equal(1, paymentSource.Id);
            Assert.True(paymentSource.IsValid);
        }

        [Fact]
        public async Task TestPaymentSourceER_New()
        {
            var paymentSource = await PaymentSourceER.NewPaymentSource();

            Assert.NotNull(paymentSource);
            Assert.False(paymentSource.IsValid);
        }

        [Fact]
        public async Task TestPaymentSourceER_Update()
        {
            var paymentSource = await PaymentSourceER.GetPaymentSource(1);
            paymentSource.Notes = "These are updated Notes";
            
            var result = paymentSource.Save();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes",result.Notes);
        }

        [Fact]
        public async Task TestPaymentSourceER_Insert()
        {
            var paymentSource = await PaymentSourceER.NewPaymentSource();
            paymentSource.Description = "Standby";
            paymentSource.Notes = "This person is on standby";

            var savedPaymentSource = paymentSource.Save();
           
            Assert.NotNull(savedPaymentSource);
            Assert.IsType<PaymentSourceER>(savedPaymentSource);
            Assert.True( savedPaymentSource.Id > 0 );
        }

        [Fact]
        public async Task TestPaymentSourceER_Delete()
        {
            int beforeCount = MockDb.PaymentSources.Count();
            
            await PaymentSourceER.DeletePaymentSource(99);
            
            Assert.NotEqual(beforeCount,MockDb.PaymentSources.Count());
        }
        
        // test invalid state 
        [Fact]
        public async Task TestPaymentSourceER_DescriptionRequired()
        {
            var paymentSource = await PaymentSourceER.NewPaymentSource();
            paymentSource.Description = "make valid";
            var isObjectValidInit = paymentSource.IsValid;
            paymentSource.Description = string.Empty;

            Assert.NotNull(paymentSource);
            Assert.True(isObjectValidInit);
            Assert.False(paymentSource.IsValid);
 
        }
       
        [Fact]
        public async Task TestPaymentSourceER_DescriptionExceedsMaxLengthOf50()
        {
            var paymentSource = await PaymentSourceER.NewPaymentSource();
            paymentSource.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(paymentSource);
            Assert.False(paymentSource.IsValid);
            Assert.Equal("The field Description must be a string or array type with a maximum length of '50'.",
                paymentSource.BrokenRulesCollection[0].Description);
 
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestPaymentSourceER_TestInvalidSave()
        {
            var paymentSource = await PaymentSourceER.NewPaymentSource();
            paymentSource.Description = String.Empty;
            
            Assert.False(paymentSource.IsValid);
            Assert.Throws<ValidationException>(() =>  paymentSource.Save() );
        }
    }
}

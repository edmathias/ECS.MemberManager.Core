using System;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentSourceEdit_Tests 
    {
        public PaymentSourceEdit_Tests()
        {
            MockDb.ResetMockDb();
        }
        
        [Fact]
        public async Task TestPaymentSourceEdit_Get()
        {
            var paymentSource = await PaymentSourceEdit.GetPaymentSourceEdit(1);

            Assert.Equal(1, paymentSource.Id);
            Assert.True(paymentSource.IsValid);
        }

        [Fact]
        public async Task TestPaymentSourceEdit_New()
        {
            var paymentSource = await PaymentSourceEdit.NewPaymentSourceEdit();

            Assert.NotNull(paymentSource);
            Assert.False(paymentSource.IsValid);
        }

        [Fact]
        public async Task TestPaymentSourceEdit_Update()
        {
            var paymentSource = await PaymentSourceEdit.GetPaymentSourceEdit(1);
            paymentSource.Notes = "These are updated Notes";
            
            var result = await paymentSource.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes",result.Notes);
        }

        [Fact]
        public async Task TestPaymentSourceEdit_Insert()
        {
            var paymentSource = await PaymentSourceEdit.NewPaymentSourceEdit();
            paymentSource.Description = "Standby";
            paymentSource.Notes = "This person is on standby";

            var savedPaymentSource = await paymentSource.SaveAsync();
           
            Assert.NotNull(savedPaymentSource);
            Assert.IsType<PaymentSourceEdit>(savedPaymentSource);
            Assert.True( savedPaymentSource.Id > 0 );
        }

        [Fact]
        public async Task TestPaymentSourceEdit_Delete()
        {
            const int ID_TO_DELETE = 99;
            
            await PaymentSourceEdit.DeletePaymentSourceEdit(ID_TO_DELETE);

            await Assert.ThrowsAsync<DataPortalException>(() => PaymentSourceEdit.GetPaymentSourceEdit(ID_TO_DELETE));
        }
        
        // test invalid state 
        [Fact]
        public async Task TestPaymentSourceEdit_DescriptionRequired()
        {
            var paymentSource = await PaymentSourceEdit.NewPaymentSourceEdit();
            paymentSource.Description = "make valid";
            var isObjectValidInit = paymentSource.IsValid;
            paymentSource.Description = string.Empty;

            Assert.NotNull(paymentSource);
            Assert.True(isObjectValidInit);
            Assert.False(paymentSource.IsValid);
 
        }
       
        [Fact]
        public async Task TestPaymentSourceEdit_DescriptionExceedsMaxLengthOf50()
        {
            var paymentSource = await PaymentSourceEdit.NewPaymentSourceEdit();
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
        public async Task TestPaymentSourceEdit_TestInvalidSave()
        {
            var paymentSource = await PaymentSourceEdit.NewPaymentSourceEdit();
            paymentSource.Description = String.Empty;
            
            Assert.False(paymentSource.IsValid);
            Assert.Throws<ValidationException>(() =>  paymentSource.Save() );
        }
    }
}

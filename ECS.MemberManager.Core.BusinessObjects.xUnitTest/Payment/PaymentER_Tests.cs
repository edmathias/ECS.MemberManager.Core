using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentER_Tests 
    {

        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PaymentER_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");

            if (testLibrary == "Mock")
                MockDb.ResetMockDb();
            else
            {
                if (!IsDatabaseBuilt)
                {
                    var adoDb = new ADODb();
                    adoDb.BuildMemberManagerADODb();
                    IsDatabaseBuilt = true;
                }
            }
        }
        
        [Fact]
        public async Task TestPaymentER_Get()
        {
            var payment = await PaymentER.GetPaymentER(1);
 
            Assert.Equal(1, payment.Id);
            Assert.True(payment.IsValid);
        }

        [Fact]
        public async Task TestPaymentER_GetNewObject()
        {
            var payment = await PaymentER.NewPaymentER();

            Assert.NotNull(payment);
            Assert.False(payment.IsValid);
        }

        [Fact]
        public async Task TestPaymentER_UpdateExistingObjectInDatabase()
        {
            var payment = await PaymentER.GetPaymentER(1);
            payment.Notes = "These are updated Notes";
            
            var result = await payment.SaveAsync();

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
            const int ID_TO_DELETE = 99;
            
            await PaymentER.DeletePaymentER(ID_TO_DELETE);
            
            var paymentToCheck = await Assert.ThrowsAsync<Csla.DataPortalException>
                (() => PaymentER.GetPaymentER(ID_TO_DELETE));        
        }
        
        [Fact]
        public async Task TestPaymentER_TestInvalidSave()
        {
            var payment = await PaymentER.NewPaymentER();
                
            Assert.False(payment.IsValid);
            Assert.Throws<ValidationException>(() => payment.Save() );
        }

        [Fact]
        public async Task TestEMailER_LastUpdatedByRequired()
        {
            var paymentType = await BuildValidPaymentER();
            var isObjectValidInit = paymentType.IsValid;
            paymentType.LastUpdatedBy = string.Empty;

            Assert.NotNull(paymentType);
            Assert.True(isObjectValidInit);
            Assert.False(paymentType.IsValid);
            Assert.Equal("LastUpdatedBy",paymentType.BrokenRulesCollection[0].OriginProperty);
        }

        [Fact]
        public async Task TestEMailER_LastUpdatedByMaxLengthLessThan255()
        {
            var paymentType = await BuildValidPaymentER();
            var isObjectValidInit = paymentType.IsValid;
            paymentType.LastUpdatedBy =  "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                         "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                         "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                         "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(paymentType);
            Assert.True(isObjectValidInit);
            Assert.False(paymentType.IsValid);
            Assert.Equal("LastUpdatedBy",paymentType.BrokenRulesCollection[0].OriginProperty);
            Assert.Equal("LastUpdatedBy can not exceed 255 characters",paymentType.BrokenRulesCollection[0].Description);

        }
          // test exception if attempt to save in invalid state

        private async Task<PaymentER> BuildValidPaymentER()
        {
            var paymentER = await PaymentER.NewPaymentER();

            paymentER.Amount = 39.99;
            paymentER.Person = await PersonEC.GetPersonEC(new Person() {Id = 1});
            paymentER.LastUpdatedBy = "edm";
            paymentER.LastUpdatedDate = DateTime.Now;
            paymentER.Notes = "notes here";
            paymentER.PaymentDate = DateTime.Now;
            paymentER.PaymentExpirationDate = DateTime.Now;
            paymentER.PaymentSource = await PaymentSourceEC.GetPaymentSourceEC( new PaymentSource() {Id = 1});
            paymentER.PaymentSource.Description = "Source 1";
            paymentER.PaymentSource.Notes = "source notes";
            paymentER.PaymentType = await PaymentTypeEC.GetPaymentTypeEC(new PaymentType() {Id = 1});
            paymentER.PaymentType.Description = "type description";
            paymentER.PaymentType.Notes = "notes for type";

            return paymentER;
        }
    }
}

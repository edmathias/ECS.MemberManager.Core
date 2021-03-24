using System;
using System.IO;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentSourceER_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PaymentSourceER_Tests()
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
        public async Task PaymentSourceER_TestGetPaymentSource()
        {
            var paymentSource = await PaymentSourceER.GetPaymentSourceER(1);

            Assert.NotNull(paymentSource);
            Assert.IsType<PaymentSourceER>(paymentSource);
        }

        [Fact]
        public async Task PaymentSourceER_TestGetNewPaymentSourceER()
        {
            var paymentSource = await PaymentSourceER.NewPaymentSourceER();

            Assert.NotNull(paymentSource);
            Assert.False(paymentSource.IsValid);
        }

        [Fact]
        public async Task PaymentSourceER_TestUpdateExistingPaymentSourceER()
        {
            var paymentSource = await PaymentSourceER.GetPaymentSourceER(1);
            paymentSource.Notes = "These are updated Notes";

            var result = await paymentSource.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes", result.Notes);
        }

        [Fact]
        public async Task PaymentSourceER_TestInsertNewPaymentSourceER()
        {
            var paymentSource = await PaymentSourceER.NewPaymentSourceER();
            paymentSource.Description = "Standby";
            paymentSource.Notes = "This person is on standby";

            var savedPaymentSource = await paymentSource.SaveAsync();

            Assert.NotNull(savedPaymentSource);
            Assert.IsType<PaymentSourceER>(savedPaymentSource);
            Assert.True(savedPaymentSource.Id > 0);
        }

        [Fact]
        public async Task PaymentSourceER_TestDeleteObjectFromDatabase()
        {
            const int ID_TO_DELETE = 99;

            await PaymentSourceER.DeletePaymentSourceER(ID_TO_DELETE);

            await Assert.ThrowsAsync<DataPortalException>(() => PaymentSourceER.GetPaymentSourceER(ID_TO_DELETE));
        }

        // test invalid state 
        [Fact]
        public async Task PaymentSourceER_TestDescriptionRequired()
        {
            var paymentSource = await PaymentSourceER.NewPaymentSourceER();
            paymentSource.Description = "make valid";
            var isObjectValidInit = paymentSource.IsValid;
            paymentSource.Description = string.Empty;

            Assert.NotNull(paymentSource);
            Assert.True(isObjectValidInit);
            Assert.False(paymentSource.IsValid);
        }

        [Fact]
        public async Task PaymentSourceER_TestDescriptionExceedsMaxLengthOf50()
        {
            var paymentSource = await PaymentSourceER.NewPaymentSourceER();
            paymentSource.Description = "valid length";
            Assert.True(paymentSource.IsValid);

            paymentSource.Description =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(paymentSource);
            Assert.False(paymentSource.IsValid);
            Assert.Equal("Description", paymentSource.BrokenRulesCollection[0].Property);
            Assert.Equal("Description can not exceed 50 characters",
                paymentSource.BrokenRulesCollection[0].Description);
        }
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task PaymentSourceER_TestInvalidSavePaymentSourceER()
        {
            var paymentSource = await PaymentSourceER.NewPaymentSourceER();
            paymentSource.Description = String.Empty;
            PaymentSourceER savedPaymentSource = null;

            Assert.False(paymentSource.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedPaymentSource = paymentSource.Save());
        }
    }
}
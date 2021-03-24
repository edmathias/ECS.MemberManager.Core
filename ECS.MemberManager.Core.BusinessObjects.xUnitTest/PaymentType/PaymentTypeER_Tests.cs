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
    public class PaymentTypeER_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PaymentTypeER_Tests()
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
        public async Task PaymentTypeER_TestGetPaymentType()
        {
            var paymentType = await PaymentTypeER.GetPaymentTypeER(1);

            Assert.NotNull(paymentType);
            Assert.IsType<PaymentTypeER>(paymentType);
        }

        [Fact]
        public async Task PaymentTypeER_TestGetNewPaymentTypeER()
        {
            var paymentType = await PaymentTypeER.NewPaymentTypeER();

            Assert.NotNull(paymentType);
            Assert.False(paymentType.IsValid);
        }

        [Fact]
        public async Task PaymentTypeER_TestUpdateExistingPaymentTypeER()
        {
            var paymentType = await PaymentTypeER.GetPaymentTypeER(1);
            paymentType.Notes = "These are updated Notes";

            var result = await paymentType.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes", result.Notes);
        }

        [Fact]
        public async Task PaymentTypeER_TestInsertNewPaymentTypeER()
        {
            var paymentType = await PaymentTypeER.NewPaymentTypeER();
            paymentType.Description = "Standby";
            paymentType.Notes = "This person is on standby";

            var savedPaymentType = await paymentType.SaveAsync();

            Assert.NotNull(savedPaymentType);
            Assert.IsType<PaymentTypeER>(savedPaymentType);
            Assert.True(savedPaymentType.Id > 0);
        }

        [Fact]
        public async Task PaymentTypeER_TestDeleteObjectFromDatabase()
        {
            const int ID_TO_DELETE = 99;

            await PaymentTypeER.DeletePaymentTypeER(ID_TO_DELETE);

            await Assert.ThrowsAsync<DataPortalException>(() => PaymentTypeER.GetPaymentTypeER(ID_TO_DELETE));
        }

        // test invalid state 
        [Fact]
        public async Task PaymentTypeER_TestDescriptionRequired()
        {
            var paymentType = await PaymentTypeER.NewPaymentTypeER();
            paymentType.Description = "make valid";
            var isObjectValidInit = paymentType.IsValid;
            paymentType.Description = string.Empty;

            Assert.NotNull(paymentType);
            Assert.True(isObjectValidInit);
            Assert.False(paymentType.IsValid);
            Assert.Equal("Description", paymentType.BrokenRulesCollection[0].Property);
            Assert.Equal("Description required", paymentType.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task PaymentTypeER_TestDescriptionExceedsMaxLengthOf50()
        {
            var paymentType = await PaymentTypeER.NewPaymentTypeER();
            paymentType.Description = "valid length";
            Assert.True(paymentType.IsValid);

            paymentType.Description =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(paymentType);
            Assert.False(paymentType.IsValid);
            Assert.Equal("Description", paymentType.BrokenRulesCollection[0].Property);
            Assert.Equal("Description can not exceed 50 characters", paymentType.BrokenRulesCollection[0].Description);
        }
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task PaymentTypeER_TestInvalidSavePaymentTypeER()
        {
            var paymentType = await PaymentTypeER.NewPaymentTypeER();
            paymentType.Description = String.Empty;
            PaymentTypeER savedPaymentType = null;

            Assert.False(paymentType.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedPaymentType = paymentType.Save());
        }
    }
}
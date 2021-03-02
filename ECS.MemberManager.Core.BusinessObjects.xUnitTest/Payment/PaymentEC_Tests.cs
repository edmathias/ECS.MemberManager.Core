using System;
using System.Data;
using System.IO;
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
    public class PaymentEC_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PaymentEC_Tests()
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
        public async Task PaymentEC_Get()
        {
            var organization = await PaymentEC.GetPaymentEC(BuildValidPayment());

            Assert.NotNull(organization);
            Assert.IsType<PaymentEC>(organization);
            Assert.Equal(1, organization.Id);
            Assert.True(organization.IsValid);
        }

        [Fact]
        public async Task PaymentEC_New()
        {
            var organization = await PaymentEC.NewPaymentEC();

            Assert.NotNull(organization);
            Assert.False(organization.IsValid);
        }

        [Fact]
        public async Task TestPaymentER_LastUpdatedByRequired()
        {
            var paymentType = await PaymentEC.NewPaymentEC();
            await BuildValidPaymentEC(paymentType);
            var isObjectValidInit = paymentType.IsValid;
            paymentType.LastUpdatedBy = string.Empty;

            Assert.NotNull(paymentType);
            Assert.True(isObjectValidInit);
            Assert.False(paymentType.IsValid);
            Assert.Equal("LastUpdatedBy",paymentType.BrokenRulesCollection[0].OriginProperty);
        }

        [Fact]
        public async Task TestPaymentER_LastUpdatedByMaxLengthLessThan255()
        {
            var paymentType = await PaymentEC.NewPaymentEC();
            await BuildValidPaymentEC(paymentType);
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

        private async Task BuildValidPaymentEC(PaymentEC payment)
        {
            payment.Amount = 39.99;
            payment.Person = await PersonEC.GetPersonEC(new Person() {Id = 1});
            payment.LastUpdatedBy = "edm";
            payment.LastUpdatedDate = DateTime.Now;
            payment.Notes = "notes here";
            payment.PaymentDate = DateTime.Now;
            payment.PaymentExpirationDate = DateTime.Now;
            payment.PaymentSource = await PaymentSourceEC.GetPaymentSourceEC(new PaymentSource() {Id = 1});
            payment.PaymentType = await PaymentTypeEC.GetPaymentTypeEC(new PaymentType() {Id = 1});
        }

        private Payment BuildValidPayment()
        {
            var payment = new Payment();
            payment.Id = 1;
            payment.Amount = 39.99;
            payment.Person = new Person() {Id = 1};
            payment.LastUpdatedBy = "edm";
            payment.LastUpdatedDate = DateTime.Now;
            payment.Notes = "notes here";
            payment.PaymentDate = DateTime.Now;
            payment.PaymentExpirationDate = DateTime.Now;
            payment.PaymentSource = new PaymentSource() {Id = 1};
            payment.PaymentType = new PaymentType() {Id = 1};

            return payment;
        }
    }
}
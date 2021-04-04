using System;
using System.IO;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentROC_Tests : CslaBaseTest
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PaymentROC_Tests()
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
        public async Task PaymentROC_Get()
        {
            var validPayment = BuildValidPayment();
            var payment = await PaymentROC.GetPaymentROC(validPayment);

            Assert.NotNull(payment);
            Assert.IsType<PaymentROC>(payment);
            Assert.Equal(1, payment.Id);
            Assert.Equal(validPayment.Amount, payment.Amount);
            Assert.Equal(validPayment.Person.Id, payment.Person.Id);
            Assert.Equal(validPayment.LastUpdatedBy, payment.LastUpdatedBy);
            Assert.Equal(new SmartDate(validPayment.LastUpdatedDate), payment.LastUpdatedDate);
            Assert.Equal(validPayment.Notes, payment.Notes);
            Assert.Equal(new SmartDate(validPayment.PaymentDate), payment.PaymentDate);
            Assert.Equal(new SmartDate(validPayment.PaymentExpirationDate), payment.PaymentExpirationDate);
            Assert.Equal(validPayment.PaymentSource.Id, payment.PaymentSource.Id);
            Assert.Equal(validPayment.PaymentType.Id, payment.PaymentType.Id);
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
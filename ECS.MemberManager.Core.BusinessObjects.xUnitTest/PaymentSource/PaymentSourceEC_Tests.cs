using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentSourceEC_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PaymentSourceEC_Tests()
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
        public async Task TestPaymentSourceEC_NewPaymentSourceEC()
        {
            var paymentSource = await PaymentSourceEC.NewPaymentSourceEC();

            Assert.NotNull(paymentSource);
            Assert.IsType<PaymentSourceEC>(paymentSource);
            Assert.False(paymentSource.IsValid);
        }
        
        [Fact]
        public async Task TestPaymentSourceEC_GetPaymentSourceEC()
        {
            var paymentSourceToLoad = BuildPaymentSource();
            var paymentSource = await PaymentSourceEC.GetPaymentSourceEC(paymentSourceToLoad);

            Assert.NotNull(paymentSource);
            Assert.IsType<PaymentSourceEC>(paymentSource);
            Assert.Equal(paymentSourceToLoad.Id,paymentSource.Id);
            Assert.Equal(paymentSourceToLoad.Description,paymentSource.Description);
            Assert.Equal(paymentSourceToLoad.Notes, paymentSource.Notes);
            Assert.Equal(paymentSourceToLoad.RowVersion, paymentSource.RowVersion);
            Assert.True(paymentSource.IsValid);
        }

        [Fact]
        public async Task TestPaymentSourceEC_DescriptionRequired()
        {
            var paymentSourceToTest = BuildPaymentSource();
            var paymentSource = await PaymentSourceEC.GetPaymentSourceEC(paymentSourceToTest);
            var isObjectValidInit = paymentSource.IsValid;
            paymentSource.Description = string.Empty;

            Assert.NotNull(paymentSource);
            Assert.True(isObjectValidInit);
            Assert.False(paymentSource.IsValid);
            Assert.Equal("Description",paymentSource.BrokenRulesCollection[0].Property);
        }

        [Fact]
        public async Task TestPaymentSourceEC_DescriptionLessThan50Chars()
        {
            var paymentSourceToTest = BuildPaymentSource();
            var paymentSource = await PaymentSourceEC.GetPaymentSourceEC(paymentSourceToTest);
            var isObjectValidInit = paymentSource.IsValid;
            paymentSource.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(paymentSource);
            Assert.True(isObjectValidInit);
            Assert.False(paymentSource.IsValid);
            Assert.Equal("Description",paymentSource.BrokenRulesCollection[0].Property);
        }

        private PaymentSource BuildPaymentSource()
        {
            var paymentSource = new PaymentSource();
            paymentSource.Id = 1;
            paymentSource.Description = "test description";
            paymentSource.Notes = "notes for doctype";

            return paymentSource;
        }        
    }
}
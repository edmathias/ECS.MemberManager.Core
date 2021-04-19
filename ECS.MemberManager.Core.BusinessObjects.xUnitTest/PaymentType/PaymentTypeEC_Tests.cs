using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentTypeEC_Tests : CslaBaseTest
    {
        [Fact]
        public async Task TestPaymentTypeEC_NewPaymentTypeEC()
        {
            var paymentType = await PaymentTypeEC.NewPaymentTypeEC();

            Assert.NotNull(paymentType);
            Assert.IsType<PaymentTypeEC>(paymentType);
            Assert.False(paymentType.IsValid);
        }

        [Fact]
        public async Task TestPaymentTypeEC_GetPaymentTypeEC()
        {
            var paymentTypeToLoad = BuildPaymentType();
            var paymentType = await PaymentTypeEC.GetPaymentTypeEC(paymentTypeToLoad);

            Assert.NotNull(paymentType);
            Assert.IsType<PaymentTypeEC>(paymentType);
            Assert.Equal(paymentTypeToLoad.Id, paymentType.Id);
            Assert.Equal(paymentTypeToLoad.Description, paymentType.Description);
            Assert.Equal(paymentTypeToLoad.Notes, paymentType.Notes);
            Assert.Equal(paymentTypeToLoad.RowVersion, paymentType.RowVersion);
            Assert.True(paymentType.IsValid);
        }

        [Fact]
        public async Task TestPaymentTypeEC_DescriptionRequired()
        {
            var paymentTypeToTest = BuildPaymentType();
            var paymentType = await PaymentTypeEC.GetPaymentTypeEC(paymentTypeToTest);
            var isObjectValidInit = paymentType.IsValid;
            paymentType.Description = string.Empty;

            Assert.NotNull(paymentType);
            Assert.True(isObjectValidInit);
            Assert.False(paymentType.IsValid);
            Assert.Equal("Description", paymentType.BrokenRulesCollection[0].Property);
        }

        [Fact]
        public async Task TestPaymentTypeEC_DescriptionLessThan50Chars()
        {
            var paymentTypeToTest = BuildPaymentType();
            var paymentType = await PaymentTypeEC.GetPaymentTypeEC(paymentTypeToTest);
            var isObjectValidInit = paymentType.IsValid;
            paymentType.Description =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(paymentType);
            Assert.True(isObjectValidInit);
            Assert.False(paymentType.IsValid);
            Assert.Equal("Description", paymentType.BrokenRulesCollection[0].Property);
        }

        private PaymentType BuildPaymentType()
        {
            var paymentType = new PaymentType();
            paymentType.Id = 1;
            paymentType.Description = "test description";
            paymentType.Notes = "notes for doctype";

            return paymentType;
        }
    }
}
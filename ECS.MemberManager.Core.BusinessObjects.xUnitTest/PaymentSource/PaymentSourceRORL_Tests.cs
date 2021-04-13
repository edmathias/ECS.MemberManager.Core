using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentSourceRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void PaymentSourceRORL_TestGetPaymentSourceRORL()
        {
            var paymentSourceTypeInfoList = await PaymentSourceRORL.GetPaymentSourceRORL();

            Assert.NotNull(paymentSourceTypeInfoList);
            Assert.True(paymentSourceTypeInfoList.IsReadOnly);
            Assert.Equal(3, paymentSourceTypeInfoList.Count);
        }
    }
}
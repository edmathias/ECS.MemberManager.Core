using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentTypeRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void PaymentTypeRORL_TestGetPaymentTypeRORL()
        {
            var paymentTypeTypeInfoList = await PaymentTypeRORL.GetPaymentTypeRORL();

            Assert.NotNull(paymentTypeTypeInfoList);
            Assert.True(paymentTypeTypeInfoList.IsReadOnly);
            Assert.Equal(3, paymentTypeTypeInfoList.Count);
        }
    }
}
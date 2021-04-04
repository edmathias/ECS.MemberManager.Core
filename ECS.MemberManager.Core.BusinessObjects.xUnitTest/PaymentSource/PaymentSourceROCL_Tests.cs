using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentSourceROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void PaymentSourceInfoList_TestGetPaymentSourceInfoList()
        {
            var childData = MockDb.PaymentSources;

            var paymentSourceInfoList = await PaymentSourceROCL.GetPaymentSourceROCL(childData);

            Assert.NotNull(paymentSourceInfoList);
            Assert.True(paymentSourceInfoList.IsReadOnly);
            Assert.Equal(3, paymentSourceInfoList.Count);
        }
    }
}
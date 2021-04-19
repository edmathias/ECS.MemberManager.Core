using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentTypeROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void PaymentTypeInfoList_TestGetPaymentTypeInfoList()
        {
            var childData = MockDb.PaymentTypes;

            var paymentTypeInfoList = await PaymentTypeROCL.GetPaymentTypeROCL(childData);

            Assert.NotNull(paymentTypeInfoList);
            Assert.True(paymentTypeInfoList.IsReadOnly);
            Assert.Equal(3, paymentTypeInfoList.Count);
        }
    }
}
using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentTypeROR_Tests
    {

        [Fact]
        public async void PaymentTypeROR_TestGetById()
        {
            var category = await PaymentTypeROR.GetPaymentTypeROR(1);
            
            Assert.NotNull(category);
            Assert.IsType<PaymentTypeROR>(category);
            Assert.Equal(1, category.Id);
        }
    }
}
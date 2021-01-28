using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentSourceROR_Tests
    {

        [Fact]
        public async void PaymentSourceROR_TestGetById()
        {
            var category = await PaymentSourceROR.GetPaymentSourceROR(1);
            
            Assert.NotNull(category);
            Assert.IsType<PaymentSourceROR>(category);
            Assert.Equal(1, category.Id);
        }
    }
}
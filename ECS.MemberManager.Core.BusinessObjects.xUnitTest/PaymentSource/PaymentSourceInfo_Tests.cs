using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentSourceInfo_Tests
    {
        [Fact]
        public async void PaymentSourceInfo_TestGetById()
        {
            var organizationTypeInfo = await PaymentSourceInfo.GetPaymentSourceInfo(1);
            
            Assert.NotNull(organizationTypeInfo);
            Assert.IsType<PaymentSourceInfo>(organizationTypeInfo);
            Assert.Equal(1, organizationTypeInfo.Id);
        }

        [Fact]
        public async void PaymentSourceInfo_TestGetChild()
        {
            const int ID_VALUE = 999;
            
            var organizationType = new PaymentSource()
            {
                Id = ID_VALUE,
                Description = "organization type description",
                Notes = "organization type notes",
            };

            var organizationTypeInfo = await PaymentSourceInfo.GetPaymentSourceInfo(organizationType);
            
            Assert.NotNull(organizationTypeInfo);
            Assert.IsType<PaymentSourceInfo>(organizationTypeInfo);
            Assert.Equal(ID_VALUE, organizationTypeInfo.Id);
            Assert.Equal("organization type description",organizationTypeInfo.Description);
            Assert.Equal("organization type notes",organizationTypeInfo.Notes);
        }
    }
}
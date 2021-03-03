using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PhoneROR_Tests
    {

        [Fact]
        public async void PhoneROR_TestGetById()
        {
            var phone = await PhoneROR.GetPhoneROR(1);
            
            Assert.NotNull(phone);
            Assert.IsType<PhoneROR>(phone);
            Assert.Equal(1, phone.Id);
        }
    }
}
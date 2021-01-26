using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class AddressROR_Tests
    {

        [Fact]
        public async void AddressROR_TestGetById()
        {
            var addressInfo = await AddressROR.GetAddressROR(1);
            
            Assert.NotNull(addressInfo);
            
            Assert.IsType<AddressROR>(addressInfo);
            Assert.Equal(1, addressInfo.Id);
        }

    }
}
using System;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class AddressERL_Tests
    {
        [Fact]
        private async void TestAddressERL_GetAddressList()
        {
            var listToTest = MockDb.Addresses;
            
            var addressErl = await AddressERL.GetAddressList(listToTest);

            Assert.NotNull(addressErl);
            Assert.Equal(MockDb.Addresses.Count, addressErl.Count);
        }
    }
}
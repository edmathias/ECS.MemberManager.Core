using System;
using System.Linq;
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
        
        [Fact]
        private async void TestAddressERL_DeleteAddressEntry()
        {
            var listToTest = MockDb.Addresses;
            var idToDelete = MockDb.Addresses.Max(a => a.Id);
            
            var addressErl = await AddressERL.GetAddressList(listToTest);

            var address = addressErl.First(a => a.Id == idToDelete);

            // remove is deferred delete
            addressErl.Remove(address); 

            var addressListAfterDelete = await addressErl.SaveAsync();
            
            Assert.NotNull(addressListAfterDelete);
        }
        
    }
}
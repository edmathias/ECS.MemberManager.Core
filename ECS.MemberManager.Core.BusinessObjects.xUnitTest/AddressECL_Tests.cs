using System;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class AddressECL_Tests
    {
        [Fact]
        private async void TestAddressECL_GetAddressList()
        {
            var listToTest = MockDb.Addresses;
            
            var addressEcl = await AddressECL.GetAddressList(listToTest);

            Assert.NotNull(addressEcl);
            Assert.Equal(3,addressEcl.Count);

        }
    }
}
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class AddressROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void AddressROCL_TestGetAddressROCL()
        {
            var addresses = MockDb.Addresses;

            var addressTypeInfoList = await AddressROCL.GetAddressROCL(addresses);

            Assert.NotNull(addressTypeInfoList);
            Assert.True(addressTypeInfoList.IsReadOnly);
            Assert.Equal(3, addressTypeInfoList.Count);
        }
    }
}
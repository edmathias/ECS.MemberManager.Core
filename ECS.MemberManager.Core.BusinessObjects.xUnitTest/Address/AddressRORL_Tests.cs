using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class AddressRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void AddressRORL_TestGetAddressRORL()
        {
            var addressTypeInfoList = await AddressRORL.GetAddressRORL();

            Assert.NotNull(addressTypeInfoList);
            Assert.True(addressTypeInfoList.IsReadOnly);
            Assert.Equal(3, addressTypeInfoList.Count);
        }
    }
}
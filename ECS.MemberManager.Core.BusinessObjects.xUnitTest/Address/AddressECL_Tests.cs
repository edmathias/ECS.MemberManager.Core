using System;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class AddressECL_Tests : CslaBaseTest
    {
        [Fact]
        private async void AddressECL_TestNewAddressECL()
        {
            var addressErl = await AddressECL.NewAddressECL();

            Assert.NotNull(addressErl);
            Assert.IsType<AddressECL>(addressErl);
        }

        [Fact]
        private async void AddressECL_TestGetAddressECL()
        {
            var addresses = MockDb.Addresses;
            var addressECL = await AddressECL.GetAddressECL(addresses);

            Assert.NotNull(addressECL);
            Assert.Equal(3, addressECL.Count);
        }


        private void BuildValidAddress(AddressEC address)
        {
            address.Address1 = "8365 Gildersleeve Lane";
            address.Address2 = "Unit 103";
            address.City = "Kirtland";
            address.State = "OH";
            address.PostCode = "44094";
            address.Notes = "address notes";
            address.LastUpdatedBy = "edm";
            address.LastUpdatedDate = DateTime.Now;
        }
    }
}
using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class AddressInfoTests
    {

        [Fact]
        public async void AddressInfo_TestGetById()
        {
            var addressInfo = await AddressInfo.GetAddressInfo(1);
            
            Assert.NotNull(addressInfo);
            Assert.IsType<AddressInfo>(addressInfo);
            Assert.Equal(1, addressInfo.Id);
        }

        [Fact]
        public async void AddressInfo_TestGetChild()
        {
            const int ID_VALUE = 999;
            
            var addressType = new Address()
            {
                Id = ID_VALUE,
                Address1 = "5514 Oriole Lane",
                Address2 = "Apt B",
                City = "Greendale",
                State = "WI",
                PostCode = "53129",
                LastUpdatedBy = "edm",
                LastUpdatedDate = DateTime.Now,
                Notes = "address type notes"
            };

            var addressTypeInfo = await AddressInfo.GetAddressInfo(addressType);
            
            Assert.NotNull(addressTypeInfo);
            Assert.IsType<AddressInfo>(addressTypeInfo);
            Assert.Equal(ID_VALUE, addressTypeInfo.Id);

        }
    }
}
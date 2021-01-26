using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class AddressInfoChild_Tests
    {

        [Fact]
        public async void AddressInfoChild_TestGetChild()
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

            var addressTypeInfo = await AddressInfoChild.GetAddressInfoChild(addressType);
            
            Assert.NotNull(addressTypeInfo);
            Assert.IsType<AddressInfoChild>(addressTypeInfo);
            Assert.Equal(addressType.Id, addressTypeInfo.Id);
            Assert.Equal(addressType.Address1, addressTypeInfo.Address1);
            Assert.Equal(addressType.Address2, addressTypeInfo.Address2);
            Assert.Equal(addressType.City, addressTypeInfo.City);
            Assert.Equal(addressType.State, addressTypeInfo.State);
            Assert.Equal(addressType.PostCode, addressTypeInfo.PostCode);
            Assert.Equal(addressType.Notes, addressTypeInfo.Notes);
            Assert.Equal(addressType.LastUpdatedBy,addressTypeInfo.LastUpdatedBy);
            Assert.Equal(new SmartDate(addressType.LastUpdatedDate),addressTypeInfo.LastUpdatedDate);
        }
    }
}
using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class AddressROC_Tests
    {

        [Fact]
        public async void AddressROC_TestGetById()
        {
            var addressToTest = BuildAddress();
            var address = await AddressROC.GetAddressROC(addressToTest);
            
            Assert.NotNull(address);
            Assert.IsType<AddressROC>(address);
            Assert.Equal(addressToTest.Id,address.Id);
            Assert.Equal(addressToTest.Address1,address.Address1);
            Assert.Equal(addressToTest.Address2,address.Address2);
            Assert.Equal(addressToTest.City,address.City);
            Assert.Equal(addressToTest.State,address.State);
            Assert.Equal(addressToTest.PostCode,address.PostCode);
            Assert.Equal(addressToTest.Notes,address.Notes);
            Assert.Equal(addressToTest.LastUpdatedBy,address.LastUpdatedBy);
            Assert.Equal(new SmartDate(addressToTest.LastUpdatedDate),address.LastUpdatedDate);
            
        }

        private Address BuildAddress()
        {
            var address = new Address()
            {
                Address1 = "Address line 1",
                Address2 = "Address line 2",
                City = "City",
                State = "CO",
                PostCode = "80111",
                LastUpdatedDate = DateTime.Now,
                LastUpdatedBy = "edm",
                Notes = "This person is on standby"
            };

            return address;
        }        
    }
}
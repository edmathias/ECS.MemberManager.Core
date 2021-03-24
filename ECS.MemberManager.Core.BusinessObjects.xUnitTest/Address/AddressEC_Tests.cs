using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class AddressEC_Tests : CslaBaseTest
    {
        [Fact]
        public async Task TestAddressEC_GetNewAddressEC()
        {
            var addressToLoad = BuildAddress();
            var address = await AddressEC.GetAddressEC(addressToLoad);

            Assert.NotNull(address);
            Assert.IsType<AddressEC>(address);
            Assert.Equal(addressToLoad.Id, address.Id);
            Assert.Equal(addressToLoad.Address1, address.Address1);
            Assert.Equal(addressToLoad.Address2, address.Address2);
            Assert.Equal(addressToLoad.City, address.City);
            Assert.Equal(addressToLoad.State, address.State);
            Assert.Equal(addressToLoad.PostCode, address.PostCode);
            Assert.Equal(addressToLoad.Notes, address.Notes);
            Assert.Equal(addressToLoad.LastUpdatedBy, address.LastUpdatedBy);
            Assert.Equal(new SmartDate(addressToLoad.LastUpdatedDate), address.LastUpdatedDate);
            Assert.True(address.IsValid);
        }

        [Fact]
        public async Task TestAddressEC_NewAddressEC()
        {
            var Address = await AddressEC.NewAddressEC();

            Assert.NotNull(Address);
            Assert.False(Address.IsValid);
        }

        // test invalid state 
        [Fact]
        public async Task TestAddressEC_Address1Required()
        {
            var addressToTest = BuildAddress();
            var address = await AddressEC.GetAddressEC(addressToTest);
            var isObjectValidInit = address.IsValid;
            address.Address1 = string.Empty;

            Assert.NotNull(address);
            Assert.True(isObjectValidInit);
            Assert.False(address.IsValid);
            Assert.Equal("Address1", address.BrokenRulesCollection[0].Property);
            Assert.Equal("Address1 required", address.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressEC_CityRequired()
        {
            var addressToTest = BuildAddress();
            var address = await AddressEC.GetAddressEC(addressToTest);
            var isObjectValidInit = address.IsValid;
            address.City = string.Empty;

            Assert.NotNull(address);
            Assert.True(isObjectValidInit);
            Assert.False(address.IsValid);
            Assert.Equal("City", address.BrokenRulesCollection[0].Property);
            Assert.Equal("City required", address.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressEC_StateRequired()
        {
            var addressToTest = BuildAddress();
            var address = await AddressEC.GetAddressEC(addressToTest);
            var isObjectValidInit = address.IsValid;
            address.State = string.Empty;

            Assert.NotNull(address);
            Assert.True(isObjectValidInit);
            Assert.False(address.IsValid);
            Assert.Equal("State", address.BrokenRulesCollection[0].Property);
            Assert.Equal("State required", address.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressEC_PostCodeRequired()
        {
            var addressToTest = BuildAddress();
            var address = await AddressEC.GetAddressEC(addressToTest);
            var isObjectValidInit = address.IsValid;
            address.PostCode = string.Empty;

            Assert.NotNull(address);
            Assert.True(isObjectValidInit);
            Assert.False(address.IsValid);
            Assert.Equal("PostCode", address.BrokenRulesCollection[0].Property);
            Assert.Equal("PostCode required", address.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressEC_LastUpdatedByRequired()
        {
            var addressToTest = BuildAddress();
            var address = await AddressEC.GetAddressEC(addressToTest);
            var isObjectValidInit = address.IsValid;
            address.LastUpdatedDate = DateTime.MinValue;

            Assert.NotNull(address);
            Assert.True(isObjectValidInit);
            Assert.False(address.IsValid);
            Assert.Equal("LastUpdatedDate", address.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedDate required", address.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressEC_LastUpdatedDateRequired()
        {
            var addressToTest = BuildAddress();
            var address = await AddressEC.GetAddressEC(addressToTest);
            var isObjectValidInit = address.IsValid;
            address.LastUpdatedDate = DateTime.MinValue;

            Assert.NotNull(address);
            Assert.True(isObjectValidInit);
            Assert.False(address.IsValid);
            Assert.Equal("LastUpdatedDate", address.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedDate required", address.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressEC_Address1ExceedsMaxLengthOf35()
        {
            var addressToTest = BuildAddress();
            var address = await AddressEC.GetAddressEC(addressToTest);
            var isObjectValidInit = address.IsValid;
            address.Address1 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                               "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                               "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                               "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(address);
            Assert.True(isObjectValidInit);
            Assert.False(address.IsValid);
            Assert.Equal("Address1", address.BrokenRulesCollection[0].Property);
            Assert.Equal("Address1 can not exceed 35 characters", address.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressEC_Address2ExceedsMaxLengthOf35()
        {
            var addressToTest = BuildAddress();
            var address = await AddressEC.GetAddressEC(addressToTest);
            var isObjectValidInit = address.IsValid;
            address.Address2 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                               "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                               "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                               "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(address);
            Assert.True(isObjectValidInit);
            Assert.False(address.IsValid);
            Assert.Equal("Address2", address.BrokenRulesCollection[0].Property);
            Assert.Equal("Address2 can not exceed 35 characters", address.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressEC_CityExceedsMaxLengthOf50()
        {
            var addressToTest = BuildAddress();
            var address = await AddressEC.GetAddressEC(addressToTest);
            var isObjectValidInit = address.IsValid;
            address.City = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                           "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                           "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                           "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(address);
            Assert.True(isObjectValidInit);
            Assert.False(address.IsValid);
            Assert.Equal("City", address.BrokenRulesCollection[0].Property);
            Assert.Equal("City can not exceed 50 characters", address.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressEC_StateExceedsMaxLengthOf2()
        {
            var addressToTest = BuildAddress();
            var address = await AddressEC.GetAddressEC(addressToTest);
            var isObjectValidInit = address.IsValid;
            address.State = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor ";

            Assert.NotNull(address);
            Assert.True(isObjectValidInit);
            Assert.False(address.IsValid);
            Assert.Equal("State", address.BrokenRulesCollection[0].Property);
            Assert.Equal("State can not exceed 2 characters", address.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressEC_PostCodeExceedsMaxLengthOf9()
        {
            var addressToTest = BuildAddress();
            var address = await AddressEC.GetAddressEC(addressToTest);
            var isObjectValidInit = address.IsValid;
            address.PostCode = "Lorem ipsum dolor sit amet";

            Assert.NotNull(address);
            Assert.True(isObjectValidInit);
            Assert.False(address.IsValid);
            Assert.Equal("PostCode", address.BrokenRulesCollection[0].Property);
            Assert.Equal("PostCode can not exceed 9 characters", address.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressEC_NotesExceedsMaxLengthOf255()
        {
            var addressToTest = BuildAddress();
            var address = await AddressEC.GetAddressEC(addressToTest);
            var isObjectValidInit = address.IsValid;
            address.Notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                            "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                            "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                            "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(address);
            Assert.True(isObjectValidInit);
            Assert.False(address.IsValid);
            Assert.Equal("Notes", address.BrokenRulesCollection[0].Property);
            Assert.Equal("Notes can not exceed 255 characters", address.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressEC_LastUpdatedByExceedsMaxLengthOf255()
        {
            var addressToTest = BuildAddress();
            var address = await AddressEC.GetAddressEC(addressToTest);
            var isObjectValidInit = address.IsValid;
            address.LastUpdatedBy = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                    "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                    "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                    "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(address);
            Assert.True(isObjectValidInit);
            Assert.False(address.IsValid);
            Assert.Equal("LastUpdatedBy", address.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy can not exceed 255 characters", address.BrokenRulesCollection[0].Description);
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
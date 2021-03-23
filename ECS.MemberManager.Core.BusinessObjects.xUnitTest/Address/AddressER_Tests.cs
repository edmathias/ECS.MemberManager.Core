using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.EF;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class AddressER_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;
        private readonly ITestOutputHelper _testOutputHelper;

        public AddressER_Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");

            if (testLibrary == "Mock")
            {
                MockDb.ResetMockDb();
            }
            else if (testLibrary == "ADO")
            {
                if (!IsDatabaseBuilt)
                {
                    var adoDb = new ADODb();
                    adoDb.BuildMemberManagerADODb();
                }
            }
            else if (testLibrary == "EF")
            {
                if (!IsDatabaseBuilt)
                {
                    var efDb = new EFDb();
                    efDb.BuildMemberManagerEFDb();
                }
            }

            IsDatabaseBuilt = true;
        }

        [Fact]
        public async Task TestAddressER_Get()
        {
            var address = await AddressER.GetAddressER(1);

            Assert.NotNull(address);
            Assert.IsType<AddressER>(address);
            Assert.Equal(1, address.Id);
            Assert.True(address.IsValid);
        }

        [Fact]
        public async Task TestAddressER_New()
        {
            var Address = await AddressER.NewAddressER();

            Assert.NotNull(Address);
            Assert.False(Address.IsValid);
        }

        [Fact]
        public async Task TestAddressER_Update()
        {
            var address = await AddressER.GetAddressER(1);
            address.Notes = "These are updated Notes";

            var result = await address.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes", result.Notes);
        }

        [Fact]
        public async Task TestAddressER_Insert()
        {
            var address = await AddressER.NewAddressER();
            BuildAddress(address);

            var savedAddress = await address.SaveAsync();

            Assert.NotNull(savedAddress);
            Assert.IsType<AddressER>(savedAddress);
            Assert.True(savedAddress.Id > 0);
        }

        [Fact]
        public async Task TestAddressER_Delete()
        {
            var addressToDelete = await AddressER.GetAddressER(99);

            await AddressER.DeleteAddressER(addressToDelete.Id);

            var addressToCheck = await Assert.ThrowsAsync<Csla.DataPortalException>
                (() => AddressER.GetAddressER(addressToDelete.Id));
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
            address.LastUpdatedBy = string.Empty;

            Assert.NotNull(address);
            Assert.True(isObjectValidInit);
            Assert.False(address.IsValid);
            Assert.Equal("LastUpdatedBy", address.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy required", address.BrokenRulesCollection[0].Description);
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

        [Fact]
        public async Task TestAddressER_InvalidGet()
        {
            await Assert.ThrowsAsync<DataPortalException>(() => AddressER.GetAddressER(999));
        }

        void BuildAddress(AddressER addressER)
        {
            addressER.Address1 = "Address line 1";
            addressER.Address2 = "Address line 2";
            addressER.City = "City";
            addressER.State = "CO";
            addressER.PostCode = "80111";
            addressER.LastUpdatedDate = DateTime.Now;
            addressER.LastUpdatedBy = "edm";
            addressER.Notes = "This person is on standby";
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
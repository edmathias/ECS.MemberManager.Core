﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class AddressEC_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;
        private readonly ITestOutputHelper _testOutputHelper;
        public AddressEC_Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");
            
            if(testLibrary == "Mock")
                MockDb.ResetMockDb();
            else
            {
                if (!IsDatabaseBuilt)
                {
                    var adoDb = new ADODb();
                    adoDb.BuildMemberManagerADODb();
                    IsDatabaseBuilt = true;
                }
            }
        }
        
        [Fact]
        public async Task TestAddressEC_GetNewAddressEC()
        {
            var addressToLoad = BuildAddress();
            var address = await AddressEC.GetAddressEC(addressToLoad);

            Assert.NotNull(address);
            Assert.IsType<AddressEC>(address);
            Assert.Equal(addressToLoad.Id,address.Id);
            Assert.Equal(addressToLoad.Address1,address.Address1);
            Assert.Equal(addressToLoad.Address2,address.Address2);
            Assert.Equal(addressToLoad.City,address.City);
            Assert.Equal(addressToLoad.State,address.State);
            Assert.Equal(addressToLoad.PostCode,address.PostCode);
            Assert.Equal(addressToLoad.Notes,address.Notes);
            Assert.Equal(addressToLoad.LastUpdatedBy,address.LastUpdatedBy);
            Assert.Equal(new SmartDate(addressToLoad.LastUpdatedDate),address.LastUpdatedDate);
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
            Assert.Equal("Address1",address.BrokenRulesCollection[0].Property);
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
            Assert.Equal("City",address.BrokenRulesCollection[0].Property);
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
            Assert.Equal("State",address.BrokenRulesCollection[0].Property);
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
            Assert.Equal("PostCode",address.BrokenRulesCollection[0].Property);
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
            Assert.Equal("Address1",address.BrokenRulesCollection[0].Property);
            Assert.Equal("The field Address1 must be a string or array type with a maximum length of '35'.",address.BrokenRulesCollection[0].Description);
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
            Assert.Equal("Address2",address.BrokenRulesCollection[0].Property);
            Assert.Equal("The field Address2 must be a string or array type with a maximum length of '35'.",address.BrokenRulesCollection[0].Description);
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
            Assert.Equal("City",address.BrokenRulesCollection[0].Property);
            Assert.Equal("The field City must be a string or array type with a maximum length of '50'.",address.BrokenRulesCollection[0].Description);
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
            Assert.Equal("State",address.BrokenRulesCollection[0].Property);
            Assert.Equal("The field State must be a string or array type with a maximum length of '2'.",address.BrokenRulesCollection[0].Description);
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
            Assert.Equal("The field PostCode must be a string or array type with a maximum length of '9'.",address.BrokenRulesCollection[0].Description);
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
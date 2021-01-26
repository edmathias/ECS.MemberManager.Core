using System;
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
    public class AddressEditChild_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;
        private readonly ITestOutputHelper _testOutputHelper;
        public AddressEditChild_Tests(ITestOutputHelper testOutputHelper)
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
        public async Task TestAddressEditChild_GetAddressEditChild()
        {
            var address = BuildAddress();
                
            var addressEditChild = await AddressEditChild.GetAddressEditChild(address);

            Assert.NotNull(addressEditChild);
            Assert.Equal(address.Id, addressEditChild.Id);
            Assert.Equal(address.Address1, addressEditChild.Address1);
            Assert.Equal(address.Address2, addressEditChild.Address2);
            Assert.Equal(address.City, addressEditChild.City);
            Assert.Equal(address.State, addressEditChild.State);
            Assert.Equal(address.PostCode, addressEditChild.PostCode);
            Assert.Equal(address.Notes, addressEditChild.Notes);
            Assert.Equal(address.LastUpdatedBy,addressEditChild.LastUpdatedBy);
            Assert.Equal(new SmartDate(address.LastUpdatedDate),addressEditChild.LastUpdatedDate);
            
            Assert.True(addressEditChild.IsValid);
        }

        // test invalid state 
        [Fact]
        public async Task TestAddressEditChild_Address1Required()
        {
            
            var address = BuildAddress();
            var addressEditChild = await AddressEditChild.GetAddressEditChild(address);
            var isObjectValidInit = addressEditChild.IsValid;
            addressEditChild.Address1 = string.Empty;

            Assert.True(isObjectValidInit);
            Assert.False(addressEditChild.IsValid);
            Assert.Equal("The Address1 field is required.",addressEditChild.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressEditChild_CityRequired()
        {
            var address = BuildAddress();
            var addressEditChild = await AddressEditChild.GetAddressEditChild(address);
            var isObjectValidInit = addressEditChild.IsValid;
            addressEditChild.City = string.Empty;

            Assert.True(isObjectValidInit);
            Assert.False(addressEditChild.IsValid);
            Assert.Equal("The City field is required.",addressEditChild.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressEditChild_StateRequired()
        {
            var address = BuildAddress();
            var addressEditChild = await AddressEditChild.GetAddressEditChild(address);
            var isObjectValidInit = addressEditChild.IsValid;
            addressEditChild.State = string.Empty;

            Assert.True(isObjectValidInit);
            Assert.False(addressEditChild.IsValid);
            Assert.Equal("The State field is required.",addressEditChild.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressEditChild_PostCodeRequired()
        {
            var address = BuildAddress();
            var addressEditChild = await AddressEditChild.GetAddressEditChild(address);
            var isObjectValidInit = addressEditChild.IsValid;
            addressEditChild.PostCode = string.Empty;

            Assert.True(isObjectValidInit);
            Assert.False(addressEditChild.IsValid);
            Assert.Equal("The PostCode field is required.",addressEditChild.BrokenRulesCollection[0].Description);
        }


        [Fact]
        public async Task TestAddressEditChild_Address1ExceedsMaxLengthOf35()
        {
            var address = BuildAddress();
            var addressEditChild = await AddressEditChild.GetAddressEditChild(address);
            addressEditChild.Address1 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                        "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                        "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                        "Duis aute irure dolor in reprehenderit";

            Assert.False(addressEditChild.IsValid);
            Assert.Equal("The field Address1 must be a string or array type with a maximum length of '35'.",addressEditChild.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressEditChild_Address2ExceedsMaxLengthOf35()
        {
            var address = BuildAddress();
            var addressEditChild = await AddressEditChild.GetAddressEditChild(address);
            addressEditChild.Address2 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                        "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                        "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                        "Duis aute irure dolor in reprehenderit";

            Assert.False(addressEditChild.IsValid);
            Assert.Equal("The field Address2 must be a string or array type with a maximum length of '35'.",addressEditChild.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressEditChild_CityExceedsMaxLengthOf50()
        {
            var address = BuildAddress();
            var addressEditChild = await AddressEditChild.GetAddressEditChild(address);
            addressEditChild.City = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                    "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                    "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                    "Duis aute irure dolor in reprehenderit";

            Assert.False(addressEditChild.IsValid);
            Assert.Equal("The field City must be a string or array type with a maximum length of '50'.",addressEditChild.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressEditChild_StateExceedsMaxLengthOf2()
        {
            var address = BuildAddress();
            var addressEditChild = await AddressEditChild.GetAddressEditChild(address);
            addressEditChild.State = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor ";

            Assert.False(addressEditChild.IsValid);
            Assert.Equal("The field State must be a string or array type with a maximum length of '2'.",addressEditChild.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressEditChild_PostCodeExceedsMaxLengthOf9()
        {
            var address = BuildAddress();
            var addressEditChild = await AddressEditChild.GetAddressEditChild(address);
            addressEditChild.PostCode = "Lorem ipsum dolor sit amet";

            Assert.False(addressEditChild.IsValid);
            Assert.Equal("The field PostCode must be a string or array type with a maximum length of '9'.",
                addressEditChild.BrokenRulesCollection[0].Description);
        }

        private Address BuildAddress()
        {
            var address = new Address();
            address.Address1 = "Address line 1";
            address.Address2 = "Address line 2";
            address.City = "City";
            address.State = "CO";
            address.PostCode = "80111";
            address.LastUpdatedDate = DateTime.Now;
            address.LastUpdatedBy = "edm";
            address.Notes = "This person is on standby";

            return address;
        }
    }
}
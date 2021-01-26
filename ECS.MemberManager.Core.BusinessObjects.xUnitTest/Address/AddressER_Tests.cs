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
        public async Task TestAddressER_Get()
        {
            var address = await AddressER.GetAddressER(1);

            Assert.NotNull(address);
            Assert.IsType<AddressER>(address);
            Assert.Equal(1,address.Id);
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
            Assert.Equal("These are updated Notes",result.Notes );
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
        public async Task TestAddressER_Address1Required()
        {
            var address = await AddressER.NewAddressER();
            BuildAddress(address);
            address.Address1 = "make valid";
            var isObjectValidInit = address.IsValid;
            address.Address1 = string.Empty;

            Assert.NotNull(address);
            Assert.True(isObjectValidInit);
            Assert.False(address.IsValid);
        }

        [Fact]
        public async Task TestAddressER_CityRequired()
        {
            var address = await AddressER.NewAddressER();
            BuildAddress(address);
            var isObjectValidInit = address.IsValid;
            address.City = string.Empty;

            Assert.NotNull(address);
            Assert.True(isObjectValidInit);
            Assert.False(address.IsValid);
        }

        [Fact]
        public async Task TestAddressER_StateRequired()
        {
            var address = await AddressER.NewAddressER();
            BuildAddress(address);
            var isObjectValidInit = address.IsValid;
            address.State = string.Empty;

            Assert.NotNull(address);
            Assert.True(isObjectValidInit);
            Assert.False(address.IsValid);
        }

        [Fact]
        public async Task TestAddressER_PostCodeRequired()
        {
            var address = await AddressER.NewAddressER();
            BuildAddress(address);
            var isObjectValidInit = address.IsValid;
            address.PostCode = string.Empty;

            Assert.NotNull(address);
            Assert.True(isObjectValidInit);
            Assert.False(address.IsValid);
        }


        [Fact]
        public async Task TestAddressER_Address1ExceedsMaxLengthOf35()
        {
            var address = await AddressER.NewAddressER();
            BuildAddress(address);
            address.Address1 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                               "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                               "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                               "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(address);
            Assert.False(address.IsValid);
            Assert.Equal("The field Address1 must be a string or array type with a maximum length of '35'.",address.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressER_Address2ExceedsMaxLengthOf35()
        {
            var address = await AddressER.NewAddressER();
            BuildAddress(address);
            address.Address2 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                               "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                               "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                               "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(address);
            Assert.False(address.IsValid);
            Assert.Equal("The field Address2 must be a string or array type with a maximum length of '35'.",address.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressER_CityExceedsMaxLengthOf50()
        {
            var address = await AddressER.NewAddressER();
            BuildAddress(address);
            address.City = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                           "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                           "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                           "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(address);
            Assert.False(address.IsValid);
            Assert.Equal("The field City must be a string or array type with a maximum length of '50'.",address.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressER_StateExceedsMaxLengthOf2()
        {
            var address = await AddressER.NewAddressER();
            BuildAddress(address);
            address.State = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor ";

            Assert.NotNull(address);
            Assert.False(address.IsValid);
            Assert.Equal("The field State must be a string or array type with a maximum length of '2'.",address.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressER_PostCodeExceedsMaxLengthOf9()
        {
            var address = await AddressER.NewAddressER();
            BuildAddress(address);
            address.PostCode = "Lorem ipsum dolor sit amet";

            Assert.NotNull(address);
            Assert.False(address.IsValid);
            Assert.Equal("The field PostCode must be a string or array type with a maximum length of '9'.",address.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestAddressER_TestInvalidSave()
        {
            var Address = await AddressER.NewAddressER();
            Address.Address1 = String.Empty;
            AddressER savedAddress = null;

            Assert.False(Address.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedAddress = Address.Save());
        }

        [Fact]
        public async Task AddressER_TestSaveOutOfOrder()
        {
            var address1 = await AddressER.GetAddressER(1);
            var address2 = await AddressER.GetAddressER(1);
            address1.Notes = "set up timestamp issue"; // turn on IsDirty
            address2.Notes = "set up timestamp issue";

            var address2_2 = await address2.SaveAsync();
            _testOutputHelper.WriteLine($"address 2 save first {address2_2.RowVersion}");
            Assert.NotEqual(address2_2.RowVersion, address1.RowVersion);
            Assert.Equal("set up timestamp issue", address2_2.Notes);
 
            Assert.NotEqual(address2_2.RowVersion, address1.RowVersion);
            await Assert.ThrowsAsync<DataPortalException>(() => address1.SaveAsync());
        }

        [Fact]
        public async Task AddressER_TestSubsequentSaves()
        {
            var address = await AddressER.GetAddressER(1);
            address.Notes = "set up timestamp issue"; // turn on IsDirty

            var address2 = await address.SaveAsync();
            var rowVersion1 = address2.RowVersion;
            address2.Notes = "another timestamp trigger";

            var address3 = await address2.SaveAsync();

            Assert.NotEqual(address2.RowVersion, address3.RowVersion);
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
    }
}
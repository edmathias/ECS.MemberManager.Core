using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;

namespace ECS.MemberManager.Core.BusinessObjects.Test
{
    /// <summary>
    /// Summary description for JustMockTest
    /// </summary>
    [TestClass]
    public class AddressER_Tests
    {
        [TestMethod]
        public async Task TestAddressER_Get()
        {
            var Address = await AddressER.GetAddress(1);

            Assert.AreEqual(Address.Id, 1);
            Assert.IsTrue(Address.IsValid);
        }

        [TestMethod]
        public async Task TestAddressER_New()
        {
            var Address = await AddressER.NewAddress();

            Assert.IsNotNull(Address);
            Assert.IsFalse(Address.IsValid);
        }

        [TestMethod]
        public async Task TestAddressER_Update()
        {
            var Address = await AddressER.GetAddress(1);
            Address.Notes = "These are updated Notes";

            var result = Address.Save();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Notes, "These are updated Notes");
        }

        [TestMethod]
        public async Task TestAddressER_Insert()
        {
            var address = await AddressER.NewAddress();
            BuildAddress(address);

            var savedAddress = address.Save();

            Assert.IsNotNull(savedAddress);
            Assert.IsInstanceOfType(savedAddress, typeof(AddressER));
            Assert.IsTrue(savedAddress.Id > 0);
        }

        [TestMethod]
        public async Task TestAddressER_Delete()
        {
            int beforeCount = MockDb.Addresses.Count();

            await AddressER.DeleteAddress(1);

            Assert.AreNotEqual(beforeCount, MockDb.Addresses.Count());
        }

        // test invalid state 
        [TestMethod]
        public async Task TestAddressER_Address1Required()
        {
            var address = await AddressER.NewAddress();
            BuildAddress(address);
            address.Address1 = "make valid";
            var isObjectValidInit = address.IsValid;
            address.Address1 = string.Empty;

            Assert.IsNotNull(address);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(address.IsValid);
        }

        [TestMethod]
        public async Task TestAddressER_CityRequired()
        {
            var address = await AddressER.NewAddress();
            BuildAddress(address);
            var isObjectValidInit = address.IsValid;
            address.City = string.Empty;

            Assert.IsNotNull(address);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(address.IsValid);
        }

        [TestMethod]
        public async Task TestAddressER_StateRequired()
        {
            var address = await AddressER.NewAddress();
            BuildAddress(address);
            var isObjectValidInit = address.IsValid;
            address.State = string.Empty;

            Assert.IsNotNull(address);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(address.IsValid);
        }

        [TestMethod]
        public async Task TestAddressER_PostCodeRequired()
        {
            var address = await AddressER.NewAddress();
            BuildAddress(address);
            var isObjectValidInit = address.IsValid;
            address.PostCode = string.Empty;

            Assert.IsNotNull(address);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(address.IsValid);
        }


        [TestMethod]
        public async Task TestAddressER_Address1ExceedsMaxLengthOf35()
        {
            var address = await AddressER.NewAddress();
            BuildAddress(address);
            address.Address1 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                               "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                               "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                               "Duis aute irure dolor in reprehenderit";

            Assert.IsNotNull(address);
            Assert.IsFalse(address.IsValid);
            Assert.AreEqual(address.BrokenRulesCollection[0].Description,
                "The field Address1 must be a string or array type with a maximum length of '35'.");
        }

        [TestMethod]
        public async Task TestAddressER_Address2ExceedsMaxLengthOf35()
        {
            var address = await AddressER.NewAddress();
            BuildAddress(address);
            address.Address2 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                               "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                               "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                               "Duis aute irure dolor in reprehenderit";

            Assert.IsNotNull(address);
            Assert.IsFalse(address.IsValid);
            Assert.AreEqual(address.BrokenRulesCollection[0].Description,
                "The field Address2 must be a string or array type with a maximum length of '35'.");
        }

        [TestMethod]
        public async Task TestAddressER_CityExceedsMaxLengthOf50()
        {
            var address = await AddressER.NewAddress();
            BuildAddress(address);
            address.City = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                           "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                           "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                           "Duis aute irure dolor in reprehenderit";

            Assert.IsNotNull(address);
            Assert.IsFalse(address.IsValid);
            Assert.AreEqual(address.BrokenRulesCollection[0].Description,
                "The field City must be a string or array type with a maximum length of '50'.");
        }

        [TestMethod]
        public async Task TestAddressER_StateExceedsMaxLengthOf2()
        {
            var address = await AddressER.NewAddress();
            BuildAddress(address);
            address.State = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor ";

            Assert.IsNotNull(address);
            Assert.IsFalse(address.IsValid);
            Assert.AreEqual(address.BrokenRulesCollection[0].Description,
                "The field State must be a string or array type with a maximum length of '2'.");
        }

        [TestMethod]
        public async Task TestAddressER_PostCodeExceedsMaxLengthOf9()
        {
            var address = await AddressER.NewAddress();
            BuildAddress(address);
            address.PostCode = "Lorem ipsum dolor sit amet";

            Assert.IsNotNull(address);
            Assert.IsFalse(address.IsValid);
            Assert.AreEqual(address.BrokenRulesCollection[0].Description,
                "The field PostCode must be a string or array type with a maximum length of '9'.");
        }

        [TestMethod]
        public async Task TestAddressER_TestInvalidSave()
        {
            var Address = await AddressER.NewAddress();
            Address.Address1 = String.Empty;
            AddressER savedAddress = null;

            Assert.IsFalse(Address.IsValid);
            Assert.ThrowsException<ValidationException>(() => savedAddress = Address.Save());
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
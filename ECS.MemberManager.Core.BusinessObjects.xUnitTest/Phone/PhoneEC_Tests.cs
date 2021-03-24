using System;
using System.IO;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PhoneEC_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PhoneEC_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");

            if (testLibrary == "Mock")
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
        public async Task TestPhoneEC_NewPhoneEC()
        {
            var phone = await PhoneEC.NewPhoneEC();

            Assert.NotNull(phone);
            Assert.IsType<PhoneEC>(phone);
            Assert.False(phone.IsValid);
        }

        [Fact]
        public async Task TestPhoneEC_GetPhoneEC()
        {
            var phoneToLoad = BuildPhone();
            var phone = await PhoneEC.GetPhoneEC(phoneToLoad);

            Assert.NotNull(phone);
            Assert.IsType<PhoneEC>(phone);
            Assert.Equal(phoneToLoad.Id, phone.Id);
            Assert.Equal(phoneToLoad.PhoneType, phone.PhoneType);
            Assert.Equal(phoneToLoad.AreaCode, phone.AreaCode);
            Assert.Equal(phoneToLoad.Number, phone.Number);
            Assert.Equal(phoneToLoad.Extension, phone.Extension);
            Assert.Equal(phoneToLoad.DisplayOrder, phone.DisplayOrder);
            Assert.Equal(phoneToLoad.LastUpdatedBy, phone.LastUpdatedBy);
            Assert.Equal(new SmartDate(phoneToLoad.LastUpdatedDate), phone.LastUpdatedDate);
            Assert.Equal(phoneToLoad.Notes, phone.Notes);
            Assert.Equal(phoneToLoad.RowVersion, phone.RowVersion);
            Assert.True(phone.IsValid);
        }

        [Fact]
        public async Task TestPhoneEC_PhoneTypeRequired()
        {
            var phoneToTest = BuildPhone();
            var phone = await PhoneEC.GetPhoneEC(phoneToTest);
            var isObjectValidInit = phone.IsValid;
            phone.PhoneType = string.Empty;

            Assert.NotNull(phone);
            Assert.True(isObjectValidInit);
            Assert.False(phone.IsValid);
            Assert.Equal("PhoneType", phone.BrokenRulesCollection[0].Property);
            Assert.Equal("PhoneType required", phone.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestPhoneEC_AreaCodeRequired()
        {
            var phoneToTest = BuildPhone();
            var phone = await PhoneEC.GetPhoneEC(phoneToTest);
            var isObjectValidInit = phone.IsValid;
            phone.AreaCode = string.Empty;

            Assert.NotNull(phone);
            Assert.True(isObjectValidInit);
            Assert.False(phone.IsValid);
            Assert.Equal("AreaCode", phone.BrokenRulesCollection[0].Property);
            Assert.Equal("AreaCode required", phone.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestPhoneEC_NumberRequired()
        {
            var phoneToTest = BuildPhone();
            var phone = await PhoneEC.GetPhoneEC(phoneToTest);
            var isObjectValidInit = phone.IsValid;
            phone.Number = string.Empty;

            Assert.NotNull(phone);
            Assert.True(isObjectValidInit);
            Assert.False(phone.IsValid);
            Assert.Equal("Number", phone.BrokenRulesCollection[0].Property);
            Assert.Equal("Number required", phone.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestPhoneEC_LastUpdatedByRequired()
        {
            var phoneToTest = BuildPhone();
            var phone = await PhoneEC.GetPhoneEC(phoneToTest);
            var isObjectValidInit = phone.IsValid;
            phone.LastUpdatedBy = string.Empty;

            Assert.NotNull(phone);
            Assert.True(isObjectValidInit);
            Assert.False(phone.IsValid);
            Assert.Equal("LastUpdatedBy", phone.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy required", phone.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestPhoneEC_DescriptionLessThan50Chars()
        {
            var phoneToTest = BuildPhone();
            var phone = await PhoneEC.GetPhoneEC(phoneToTest);
            var isObjectValidInit = phone.IsValid;
            phone.LastUpdatedBy = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                  "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                  "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                  "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(phone);
            Assert.True(isObjectValidInit);
            Assert.False(phone.IsValid);
            Assert.Equal("LastUpdatedBy", phone.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy can not exceed 255 characters", phone.BrokenRulesCollection[0].Description);
        }

        private Phone BuildPhone()
        {
            var phone = new Phone();
            phone.Id = 1;
            phone.PhoneType = "mobile";
            phone.AreaCode = "303";
            phone.Number = "555-2368";
            phone.Extension = "123";
            phone.DisplayOrder = 1;
            phone.LastUpdatedBy = "Hank";
            phone.LastUpdatedDate = DateTime.Now;
            phone.Notes = "notes for doctype";

            return phone;
        }
    }
}
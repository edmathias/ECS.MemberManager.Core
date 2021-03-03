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
    public class PhoneER_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PhoneER_Tests()
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
        public async Task TestPhoneER_NewPhoneER()
        {
            var phone = await PhoneER.NewPhoneER();

            Assert.NotNull(phone);
            Assert.IsType<PhoneER>(phone);
            Assert.False(phone.IsValid);
        }
        
        [Fact]
        public async Task TestPhoneER_GetPhoneER()
        {
            var phone = await PhoneER.GetPhoneER(1);

            Assert.NotNull(phone);
            Assert.IsType<PhoneER>(phone);
            Assert.Equal(1,phone.Id);
            Assert.True(phone.IsValid);
        }

        [Fact]
        public async Task TestPhoneER_PhoneTypeRequired()
        {
            var phone = await PhoneER.GetPhoneER(1);
            var isObjectValidInit = phone.IsValid;
            phone.PhoneType= string.Empty;

            Assert.NotNull(phone);
            Assert.True(isObjectValidInit);
            Assert.False(phone.IsValid);
            Assert.Equal("PhoneType",phone.BrokenRulesCollection[0].Property);
            Assert.Equal("PhoneType required",phone.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestPhoneER_AreaCodeRequired()
        {
            var phone = await PhoneER.GetPhoneER(1);
            var isObjectValidInit = phone.IsValid;
            phone.AreaCode= string.Empty;

            Assert.NotNull(phone);
            Assert.True(isObjectValidInit);
            Assert.False(phone.IsValid);
            Assert.Equal("AreaCode",phone.BrokenRulesCollection[0].Property);
            Assert.Equal("AreaCode required",phone.BrokenRulesCollection[0].Description);
        }
    
        [Fact]
        public async Task TestPhoneER_NumberRequired()
        {
            var phone = await PhoneER.GetPhoneER(1);
            var isObjectValidInit = phone.IsValid;
            phone.Number= string.Empty;

            Assert.NotNull(phone);
            Assert.True(isObjectValidInit);
            Assert.False(phone.IsValid);
            Assert.Equal("Number",phone.BrokenRulesCollection[0].Property);
            Assert.Equal("Number required",phone.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestPhoneER_LastUpdatedByRequired()
        {
            var phone = await PhoneER.GetPhoneER(1);
            var isObjectValidInit = phone.IsValid;
            phone.LastUpdatedBy= string.Empty;

            Assert.NotNull(phone);
            Assert.True(isObjectValidInit);
            Assert.False(phone.IsValid);
            Assert.Equal("LastUpdatedBy",phone.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy required",phone.BrokenRulesCollection[0].Description);
        }
        
        [Fact]
        public async Task TestPhoneER_DescriptionLessThan50Chars()
        {
            var phone = await PhoneER.GetPhoneER(1);
            var isObjectValidInit = phone.IsValid;
            phone.LastUpdatedBy = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(phone);
            Assert.True(isObjectValidInit);
            Assert.False(phone.IsValid);
            Assert.Equal("LastUpdatedBy",phone.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy can not exceed 255 characters",phone.BrokenRulesCollection[0].Description);
            
        }
    }
}
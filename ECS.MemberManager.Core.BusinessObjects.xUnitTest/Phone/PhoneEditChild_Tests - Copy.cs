using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PhoneEditChild_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PhoneEditChild_Tests()
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
        public async Task TestPhoneEditChild_GetPhoneEditChild()
        {
            var phone = new Phone()
            {
                Id = 1,
                PhoneType = "mobile",
                AreaCode = "303",
                Number = "333-2222",
                Extension = "111",
                DisplayOrder = 3,
                LastUpdatedBy = "edm",
                LastUpdatedDate = DateTime.Now,
                Notes = "notes for phone #1"
            };
            
            var phoneEditChild = await PhoneEditChild.GetPhoneEditChild(phone);

            Assert.True(phoneEditChild.IsValid);
            Assert.Equal(phone.Id, phoneEditChild.Id);
            Assert.Equal(phone.PhoneType, phoneEditChild.PhoneType);
            Assert.Equal(phone.AreaCode,phoneEditChild.AreaCode);
            Assert.Equal(phone.Number,phoneEditChild.Number);
            Assert.Equal(phone.Extension,phoneEditChild.Extension);
            Assert.Equal(phone.DisplayOrder, phoneEditChild.DisplayOrder);
            Assert.Equal(phone.LastUpdatedBy, phoneEditChild.LastUpdatedBy);
            Assert.Equal(new SmartDate(phone.LastUpdatedDate), phoneEditChild.LastUpdatedDate);
            Assert.Equal(phone.Notes, phoneEditChild.Notes);
         }

        // test invalid state 
        [Fact]
        public async Task TestPhoneEditChild_PhoneAreaCodeRequired()
        {
            var phone = BuildValidPhone();
            var phoneEditChild = await PhoneEditChild.GetPhoneEditChild(phone);
            var isObjectValidInit = phoneEditChild.IsValid;
            phoneEditChild.AreaCode = string.Empty;

            Assert.NotNull(phoneEditChild);
            Assert.True(isObjectValidInit);
            Assert.False(phoneEditChild.IsValid);
        }
        
        [Fact]
        public async Task TestPhoneEditChild_PhoneNumberRequired()
        {
            var phone = BuildValidPhone();
            var phoneEditChild = await PhoneEditChild.GetPhoneEditChild(phone);
            var isObjectValidInit = phoneEditChild.IsValid;
            phoneEditChild.Number = string.Empty;

            Assert.NotNull(phoneEditChild);
            Assert.True(isObjectValidInit);
            Assert.False(phoneEditChild.IsValid);
        }

        [Fact]
        public async Task TestPhoneEditChild_LastUpdatedByRequired()
        {
            var phone = BuildValidPhone();
            var phoneEditChild = await PhoneEditChild.GetPhoneEditChild(phone);
            var isObjectValidInit = phoneEditChild.IsValid;
            phoneEditChild.LastUpdatedBy = string.Empty;

            Assert.NotNull(phoneEditChild);
            Assert.True(isObjectValidInit);
            Assert.False(phoneEditChild.IsValid);
        }

        private static Phone BuildValidPhone()
        {
            var phone = new Phone();
            
            phone.AreaCode = "303";
            phone.Extension = "111";
            phone.Number = "555-2368";
            phone.PhoneType = "mobile";
            phone.DisplayOrder = 1;
            phone.LastUpdatedBy = "edm";
            phone.LastUpdatedDate = DateTime.Now;
            phone.Notes = "This person is on standby";

            return phone;
        }
    }
}
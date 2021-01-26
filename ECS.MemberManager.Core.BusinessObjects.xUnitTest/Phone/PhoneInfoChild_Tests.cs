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
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PhoneInfoChild_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PhoneInfoChild_Tests()
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
        public async Task TestPhoneInfo_GetPhoneInfo()
        {
            var phone = BuildValidPhone();
            
            var phoneInfoChild = await PhoneInfoChild.GetPhoneInfoChild(phone);

            Assert.Equal(phoneInfoChild.Id,phone.Id);
            Assert.Equal(phoneInfoChild.PhoneType,phone.PhoneType);
            Assert.Equal(phoneInfoChild.AreaCode, phone.AreaCode);
            Assert.Equal(phoneInfoChild.Number, phone.Number);
            Assert.Equal(phoneInfoChild.Extension, phone.Extension);
            Assert.Equal(phoneInfoChild.DisplayOrder, phone.DisplayOrder);
            Assert.Equal(phoneInfoChild.LastUpdatedDate ,new SmartDate(phone.LastUpdatedDate));
            Assert.Equal(phoneInfoChild.LastUpdatedBy, phone.LastUpdatedBy);
            Assert.Equal(phoneInfoChild.Notes, phone.Notes);
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
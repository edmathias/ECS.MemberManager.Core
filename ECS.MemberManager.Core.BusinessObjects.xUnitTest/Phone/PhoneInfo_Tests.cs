using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PhoneInfo_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PhoneInfo_Tests()
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
        public async Task TestPhoneEdit_Get()
        {
            var phone = await PhoneInfo.GetPhoneInfo(1);

            Assert.Equal(1,phone.Id);
            Assert.Equal("mobile",phone.PhoneType);
            Assert.Equal("303", phone.AreaCode);
            Assert.Equal("333-2222", phone.Number);
            Assert.Equal("111", phone.Extension);
            Assert.Equal(0, phone.DisplayOrder);
            Assert.Equal(new SmartDate("2021-01-01") ,phone.LastUpdatedDate);
            Assert.Equal("edm", phone.LastUpdatedBy);
            Assert.Equal("notes for phone #1", phone.Notes);
        }
    }
}
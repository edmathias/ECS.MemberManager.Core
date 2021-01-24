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
    public class PhoneEdit_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PhoneEdit_Tests()
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
            var phone = await PhoneEdit.GetPhoneEdit(1);

            Assert.Equal(1,phone.Id);
            Assert.Equal("mobile",phone.PhoneType);
            Assert.Equal("303", phone.AreaCode);
            Assert.Equal("333-2222", phone.Number);
            Assert.Equal("111", phone.Extension);
            Assert.Equal(0, phone.DisplayOrder);
            Assert.Equal(new SmartDate("2021-01-01") ,phone.LastUpdatedDate);
            Assert.Equal("edm", phone.LastUpdatedBy);
            Assert.Equal("notes for phone #1", phone.Notes);
            Assert.True(phone.IsValid);
        }

        [Fact]
        public async Task TestPhoneEdit_New()
        {
            var phone = await PhoneEdit.NewPhoneEdit();

            Assert.NotNull(phone);
            Assert.False(phone.IsValid);
        }

        [Fact]
        public async Task TestPhoneEdit_Update()
        {
            var phone = await PhoneEdit.GetPhoneEdit(1);
            phone.Notes = "These are updated Notes";
            
            var result = await phone.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal( "These are updated Notes",result.Notes);
        }

        [Fact]
        public async Task TestPhoneEdit_Insert()
        {
            var phone = await PhoneEdit.NewPhoneEdit();
            
            BuildValidPhoneEdit(phone);

            var savedPhone = await phone.SaveAsync();
           
            Assert.NotNull(savedPhone);
            Assert.IsType<PhoneEdit>(savedPhone);
            Assert.True( savedPhone.Id > 0 );
        }

        [Fact]
        public async Task TestPhoneEdit_Delete()
        {
            const int ID_TO_DELETE = 99;
            
            await PhoneEdit.DeletePhoneEdit(ID_TO_DELETE);

            await Assert.ThrowsAsync<DataPortalException>(() => PhoneEdit.GetPhoneEdit(ID_TO_DELETE));
        }
        
        // test invalid state 
        [Fact]
        public async Task TestPhoneEdit_PhoneAreaCodeRequired()
        {
            var phone = await PhoneEdit.NewPhoneEdit();
            BuildValidPhoneEdit(phone);
            var isObjectValidInit = phone.IsValid;
            phone.AreaCode = string.Empty;

            Assert.NotNull(phone);
            Assert.True(isObjectValidInit);
            Assert.False(phone.IsValid);
        }
        
        [Fact]
        public async Task TestPhoneEdit_PhoneNumberRequired()
        {
            var phone = await PhoneEdit.NewPhoneEdit();
            BuildValidPhoneEdit(phone);
            var isObjectValidInit = phone.IsValid;
            phone.Number = string.Empty;

            Assert.NotNull(phone);
            Assert.True(isObjectValidInit);
            Assert.False(phone.IsValid);
        }

        [Fact]
        public async Task TestPhoneEdit_LastUpdatedByRequired()
        {
            var phone = await PhoneEdit.NewPhoneEdit();
            BuildValidPhoneEdit(phone);
            var isObjectValidInit = phone.IsValid;
            phone.LastUpdatedBy = string.Empty;

            Assert.NotNull(phone);
            Assert.True(isObjectValidInit);
            Assert.False(phone.IsValid);
        }
       
        [Fact]
        public async Task TestPhoneEdit_LastUpdatedDateRequired()
        {
            var phone = await PhoneEdit.NewPhoneEdit();
            BuildValidPhoneEdit(phone);
            var isObjectValidInit = phone.IsValid;
            phone.LastUpdatedDate = null;

            Assert.True(phone.LastUpdatedDate.IsEmpty);
            Assert.True(isObjectValidInit);
            Assert.True(phone.IsValid);
        }

        [Fact]
        public async Task TestPhoneEdit_TestInvalidSave()
        {
            var phone = await PhoneEdit.NewPhoneEdit();
            
            Assert.False(phone.IsValid);
            Assert.Throws<ValidationException>(() => phone.Save() );
        }   
        
        private static void BuildValidPhoneEdit(PhoneEdit phone)
        {
            phone.AreaCode = "303";
            phone.Extension = "111";
            phone.Number = "555-2368";
            phone.PhoneType = "mobile";
            phone.DisplayOrder = 1;
            phone.LastUpdatedBy = "edm";
            phone.LastUpdatedDate = DateTime.Now;
            phone.Notes = "This person is on standby";
        }
    }
}
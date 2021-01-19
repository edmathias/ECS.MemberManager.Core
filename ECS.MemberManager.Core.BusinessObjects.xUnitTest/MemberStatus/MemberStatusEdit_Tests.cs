using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MemberStatusEdit_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public MemberStatusEdit_Tests()
        {
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
        public async Task TestMemberStatusEdit_TestGetMemberStatusEdit()
        {
            var eMailType = await MemberStatusEdit.GetMemberStatusEdit(1);

            Assert.NotNull(eMailType);
            Assert.IsType<MemberStatusEdit>(eMailType);
            Assert.Equal(1, eMailType.Id);
            Assert.True(eMailType.IsValid);
        }

        [Fact]
        public async Task TestMemberStatusEdit_New()
        {
            var eMailType = await MemberStatusEdit.NewMemberStatusEdit();

            Assert.NotNull(eMailType);
            Assert.False(eMailType.IsValid);
        }

        [Fact]
        public async void TestMemberStatusEdit_Update()
        {
            var eMailType = await MemberStatusEdit.GetMemberStatusEdit(1);
            var notesUpdate = $"These are updated Notes {DateTime.Now}";
            eMailType.Notes = notesUpdate;

            var result = await eMailType.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal(notesUpdate, result.Notes);
        }

        [Fact]
        public async void TestMemberStatusEdit_Insert()
        {
            var eMailType = await MemberStatusEdit.NewMemberStatusEdit();
            eMailType.Description = "Standby";
            eMailType.Notes = "This person is inserted";

            var savedMemberStatus = await eMailType.SaveAsync();

            Assert.NotNull(savedMemberStatus);
            Assert.IsType<MemberStatusEdit>(savedMemberStatus);
            Assert.True(savedMemberStatus.Id > 0);
            Assert.NotNull(savedMemberStatus.RowVersion);
        }

        [Fact]
        public async Task TestMemberStatusEdit_Delete()
        {
            await MemberStatusEdit.DeleteMemberStatusEdit(99);

            var emailTypeToCheck = await Assert.ThrowsAsync<Csla.DataPortalException>
                (() => MemberStatusEdit.GetMemberStatusEdit(99));
        }

        [Fact]
        public async Task TestMemberStatusEdit_DescriptionRequired()
        {
            var eMailType = await MemberStatusEdit.NewMemberStatusEdit();
            eMailType.Description = "make valid";
            var isObjectValidInit = eMailType.IsValid;
            eMailType.Description = string.Empty;

            Assert.NotNull(eMailType);
            Assert.True(isObjectValidInit);
            Assert.False(eMailType.IsValid);
        }

        [Fact]
        public async Task TestMemberStatusEdit_DescriptionExceedsMaxLengthOf50()
        {
            var eMailType = await MemberStatusEdit.NewMemberStatusEdit();
            eMailType.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                    "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                    "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                    "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(eMailType);
            Assert.False(eMailType.IsValid);
            Assert.Equal("The field Description must be a string or array type with a maximum length of '50'.",
                eMailType.BrokenRulesCollection[0].Description);
        }
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestMemberStatusEdit_TestInvalidSave()
        {
            var eMailType = await MemberStatusEdit.NewMemberStatusEdit();
            eMailType.Description = String.Empty;

            Assert.False(eMailType.IsValid);
            await Assert.ThrowsAsync<ValidationException>(() => eMailType.SaveAsync());
        }
    
        [Fact]
        public async Task MemberStatusEdit_TestSaveOutOfOrder()
        {
            var emailType1 = await MemberStatusEdit.GetMemberStatusEdit(1);
            var emailType2 = await MemberStatusEdit.GetMemberStatusEdit(1);
            emailType1.Notes = "set up timestamp issue";  // turn on IsDirty
            emailType2.Notes = "set up timestamp issue";

            var emailType2_2 = await emailType2.SaveAsync();
            
            Assert.NotEqual(emailType2_2.RowVersion, emailType1.RowVersion);
            Assert.Equal("set up timestamp issue",emailType2_2.Notes);
            await Assert.ThrowsAsync<DataPortalException>(() => emailType1.SaveAsync());
        }

        [Fact]
        public async Task MemberStatusEdit_TestSubsequentSaves()
        {
            var emailType = await MemberStatusEdit.GetMemberStatusEdit(1);
            emailType.Notes = "set up timestamp issue";  // turn on IsDirty

            var emailType2 = await emailType.SaveAsync();
            var rowVersion1 = emailType2.RowVersion;
            emailType2.Notes = "another timestamp trigger";

            var emailType3 = await emailType2.SaveAsync();
            
            Assert.NotEqual(emailType2.RowVersion, emailType3.RowVersion);
        }
        
        [Fact]
        public async Task TestMemberStatusEdit_InvalidGet()
        {
            await Assert.ThrowsAsync<DataPortalException>(() => MemberStatusEdit.GetMemberStatusEdit(999));
        }
    }
}
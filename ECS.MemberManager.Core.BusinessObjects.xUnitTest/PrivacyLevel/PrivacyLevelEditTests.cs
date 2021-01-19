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
    public class PrivacyLevelEditTests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PrivacyLevelEditTests()
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
        public async Task PrivacyLevelEdit_TestGetPrivacyLevelEdit()
        {
            var privacyLevel = await PrivacyLevelEdit.GetPrivacyLevelEdit(1);

            Assert.NotNull(privacyLevel);
            Assert.IsType<PrivacyLevelEdit>(privacyLevel);
            Assert.Equal(1, privacyLevel.Id);
            Assert.True(privacyLevel.IsValid);
        }

        [Fact]
        public async Task PrivacyLevelEdit_New()
        {
            var privacyLevel = await PrivacyLevelEdit.NewPrivacyLevelEdit();

            Assert.NotNull(privacyLevel);
            Assert.False(privacyLevel.IsValid);
        }

        [Fact]
        public async void PrivacyLevelEdit_Update()
        {
            var privacyLevel = await PrivacyLevelEdit.GetPrivacyLevelEdit(1);
            var notesUpdate = $"These are updated Notes {DateTime.Now}";
            privacyLevel.Notes = notesUpdate;

            var result = await privacyLevel.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal(notesUpdate, result.Notes);
        }

        [Fact]
        public async void PrivacyLevelEdit_Insert()
        {
            var privacyLevel = await PrivacyLevelEdit.NewPrivacyLevelEdit();
            privacyLevel.Description = "Standby";
            privacyLevel.Notes = "This person is inserted";

            var savedPrivacyLevel = await privacyLevel.SaveAsync();

            Assert.NotNull(savedPrivacyLevel);
            Assert.IsType<PrivacyLevelEdit>(savedPrivacyLevel);
            Assert.True(savedPrivacyLevel.Id > 0);
            Assert.NotNull(savedPrivacyLevel.RowVersion);
        }

        [Fact]
        public async Task PrivacyLevelEdit_Delete()
        {
            await PrivacyLevelEdit.DeletePrivacyLevelEdit(99);

            var privacyLevelToCheck = await Assert.ThrowsAsync<Csla.DataPortalException>
                (() => PrivacyLevelEdit.GetPrivacyLevelEdit(99));
        }

        [Fact]
        public async Task PrivacyLevelEdit_DescriptionRequired()
        {
            var privacyLevel = await PrivacyLevelEdit.NewPrivacyLevelEdit();
            privacyLevel.Description = "make valid";
            var isObjectValidInit = privacyLevel.IsValid;
            privacyLevel.Description = string.Empty;

            Assert.NotNull(privacyLevel);
            Assert.True(isObjectValidInit);
            Assert.False(privacyLevel.IsValid);
        }

        [Fact]
        public async Task PrivacyLevelEdit_DescriptionExceedsMaxLengthOf50()
        {
            var privacyLevel = await PrivacyLevelEdit.NewPrivacyLevelEdit();
            privacyLevel.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                    "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                    "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                    "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(privacyLevel);
            Assert.False(privacyLevel.IsValid);
            Assert.Equal("The field Description must be a string or array type with a maximum length of '50'.",
                privacyLevel.BrokenRulesCollection[0].Description);
        }
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task PrivacyLevelEdit_TestInvalidSave()
        {
            var privacyLevel = await PrivacyLevelEdit.NewPrivacyLevelEdit();
            privacyLevel.Description = String.Empty;

            Assert.False(privacyLevel.IsValid);
            await Assert.ThrowsAsync<ValidationException>(() => privacyLevel.SaveAsync());
        }
    
        [Fact]
        public async Task PrivacyLevelEdit_TestSaveOutOfOrder()
        {
            var privacyLevel1 = await PrivacyLevelEdit.GetPrivacyLevelEdit(1);
            var privacyLevel2 = await PrivacyLevelEdit.GetPrivacyLevelEdit(1);
            privacyLevel1.Notes = "set up timestamp issue";  // turn on IsDirty
            privacyLevel2.Notes = "set up timestamp issue";

            var privacyLevel2_2 = await privacyLevel2.SaveAsync();
            
            Assert.NotEqual(privacyLevel2_2.RowVersion, privacyLevel1.RowVersion);
            Assert.Equal("set up timestamp issue",privacyLevel2_2.Notes);
            await Assert.ThrowsAsync<DataPortalException>(() => privacyLevel1.SaveAsync());
        }

        [Fact]
        public async Task PrivacyLevelEdit_TestSubsequentSaves()
        {
            var privacyLevel = await PrivacyLevelEdit.GetPrivacyLevelEdit(1);
            privacyLevel.Notes = "set up timestamp issue";  // turn on IsDirty

            var privacyLevel2 = await privacyLevel.SaveAsync();
            var rowVersion1 = privacyLevel2.RowVersion;
            privacyLevel2.Notes = "another timestamp trigger";

            var privacyLevel3 = await privacyLevel2.SaveAsync();
            
            Assert.NotEqual(privacyLevel2.RowVersion, privacyLevel3.RowVersion);
        }
        
        [Fact]
        public async Task PrivacyLevelEdit_InvalidGet()
        {
            await Assert.ThrowsAsync<DataPortalException>(() => PrivacyLevelEdit.GetPrivacyLevelEdit(999));
        }
    }
}
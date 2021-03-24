using System;
using System.IO;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PrivacyLevelER_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PrivacyLevelER_Tests()
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
        public async Task PrivacyLevelER_TestGetPrivacyLevel()
        {
            var privacyLevel = await PrivacyLevelER.GetPrivacyLevelER(1);

            Assert.NotNull(privacyLevel);
            Assert.IsType<PrivacyLevelER>(privacyLevel);
        }

        [Fact]
        public async Task PrivacyLevelER_TestGetNewPrivacyLevelER()
        {
            var privacyLevel = await PrivacyLevelER.NewPrivacyLevelER();

            Assert.NotNull(privacyLevel);
            Assert.False(privacyLevel.IsValid);
        }

        [Fact]
        public async Task PrivacyLevelER_TestUpdateExistingPrivacyLevelER()
        {
            var privacyLevel = await PrivacyLevelER.GetPrivacyLevelER(1);
            privacyLevel.Notes = "These are updated Notes";

            var result = await privacyLevel.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes", result.Notes);
        }

        [Fact]
        public async Task PrivacyLevelER_TestInsertNewPrivacyLevelER()
        {
            var privacyLevel = await PrivacyLevelER.NewPrivacyLevelER();
            privacyLevel.Description = "Standby";
            privacyLevel.Notes = "This person is on standby";

            var savedPrivacyLevel = await privacyLevel.SaveAsync();

            Assert.NotNull(savedPrivacyLevel);
            Assert.IsType<PrivacyLevelER>(savedPrivacyLevel);
            Assert.True(savedPrivacyLevel.Id > 0);
        }

        [Fact]
        public async Task PrivacyLevelER_TestDeleteObjectFromDatabase()
        {
            const int ID_TO_DELETE = 99;

            await PrivacyLevelER.DeletePrivacyLevelER(ID_TO_DELETE);

            await Assert.ThrowsAsync<DataPortalException>(() => PrivacyLevelER.GetPrivacyLevelER(ID_TO_DELETE));
        }

        // test invalid state 
        [Fact]
        public async Task PrivacyLevelER_TestDescriptionRequired()
        {
            var privacyLevel = await PrivacyLevelER.NewPrivacyLevelER();
            privacyLevel.Description = "make valid";
            var isObjectValidInit = privacyLevel.IsValid;
            privacyLevel.Description = string.Empty;

            Assert.NotNull(privacyLevel);
            Assert.True(isObjectValidInit);
            Assert.False(privacyLevel.IsValid);
        }

        [Fact]
        public async Task PrivacyLevelER_TestDescriptionExceedsMaxLengthOf50()
        {
            var privacyLevel = await PrivacyLevelER.NewPrivacyLevelER();
            privacyLevel.Description = "valid length";
            Assert.True(privacyLevel.IsValid);

            privacyLevel.Description =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(privacyLevel);
            Assert.False(privacyLevel.IsValid);
            Assert.Equal("Description", privacyLevel.BrokenRulesCollection[0].Property);
            Assert.Equal("Description can not exceed 255 characters",
                privacyLevel.BrokenRulesCollection[0].Description);
        }
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task PrivacyLevelER_TestInvalidSavePrivacyLevelER()
        {
            var privacyLevel = await PrivacyLevelER.NewPrivacyLevelER();
            privacyLevel.Description = String.Empty;
            PrivacyLevelER savedPrivacyLevel = null;

            Assert.False(privacyLevel.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedPrivacyLevel = privacyLevel.Save());
        }
    }
}
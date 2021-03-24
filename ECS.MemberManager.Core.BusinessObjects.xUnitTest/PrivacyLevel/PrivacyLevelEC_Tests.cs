using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PrivacyLevelEC_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PrivacyLevelEC_Tests()
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
        public async Task TestPrivacyLevelEC_NewPrivacyLevelEC()
        {
            var privacyLevel = await PrivacyLevelEC.NewPrivacyLevelEC();

            Assert.NotNull(privacyLevel);
            Assert.IsType<PrivacyLevelEC>(privacyLevel);
            Assert.False(privacyLevel.IsValid);
        }

        [Fact]
        public async Task TestPrivacyLevelEC_GetPrivacyLevelEC()
        {
            var privacyLevelToLoad = BuildPrivacyLevel();
            var privacyLevel = await PrivacyLevelEC.GetPrivacyLevelEC(privacyLevelToLoad);

            Assert.NotNull(privacyLevel);
            Assert.IsType<PrivacyLevelEC>(privacyLevel);
            Assert.Equal(privacyLevelToLoad.Id, privacyLevel.Id);
            Assert.Equal(privacyLevelToLoad.Description, privacyLevel.Description);
            Assert.Equal(privacyLevelToLoad.Notes, privacyLevel.Notes);
            Assert.Equal(privacyLevelToLoad.RowVersion, privacyLevel.RowVersion);
            Assert.True(privacyLevel.IsValid);
        }

        [Fact]
        public async Task TestPrivacyLevelEC_DescriptionRequired()
        {
            var privacyLevelToTest = BuildPrivacyLevel();
            var privacyLevel = await PrivacyLevelEC.GetPrivacyLevelEC(privacyLevelToTest);
            var isObjectValidInit = privacyLevel.IsValid;
            privacyLevel.Description = string.Empty;

            Assert.NotNull(privacyLevel);
            Assert.True(isObjectValidInit);
            Assert.False(privacyLevel.IsValid);
            Assert.Equal("Description", privacyLevel.BrokenRulesCollection[0].Property);
        }

        [Fact]
        public async Task TestPrivacyLevelEC_DescriptionLessThan50Chars()
        {
            var privacyLevelToTest = BuildPrivacyLevel();
            var privacyLevel = await PrivacyLevelEC.GetPrivacyLevelEC(privacyLevelToTest);
            var isObjectValidInit = privacyLevel.IsValid;
            privacyLevel.Description =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(privacyLevel);
            Assert.True(isObjectValidInit);
            Assert.False(privacyLevel.IsValid);
            Assert.Equal("Description", privacyLevel.BrokenRulesCollection[0].Property);
        }

        private PrivacyLevel BuildPrivacyLevel()
        {
            var privacyLevel = new PrivacyLevel();
            privacyLevel.Id = 1;
            privacyLevel.Description = "test description";
            privacyLevel.Notes = "notes for doctype";

            return privacyLevel;
        }
    }
}
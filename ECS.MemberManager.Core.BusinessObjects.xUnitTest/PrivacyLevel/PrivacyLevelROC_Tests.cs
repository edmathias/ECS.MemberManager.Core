using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PrivacyLevelROC_Tests : CslaBaseTest
    {
        [Fact]
        public async void PrivacyLevelROC_TestGetChild()
        {
            const int ID_VALUE = 999;

            var buildPrivacyLevel = BuildPrivacyLevel();
            buildPrivacyLevel.Id = ID_VALUE;

            var privacyLevel = await PrivacyLevelROC.GetPrivacyLevelROC(buildPrivacyLevel);

            Assert.NotNull(privacyLevel);
            Assert.IsType<PrivacyLevelROC>(privacyLevel);
            Assert.Equal(privacyLevel.Id, buildPrivacyLevel.Id);
            Assert.Equal(privacyLevel.Description, buildPrivacyLevel.Description);
            Assert.Equal(privacyLevel.Notes, buildPrivacyLevel.Notes);
            Assert.Equal(privacyLevel.RowVersion, buildPrivacyLevel.RowVersion);
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
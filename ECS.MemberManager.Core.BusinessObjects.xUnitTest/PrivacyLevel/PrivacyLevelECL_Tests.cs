using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PrivacyLevelECL_Tests : CslaBaseTest
    {
        [Fact]
        private async void PrivacyLevelECL_TestPrivacyLevelECL()
        {
            var privacyLevelEdit = await PrivacyLevelECL.NewPrivacyLevelECL();

            Assert.NotNull(privacyLevelEdit);
            Assert.IsType<PrivacyLevelECL>(privacyLevelEdit);
        }


        [Fact]
        private async void PrivacyLevelECL_TestGetPrivacyLevelECL()
        {
            var childData = MockDb.PrivacyLevels;

            var listToTest = await PrivacyLevelECL.GetPrivacyLevelECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }

        private void BuildPrivacyLevel(PrivacyLevelEC privacyLevel)
        {
            privacyLevel.Description = "doc type description";
            privacyLevel.Notes = "document type notes";
        }
    }
}
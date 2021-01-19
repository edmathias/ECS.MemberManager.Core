using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PrivacyLevelInfo_Tests
    {

        [Fact]
        public async void PrivacyLevelInfo_TestGetById()
        {
            var emailInfo = await PrivacyLevelInfo.GetPrivacyLevelInfo(1);
            
            Assert.NotNull(emailInfo);
            Assert.IsType<PrivacyLevelInfo>(emailInfo);
            Assert.Equal(1, emailInfo.Id);
        }

        [Fact]
        public async void PrivacyLevelInfo_TestGetChild()
        {
            const int ID_VALUE = 999;
            
            var emailType = new PrivacyLevel()
            {
                Id = ID_VALUE,
                Description = "Test email type",
                Notes = "email type notes"
            };

            var emailTypeInfo = await PrivacyLevelInfo.GetPrivacyLevelInfo(emailType);
            
            Assert.NotNull(emailTypeInfo);
            Assert.IsType<PrivacyLevelInfo>(emailTypeInfo);
            Assert.Equal(ID_VALUE, emailTypeInfo.Id);

        }
    }
}
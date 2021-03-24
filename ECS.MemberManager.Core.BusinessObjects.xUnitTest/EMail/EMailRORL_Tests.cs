using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EMailRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void EMailRORL_TestGetEMailRORL()
        {
            var eMailTypeInfoList = await EMailRORL.GetEMailRORL();

            Assert.NotNull(eMailTypeInfoList);
            Assert.True(eMailTypeInfoList.IsReadOnly);
            Assert.Equal(3, eMailTypeInfoList.Count);
        }
    }
}
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class ContactForSponsorRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void ContactForSponsorRORL_TestGetContactForSponsorRORL()
        {
            var categoryOfPersonTypeInfoList = await ContactForSponsorRORL.GetContactForSponsorRORL();

            Assert.NotNull(categoryOfPersonTypeInfoList);
            Assert.True(categoryOfPersonTypeInfoList.IsReadOnly);
            Assert.Equal(3, categoryOfPersonTypeInfoList.Count);
        }
    }
}
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class ContactForSponsorROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void ContactForSponsorROCL_TestGetContactForSponsorROCL()
        {
            var childData = MockDb.ContactForSponsors;
            var eMailTypeROCL = await ContactForSponsorROCL.GetContactForSponsorROCL(childData);

            Assert.NotNull(eMailTypeROCL);
            Assert.True(eMailTypeROCL.IsReadOnly);
            Assert.Equal(3, eMailTypeROCL.Count);
        }
    }
}
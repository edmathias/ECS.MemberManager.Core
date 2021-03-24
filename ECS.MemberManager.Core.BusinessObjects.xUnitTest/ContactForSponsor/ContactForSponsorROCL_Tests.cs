using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class ContactForSponsorROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void ContactForSponsorROCL_TestGetContactForSponsorROCL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IContactForSponsorDal>();
            var childData = await dal.Fetch();

            var eMailTypeROCL = await ContactForSponsorROCL.GetContactForSponsorROCL(childData);

            Assert.NotNull(eMailTypeROCL);
            Assert.True(eMailTypeROCL.IsReadOnly);
            Assert.Equal(3, eMailTypeROCL.Count);
        }
    }
}
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MemberInfoROR_Tests
    {
        [Fact]
        public async void MemberInfoROR_TestGetById()
        {
            var emailInfo = await MemberInfoROR.GetMemberInfoROR(1);

            Assert.NotNull(emailInfo);
            Assert.IsType<MemberInfoROR>(emailInfo);
            Assert.Equal(1, emailInfo.Id);
        }

        [Fact]
        public async void MemberInfoROR_TestGetMemberInfoROR()
        {
            const int ID_VALUE = 1;

            var emailTypeInfo = await MemberInfoROR.GetMemberInfoROR(ID_VALUE);

            Assert.NotNull(emailTypeInfo);
            Assert.IsType<MemberInfoROR>(emailTypeInfo);
            Assert.Equal(ID_VALUE, emailTypeInfo.Id);
        }
    }
}
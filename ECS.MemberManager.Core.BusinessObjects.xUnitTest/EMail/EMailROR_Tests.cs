using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EMailROR_Tests : CslaBaseTest
    {
        [Fact]
        public async void EMailROR_TestGetById()
        {
            var emailInfo = await EMailROR.GetEMailROR(1);

            Assert.NotNull(emailInfo);
            Assert.IsType<EMailROR>(emailInfo);
            Assert.Equal(1, emailInfo.Id);
        }

        [Fact]
        public async void EMailROR_TestGetEMailROR()
        {
            const int ID_VALUE = 1;

            var emailTypeInfo = await EMailROR.GetEMailROR(ID_VALUE);

            Assert.NotNull(emailTypeInfo);
            Assert.IsType<EMailROR>(emailTypeInfo);
            Assert.Equal(ID_VALUE, emailTypeInfo.Id);
        }
    }
}
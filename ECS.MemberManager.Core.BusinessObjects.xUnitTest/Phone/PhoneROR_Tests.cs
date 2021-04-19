using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PhoneROR_Tests : CslaBaseTest
    {
        [Fact]
        public async void PhoneROR_TestGetById()
        {
            var personalNote = await PhoneROR.GetPhoneROR(1);

            Assert.NotNull(personalNote);
            Assert.IsType<PhoneROR>(personalNote);
            Assert.Equal(1, personalNote.Id);
        }
    }
}
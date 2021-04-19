using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PersonalNoteROR_Tests
    {
        [Fact]
        public async void PersonalNoteROR_TestGetById()
        {
            var phone = await PersonalNoteROR.GetPersonalNoteROR(1);

            Assert.NotNull(phone);
            Assert.IsType<PersonalNoteROR>(phone);
            Assert.Equal(1, phone.Id);
        }
    }
}
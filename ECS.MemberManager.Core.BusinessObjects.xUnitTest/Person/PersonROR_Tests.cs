using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PersonROR_Tests : CslaBaseTest
    {
        [Fact]
        public async void PersonROR_TestGetById()
        {
            var personInfo = await PersonROR.GetPersonROR(1);

            Assert.NotNull(personInfo);
            Assert.IsType<PersonROR>(personInfo);
            Assert.Equal(1, personInfo.Id);
        }

        [Fact]
        public async void PersonROR_TestGetPersonROR()
        {
            const int ID_VALUE = 1;

            var personTypeInfo = await PersonROR.GetPersonROR(ID_VALUE);

            Assert.NotNull(personTypeInfo);
            Assert.IsType<PersonROR>(personTypeInfo);
            Assert.Equal(ID_VALUE, personTypeInfo.Id);
        }
    }
}
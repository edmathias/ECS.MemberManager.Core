using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MemberStatusROR_Tests : CslaBaseTest
    {
        [Fact]
        public async void MemberStatusROR_TestGetById()
        {
            var category = await MemberStatusROR.GetMemberStatusROR(1);

            Assert.NotNull(category);
            Assert.IsType<MemberStatusROR>(category);
            Assert.Equal(1, category.Id);
        }
    }
}
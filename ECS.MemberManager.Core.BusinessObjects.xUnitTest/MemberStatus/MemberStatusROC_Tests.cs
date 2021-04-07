using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MemberStatusROC_Tests : CslaBaseTest
    {
        [Fact]
        public async void MemberStatusROC_TestGetChild()
        {
            const int ID_VALUE = 999;

            var buildMemberStatus = BuildMemberStatus();
            buildMemberStatus.Id = ID_VALUE;

            var memberStatus = await MemberStatusROC.GetMemberStatusROC(buildMemberStatus);

            Assert.NotNull(memberStatus);
            Assert.IsType<MemberStatusROC>(memberStatus);
            Assert.Equal(memberStatus.Id, buildMemberStatus.Id);
            Assert.Equal(memberStatus.Description, buildMemberStatus.Description);
            Assert.Equal(memberStatus.Notes, buildMemberStatus.Notes);
            Assert.Equal(memberStatus.RowVersion, buildMemberStatus.RowVersion);
        }

        private MemberStatus BuildMemberStatus()
        {
            var memberStatus = new MemberStatus();
            memberStatus.Id = 1;
            memberStatus.Description = "test description";
            memberStatus.Notes = "notes for doctype";

            return memberStatus;
        }
    }
}
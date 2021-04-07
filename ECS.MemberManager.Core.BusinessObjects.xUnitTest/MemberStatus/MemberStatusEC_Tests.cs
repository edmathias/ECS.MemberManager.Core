using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MemberStatusEC_Tests : CslaBaseTest
    {
        [Fact]
        public async Task TestMemberStatusEC_NewMemberStatusEC()
        {
            var memberStatus = await MemberStatusEC.NewMemberStatusEC();

            Assert.NotNull(memberStatus);
            Assert.IsType<MemberStatusEC>(memberStatus);
            Assert.False(memberStatus.IsValid);
        }

        [Fact]
        public async Task TestMemberStatusEC_GetMemberStatusEC()
        {
            var memberStatusToLoad = BuildMemberStatus();
            var memberStatus = await MemberStatusEC.GetMemberStatusEC(memberStatusToLoad);

            Assert.NotNull(memberStatus);
            Assert.IsType<MemberStatusEC>(memberStatus);
            Assert.Equal(memberStatusToLoad.Id, memberStatus.Id);
            Assert.Equal(memberStatusToLoad.Description, memberStatus.Description);
            Assert.Equal(memberStatusToLoad.Notes, memberStatus.Notes);
            Assert.Equal(memberStatusToLoad.RowVersion, memberStatus.RowVersion);
            Assert.True(memberStatus.IsValid);
        }

        [Fact]
        public async Task TestMemberStatusEC_DescriptionRequired()
        {
            var memberStatusToTest = BuildMemberStatus();
            var memberStatus = await MemberStatusEC.GetMemberStatusEC(memberStatusToTest);
            var isObjectValidInit = memberStatus.IsValid;
            memberStatus.Description = string.Empty;

            Assert.NotNull(memberStatus);
            Assert.True(isObjectValidInit);
            Assert.False(memberStatus.IsValid);
            Assert.Equal("Description", memberStatus.BrokenRulesCollection[0].Property);
            Assert.Equal("Description required", memberStatus.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestMemberStatusEC_DescriptionCanNotExceed50Chars()
        {
            var memberStatusToTest = BuildMemberStatus();
            var memberStatus = await MemberStatusEC.GetMemberStatusEC(memberStatusToTest);
            var isObjectValidInit = memberStatus.IsValid;
            memberStatus.Description =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(memberStatus);
            Assert.True(isObjectValidInit);
            Assert.False(memberStatus.IsValid);
            Assert.Equal("Description", memberStatus.BrokenRulesCollection[0].Property);
            Assert.Equal("Description can not exceed 50 characters", memberStatus.BrokenRulesCollection[0].Description);
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
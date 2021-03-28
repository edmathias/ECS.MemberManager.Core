using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MemberInfoROC_Tests : CslaBaseTest
    {
        [Fact]
        public async void MemberInfoROC_Get()
        {
            var memberInfo = await MemberInfoROC.GetMemberInfoROC(await BuildMemberInfo());

            Assert.NotNull(memberInfo.Person);
            Assert.NotNull(memberInfo.MembershipType);
            Assert.NotNull(memberInfo.MemberStatus);
            Assert.NotNull(memberInfo.PrivacyLevel);
            Assert.Equal(1, memberInfo.Id);
        }

        private async Task<MemberInfo> BuildMemberInfo()
        {
            var memberInfo = new MemberInfo();
            memberInfo.Id = 1;
            memberInfo.Notes = "member info notes";
            memberInfo.MemberNumber = "9876543";
            memberInfo.Person = MockDb.Persons.Take(1).First();
            memberInfo.DateFirstJoined = DateTime.Now;
            memberInfo.MembershipType = MockDb.MembershipTypes.Take(1).First();
            memberInfo.PrivacyLevel = MockDb.PrivacyLevels.Take(1).First();
            memberInfo.MemberStatus = MockDb.MemberStatuses.Take(1).First();
            memberInfo.LastUpdatedBy = "edm";
            memberInfo.LastUpdatedDate = DateTime.Now;

            return memberInfo;
        }
    }
}
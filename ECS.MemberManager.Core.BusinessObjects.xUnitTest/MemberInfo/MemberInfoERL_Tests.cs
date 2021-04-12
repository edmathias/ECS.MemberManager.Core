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
    public class MemberInfoERL_Tests : CslaBaseTest
    {
        [Fact]
        private async void MemberInfoERL_TestNewMemberInfoList()
        {
            var memberInfoErl = await MemberInfoERL.NewMemberInfoERL();

            Assert.NotNull(memberInfoErl);
            Assert.IsType<MemberInfoERL>(memberInfoErl);
        }

        [Fact]
        private async void MemberInfoERL_TestGetMemberInfoERL()
        {
            var memberInfoEditList = await MemberInfoERL.GetMemberInfoERL();

            Assert.NotNull(memberInfoEditList);
            Assert.Equal(3, memberInfoEditList.Count);
        }

        [Fact]
        private async void MemberInfoERL_TestDeleteMemberInfosEntry()
        {
            var memberInfoErl = await MemberInfoERL.GetMemberInfoERL();
            var listCount = memberInfoErl.Count;
            var memberInfoToDelete = memberInfoErl.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = memberInfoErl.Remove(memberInfoToDelete);

            var memberInfoListAfterDelete = await memberInfoErl.SaveAsync();

            Assert.NotNull(memberInfoListAfterDelete);
            Assert.IsType<MemberInfoERL>(memberInfoListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount, memberInfoListAfterDelete.Count);
        }

        [Fact]
        private async void MemberInfoERL_TestUpdateMemberInfosEntry()
        {
            const int idToUpdate = 1;

            var memberInfoEditList = await MemberInfoERL.GetMemberInfoERL();
            var countBeforeUpdate = memberInfoEditList.Count;
            var memberInfoToUpdate = memberInfoEditList.First(a => a.Id == idToUpdate);
            memberInfoToUpdate.Notes = "This was updated";

            var updatedList = await memberInfoEditList.SaveAsync();

            Assert.Equal("This was updated", updatedList.First(a => a.Id == idToUpdate).Notes);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void MemberInfoERL_TestAddMemberInfoEntry()
        {
            var memberInfoEditList = await MemberInfoERL.GetMemberInfoERL();
            var countBeforeAdd = memberInfoEditList.Count;

            var memberInfoToAdd = await MemberInfoEC.GetMemberInfoEC(await BuildMemberInfo());

            memberInfoEditList.Add(memberInfoToAdd);
            var updatedMemberInfosList = await memberInfoEditList.SaveAsync();

            Assert.NotEqual(countBeforeAdd, updatedMemberInfosList.Count);
        }

        private  Task<MemberInfo> BuildMemberInfo()
        {
            var memberInfo = new MemberInfo();
            memberInfo.Notes = "member info notes";
            memberInfo.MemberNumber = "9876543";
            memberInfo.Person = MockDb.Persons.Take(1).First();
            memberInfo.DateFirstJoined = DateTime.Now;
            memberInfo.MembershipType = MockDb.MembershipTypes.Take(1).First();
            memberInfo.PrivacyLevel = MockDb.PrivacyLevels.Take(1).First();
            memberInfo.MemberStatus = MockDb.MemberStatuses.Take(1).First();
            memberInfo.LastUpdatedBy = "edm";
            memberInfo.LastUpdatedDate = DateTime.Now;

            return Task.FromResult(memberInfo);
        }
    }
}
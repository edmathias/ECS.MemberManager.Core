using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MemberInfoEC_Tests : CslaBaseTest
    {
        [Fact]
        public async void MemberInfoEC_Get()
        {
            var memberInfoObj = BuildMemberInfo();
            var memberInfo = await MemberInfoEC.GetMemberInfoEC(memberInfoObj);

            Assert.NotNull(memberInfo.Person);
            Assert.NotNull(memberInfo.MembershipType);
            Assert.NotNull(memberInfo.MemberStatus);
            Assert.NotNull(memberInfo.PrivacyLevel);
            Assert.Equal(memberInfoObj.Notes, memberInfo.Notes);
            Assert.Equal(memberInfoObj.MemberNumber, memberInfo.MemberNumber);
            Assert.Equal(new SmartDate(memberInfoObj.DateFirstJoined), memberInfo.DateFirstJoined);
            Assert.Equal(new SmartDate(memberInfoObj.LastUpdatedDate), memberInfo.LastUpdatedDate);
            Assert.Equal(memberInfoObj.LastUpdatedBy, memberInfo.LastUpdatedBy);
            Assert.Equal(1, memberInfo.Id);
            Assert.True(memberInfo.IsValid);
        }

        [Fact]
        public async void MemberInfoEC_GetNewObject()
        {
            var memberInfo = await MemberInfoEC.NewMemberInfoEC();

            Assert.NotNull(memberInfo);
            Assert.False(memberInfo.IsValid);
        }

        // test invalid state 
        [Fact]
        public async Task MemberInfoEC_MemberNumberRequired()
        {
            var memberInfo = await MemberInfoEC.NewMemberInfoEC();
            await BuildMemberInfoEC(memberInfo);
            var isObjectValidInit = memberInfo.IsValid;
            memberInfo.MemberNumber = String.Empty;

            Assert.NotNull(memberInfo);
            Assert.True(isObjectValidInit);
            Assert.False(memberInfo.IsValid);
        }

        [Fact]
        public async Task MemberInfoEC_MemberNumberExceedsMaxLengthOf35()
        {
            var memberInfo = await MemberInfoEC.NewMemberInfoEC();
            await BuildMemberInfoEC(memberInfo);
            Assert.True(memberInfo.IsValid);

            memberInfo.MemberNumber =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(memberInfo);
            Assert.False(memberInfo.IsValid);
            Assert.Equal("MemberNumber", memberInfo.BrokenRulesCollection[0].Property);
            Assert.Equal("MemberNumber can not exceed 35 characters",
                memberInfo.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestMemberInfoEC_DateFirstJoinedRequired()
        {
            var memberInfoToTest = BuildMemberInfo();
            var memberInfo = await MemberInfoEC.GetMemberInfoEC(memberInfoToTest);
            var isObjectValidInit = memberInfo.IsValid;
            memberInfo.DateFirstJoined = DateTime.MinValue;

            Assert.NotNull(memberInfo);
            Assert.True(isObjectValidInit);
            Assert.False(memberInfo.IsValid);
            Assert.Equal("DateFirstJoined", memberInfo.BrokenRulesCollection[0].Property);
            Assert.Equal("DateFirstJoined required", memberInfo.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestMemberInfoEC_MemberStatusRequired()
        {
            var memberInfoToTest = BuildMemberInfo();
            var memberInfo = await MemberInfoEC.GetMemberInfoEC(memberInfoToTest);
            var isObjectValidInit = memberInfo.IsValid;
            memberInfo.MemberStatus = null;

            Assert.NotNull(memberInfo);
            Assert.True(isObjectValidInit);
            Assert.False(memberInfo.IsValid);
            Assert.Equal("MemberStatus", memberInfo.BrokenRulesCollection[0].Property);
            Assert.Equal("MemberStatus required", memberInfo.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestMemberInfoEC_MembershipTypeRequired()
        {
            var memberInfoToTest = BuildMemberInfo();
            var memberInfo = await MemberInfoEC.GetMemberInfoEC(memberInfoToTest);
            var isObjectValidInit = memberInfo.IsValid;
            memberInfo.MembershipType = null;

            Assert.NotNull(memberInfo);
            Assert.True(isObjectValidInit);
            Assert.False(memberInfo.IsValid);
            Assert.Equal("MembershipType", memberInfo.BrokenRulesCollection[0].Property);
            Assert.Equal("MembershipType required", memberInfo.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestMemberInfoEC_LastUpdatedByRequired()
        {
            var memberInfoToTest = BuildMemberInfo();
            var memberInfo = await MemberInfoEC.GetMemberInfoEC(memberInfoToTest);
            var isObjectValidInit = memberInfo.IsValid;
            memberInfo.LastUpdatedBy = string.Empty;

            Assert.NotNull(memberInfo);
            Assert.True(isObjectValidInit);
            Assert.False(memberInfo.IsValid);
            Assert.Equal("LastUpdatedBy", memberInfo.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy required", memberInfo.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestMemberInfoEC_LastUpdatedByExceeds255Characters()
        {
            var memberInfoToTest = BuildMemberInfo();
            var memberInfo = await MemberInfoEC.GetMemberInfoEC(memberInfoToTest);
            var isObjectValidInit = memberInfo.IsValid;
            memberInfo.LastUpdatedBy = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempo" +
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud" +
                                       "exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure" +
                                       "dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.";

            Assert.NotNull(memberInfo);
            Assert.True(isObjectValidInit);
            Assert.False(memberInfo.IsValid);
            Assert.Equal("LastUpdatedBy", memberInfo.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy can not exceed 255 characters",
                memberInfo.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestMemberInfoEC_LastUpdatedDateRequired()
        {
            var memberInfoToTest = BuildMemberInfo();
            var memberInfo = await MemberInfoEC.GetMemberInfoEC(memberInfoToTest);
            var isObjectValidInit = memberInfo.IsValid;
            memberInfo.LastUpdatedDate = DateTime.MinValue;

            Assert.NotNull(memberInfo);
            Assert.True(isObjectValidInit);
            Assert.False(memberInfo.IsValid);
            Assert.Equal("LastUpdatedDate", memberInfo.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedDate required", memberInfo.BrokenRulesCollection[0].Description);
        }


        private async Task BuildMemberInfoEC(MemberInfoEC memberInfo)
        {
            var domainInfo = BuildMemberInfo();
            memberInfo.Notes = domainInfo.Notes;
            memberInfo.Person = await PersonEC.GetPersonEC(domainInfo.Person);
            memberInfo.MemberNumber = domainInfo.MemberNumber;
            memberInfo.MembershipType = await MembershipTypeEC.GetMembershipTypeEC(domainInfo.MembershipType);
            memberInfo.MemberStatus = await MemberStatusEC.GetMemberStatusEC(domainInfo.MemberStatus);
            memberInfo.PrivacyLevel = await PrivacyLevelEC.GetPrivacyLevelEC(domainInfo.PrivacyLevel);
            memberInfo.DateFirstJoined = new SmartDate(DateTime.Now);
            memberInfo.LastUpdatedBy = domainInfo.LastUpdatedBy;
            memberInfo.LastUpdatedDate = domainInfo.LastUpdatedDate;
        }

        private MemberInfo BuildMemberInfo()
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
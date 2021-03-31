using System;
using System.IO;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MembershipTypeEC_Tests : CslaBaseTest
    {
        [Fact]
        public async Task TestMembershipTypeEC_NewMembershipTypeEC()
        {
            var membershipTypeObj = await MembershipTypeEC.NewMembershipTypeEC();

            Assert.NotNull(membershipTypeObj);
            Assert.IsType<MembershipTypeEC>(membershipTypeObj);
            Assert.False(membershipTypeObj.IsValid);
        }

        [Fact]
        public async Task TestMembershipTypeEC_GetMembershipTypeEC()
        {
            var membershipTypeObjToLoad = BuildMembershipType();
            var membershipTypeObj = await MembershipTypeEC.GetMembershipTypeEC(membershipTypeObjToLoad);

            Assert.NotNull(membershipTypeObj);
            Assert.IsType<MembershipTypeEC>(membershipTypeObj);
            Assert.Equal(membershipTypeObjToLoad.Id, membershipTypeObj.Id);
            Assert.Equal(membershipTypeObjToLoad.Description, membershipTypeObj.Description);
            Assert.Equal(membershipTypeObjToLoad.LastUpdatedBy, membershipTypeObj.LastUpdatedBy);
            Assert.Equal(new SmartDate(membershipTypeObjToLoad.LastUpdatedDate), membershipTypeObj.LastUpdatedDate);
            Assert.Equal(membershipTypeObjToLoad.Notes, membershipTypeObj.Notes);
            Assert.Equal(membershipTypeObjToLoad.RowVersion, membershipTypeObj.RowVersion);
            Assert.True(membershipTypeObj.IsValid);
        }

        [Fact]
        public async Task TestMembershipTypeEC_DescriptionRequired()
        {
            var membershipTypeObjToTest = BuildMembershipType();
            var membershipTypeObj = await MembershipTypeEC.GetMembershipTypeEC(membershipTypeObjToTest);
            var isObjectValidInit = membershipTypeObj.IsValid;
            membershipTypeObj.Description = string.Empty;

            Assert.NotNull(membershipTypeObj);
            Assert.True(isObjectValidInit);
            Assert.False(membershipTypeObj.IsValid);
            Assert.Equal("Description", membershipTypeObj.BrokenRulesCollection[0].Property);
            Assert.Equal("Description required", membershipTypeObj.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestMembershipTypeEC_DescriptionNotGreaterThan50Chars()
        {
            var membershipTypeObjToTest = BuildMembershipType();
            var membershipTypeObj = await MembershipTypeEC.GetMembershipTypeEC(membershipTypeObjToTest);
            var isObjectValidInit = membershipTypeObj.IsValid;
            membershipTypeObj.Description =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(membershipTypeObj);
            Assert.True(isObjectValidInit);
            Assert.False(membershipTypeObj.IsValid);
            Assert.Equal("Description", membershipTypeObj.BrokenRulesCollection[0].Property);
            Assert.Equal("Description can not exceed 50 characters",
                membershipTypeObj.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestMembershipTypeEC_LastUpdatedByRequired()
        {
            var membershipTypeObjToTest = BuildMembershipType();
            var membershipTypeObj = await MembershipTypeEC.GetMembershipTypeEC(membershipTypeObjToTest);
            var isObjectValidInit = membershipTypeObj.IsValid;
            membershipTypeObj.LastUpdatedBy = string.Empty;

            Assert.NotNull(membershipTypeObj);
            Assert.True(isObjectValidInit);
            Assert.False(membershipTypeObj.IsValid);
            Assert.Equal("LastUpdatedBy", membershipTypeObj.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy required", membershipTypeObj.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestMembershipTypeEC_LastUpdatedByLengthNotGreaterThan255Characters()
        {
            var membershipTypeObjToTest = BuildMembershipType();
            var membershipTypeObj = await MembershipTypeEC.GetMembershipTypeEC(membershipTypeObjToTest);
            var isObjectValidInit = membershipTypeObj.IsValid;
            membershipTypeObj.LastUpdatedBy =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(membershipTypeObj);
            Assert.True(isObjectValidInit);
            Assert.False(membershipTypeObj.IsValid);
            Assert.Equal("LastUpdatedBy", membershipTypeObj.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy can not exceed 255 characters",
                membershipTypeObj.BrokenRulesCollection[0].Description);
        }

        private MembershipType BuildMembershipType()
        {
            var membershipTypeObj = new MembershipType();
            membershipTypeObj.Id = 1;
            membershipTypeObj.Description = "test description";
            membershipTypeObj.Level = 1;
            membershipTypeObj.LastUpdatedBy = "edm";
            membershipTypeObj.LastUpdatedDate = DateTime.Now;
            membershipTypeObj.Notes = "notes for membership type";

            return membershipTypeObj;
        }
    }
}
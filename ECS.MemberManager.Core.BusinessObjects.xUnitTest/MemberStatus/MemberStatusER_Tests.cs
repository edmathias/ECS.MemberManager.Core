﻿using System;
using System.IO;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MemberStatusER_Tests : CslaBaseTest
    {
        [Fact]
        public async Task MemberStatusER_TestGetMemberStatus()
        {
            var memberStatus = await MemberStatusER.GetMemberStatusER(1);

            Assert.NotNull(memberStatus);
            Assert.IsType<MemberStatusER>(memberStatus);
        }

        [Fact]
        public async Task MemberStatusER_TestGetNewMemberStatusER()
        {
            var memberStatus = await MemberStatusER.NewMemberStatusER();

            Assert.NotNull(memberStatus);
            Assert.False(memberStatus.IsValid);
        }

        [Fact]
        public async Task MemberStatusER_TestUpdateExistingMemberStatusER()
        {
            var memberStatus = await MemberStatusER.GetMemberStatusER(1);
            memberStatus.Notes = "These are updated Notes";

            var result = await memberStatus.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes", result.Notes);
        }

        [Fact]
        public async Task MemberStatusER_TestInsertNewMemberStatusER()
        {
            var memberStatus = await MemberStatusER.NewMemberStatusER();
            memberStatus.Description = "Standby";
            memberStatus.Notes = "This person is on standby";

            var savedMemberStatus = await memberStatus.SaveAsync();

            Assert.NotNull(savedMemberStatus);
            Assert.IsType<MemberStatusER>(savedMemberStatus);
            Assert.True(savedMemberStatus.Id > 0);
        }

        [Fact]
        public async Task MemberStatusER_TestDeleteObjectFromDatabase()
        {
            const int ID_TO_DELETE = 99;

            await MemberStatusER.DeleteMemberStatusER(ID_TO_DELETE);

            await Assert.ThrowsAsync<DataPortalException>(() => MemberStatusER.GetMemberStatusER(ID_TO_DELETE));
        }

        // test invalid state 
        [Fact]
        public async Task MemberStatusER_TestDescriptionRequired()
        {
            var memberStatus = await MemberStatusER.NewMemberStatusER();
            memberStatus.Description = "make valid";
            var isObjectValidInit = memberStatus.IsValid;
            memberStatus.Description = string.Empty;

            Assert.NotNull(memberStatus);
            Assert.True(isObjectValidInit);
            Assert.False(memberStatus.IsValid);
            Assert.Equal("Description", memberStatus.BrokenRulesCollection[0].Property);
            Assert.Equal("Description required", memberStatus.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task MemberStatusER_TestDescriptionCanNotExceed50Characters()
        {
            var memberStatus = await MemberStatusER.NewMemberStatusER();
            memberStatus.Description = "valid length";
            Assert.True(memberStatus.IsValid);

            memberStatus.Description =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(memberStatus);
            Assert.False(memberStatus.IsValid);
            Assert.Equal("Description", memberStatus.BrokenRulesCollection[0].Property);
            Assert.Equal("Description can not exceed 50 characters", memberStatus.BrokenRulesCollection[0].Description);
        }
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task MemberStatusER_TestInvalidSaveMemberStatusER()
        {
            var memberStatus = await MemberStatusER.NewMemberStatusER();
            memberStatus.Description = String.Empty;
            MemberStatusER savedMemberStatus = null;

            Assert.False(memberStatus.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedMemberStatus = memberStatus.Save());
        }
    }
}
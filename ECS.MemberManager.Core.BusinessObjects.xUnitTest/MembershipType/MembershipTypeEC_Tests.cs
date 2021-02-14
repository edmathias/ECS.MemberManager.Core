﻿using System;
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
    public class MembershipTypeEC_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public MembershipTypeEC_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");

            if (testLibrary == "Mock")
                MockDb.ResetMockDb();
            else
            {
                if (!IsDatabaseBuilt)
                {
                    var adoDb = new ADODb();
                    adoDb.BuildMemberManagerADODb();
                    IsDatabaseBuilt = true;
                }
            }
        }

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
            Assert.Equal(membershipTypeObjToLoad.Id,membershipTypeObj.Id);
            Assert.Equal(membershipTypeObjToLoad.Description,membershipTypeObj.Description);
            Assert.Equal(membershipTypeObjToLoad.LastUpdatedBy, membershipTypeObj.LastUpdatedBy);
            Assert.Equal(new SmartDate(membershipTypeObjToLoad.LastUpdatedDate), membershipTypeObj.LastUpdatedDate);
            Assert.Equal(membershipTypeObjToLoad.Notes, membershipTypeObj.Notes);
            Assert.Equal(membershipTypeObjToLoad.RowVersion, membershipTypeObj.RowVersion);
            Assert.True(membershipTypeObj.IsValid);
        }

        [Fact]
        public async Task TestMembershipTypeEC_DescriptionLessThan50Chars()
        {
            var membershipTypeObjToTest = BuildMembershipType();
            var membershipTypeObj = await MembershipTypeEC.GetMembershipTypeEC(membershipTypeObjToTest);
            var isObjectValidInit = membershipTypeObj.IsValid;
            membershipTypeObj.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(membershipTypeObj);
            Assert.True(isObjectValidInit);
            Assert.False(membershipTypeObj.IsValid);
            Assert.Equal("Description",membershipTypeObj.BrokenRulesCollection[0].Property);
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
            Assert.Equal("LastUpdatedBy",membershipTypeObj.BrokenRulesCollection[0].Property);
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
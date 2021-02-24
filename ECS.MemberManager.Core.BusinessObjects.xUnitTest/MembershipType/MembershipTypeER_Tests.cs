﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MembershipTypeER_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public MembershipTypeER_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");
            
            if(testLibrary == "Mock")
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
        public async Task MembershipTypeER_TestGetMembershipType()
        {
            var membershipTypeObj = await MembershipTypeER.GetMembershipTypeER(1);

            Assert.NotNull(membershipTypeObj);
            Assert.IsType<MembershipTypeER>(membershipTypeObj);
        }

        [Fact]
        public async Task MembershipTypeER_TestGetNewMembershipTypeER()
        {
            var membershipTypeObj = await MembershipTypeER.NewMembershipTypeER();

            Assert.NotNull(membershipTypeObj);
            Assert.False(membershipTypeObj.IsValid);
        }

        [Fact]
        public async Task MembershipTypeER_TestUpdateExistingMembershipTypeER()
        {
            var membershipTypeObj = await MembershipTypeER.GetMembershipTypeER(1);
            membershipTypeObj.Notes = "These are updated Notes";
            
            var result =  await membershipTypeObj.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes",result.Notes );
        }

        [Fact]
        public async Task MembershipTypeER_TestInsertNewMembershipTypeER()
        {
            var membershipTypeObj = await MembershipTypeER.NewMembershipTypeER();
            membershipTypeObj.Description = "Type name";
            membershipTypeObj.Level = 1;
            membershipTypeObj.Notes = "This person is on standby";
            membershipTypeObj.LastUpdatedBy = "edm";
            membershipTypeObj.LastUpdatedDate = DateTime.Now;

            var savedMembershipType = await membershipTypeObj.SaveAsync();
           
            Assert.NotNull(savedMembershipType);
            Assert.IsType<MembershipTypeER>(savedMembershipType);
            Assert.True( savedMembershipType.Id > 0 );
        }

        [Fact]
        public async Task MembershipTypeER_TestDeleteObjectFromDatabase()
        {
            const int ID_TO_DELETE = 99;
            
            await MembershipTypeER.DeleteMembershipTypeER(ID_TO_DELETE);

            await Assert.ThrowsAsync<DataPortalException>(() => MembershipTypeER.GetMembershipTypeER(ID_TO_DELETE));
        }
        
        // test invalid state 
        [Fact]
        public async Task MembershipTypeER_TestDescriptionRequired() 
        {
            var membershipTypeObj = await MembershipTypeER.NewMembershipTypeER();
            membershipTypeObj.Description = "make valid";
            membershipTypeObj.LastUpdatedBy = "edm";
            membershipTypeObj.LastUpdatedDate = DateTime.Now;
            var isObjectValidInit = membershipTypeObj.IsValid;
            membershipTypeObj.Description = string.Empty;

            Assert.NotNull(membershipTypeObj);
            Assert.True(isObjectValidInit);
            Assert.False(membershipTypeObj.IsValid);
        }
       
        [Fact]
        public async Task MembershipTypeER_TestDescriptionExceedsMaxLengthOf50()
        {
            var membershipTypeObj = await MembershipTypeER.NewMembershipTypeER();
            membershipTypeObj.LastUpdatedBy = "edm";
            membershipTypeObj.LastUpdatedDate = DateTime.Now;
            membershipTypeObj.Description = "valid length";
            Assert.True(membershipTypeObj.IsValid);
            
            membershipTypeObj.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                   "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                   "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                   "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(membershipTypeObj);
            Assert.False(membershipTypeObj.IsValid);
            Assert.Equal("Description",membershipTypeObj.BrokenRulesCollection[0].Property);
            Assert.Equal("Description can not exceed 50 characters",membershipTypeObj.BrokenRulesCollection[0].Description);
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task MembershipTypeER_TestInvalidSaveMembershipTypeER()
        {
            var membershipTypeObj = await MembershipTypeER.NewMembershipTypeER();
            membershipTypeObj.Description = String.Empty;
            MembershipTypeER savedMembershipType = null;
                
            Assert.False(membershipTypeObj.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedMembershipType =  membershipTypeObj.Save() );
        }
    }
}

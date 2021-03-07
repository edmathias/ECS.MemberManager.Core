using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MemberInfoER_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public MemberInfoER_Tests()
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
        public async void MemberInfoER_Get()
        {
            var memberInfo = await MemberInfoER.GetMemberInfoER(1);

            Assert.NotNull(memberInfo.Person);
            Assert.NotNull(memberInfo.MembershipType);
            Assert.NotNull(memberInfo.MemberStatus);
            Assert.NotNull(memberInfo.PrivacyLevel);
            Assert.Equal(1, memberInfo.Id);
            Assert.True(memberInfo.IsValid);
        }

        [Fact]
        public async void MemberInfoER_GetNewObject()
        {
            var memberInfo = await MemberInfoER.NewMemberInfoER();

            Assert.NotNull(memberInfo);
            Assert.False(memberInfo.IsValid);
        }

        [Fact]
        public async void MemberInfoER_UpdateExistingObjectInDatabase()
        {
            var newMemberNumber = "1234567890";
            var memberInfo = await MemberInfoER.GetMemberInfoER(1);
            memberInfo.MemberNumber = newMemberNumber;
            
            var result = await memberInfo.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal(newMemberNumber,result.MemberNumber);
        }

        [Fact]
        public async Task MemberInfoER_InsertNewObjectIntoDatabase()
        {
            var memberInfo = await MemberInfoER.NewMemberInfoER();
            await BuildMemberInfoER(memberInfo);

            var savedMemberInfo = await memberInfo.SaveAsync();
           
            Assert.NotNull(savedMemberInfo);
            Assert.IsType<MemberInfoER>(savedMemberInfo);
            Assert.True( savedMemberInfo.Id > 0 );
        }

        [Fact]
        public async Task MemberInfoER_DeleteObjectFromDatabase()
        {
            const int ID_TO_DELETE = 99;
            
            await MemberInfoER.DeleteMemberInfoER(ID_TO_DELETE);
            
            var categoryToCheck = await Assert.ThrowsAsync<Csla.DataPortalException>
                (() => MemberInfoER.GetMemberInfoER(ID_TO_DELETE));        
        }
     
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task MemberInfoER_TestInvalidSave()
        {
            var memberInfo = await MemberInfoER.NewMemberInfoER();
            memberInfo.MemberNumber = String.Empty;
                
            Assert.False(memberInfo.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => memberInfo.Save());
        }
        // test invalid state 
        [Fact]
        public async Task MemberInfoER_MemberNumberRequired() 
        {
            var memberInfo = await MemberInfoER.NewMemberInfoER();
            await BuildMemberInfoER(memberInfo);
            var isObjectValidInit = memberInfo.IsValid;
            memberInfo.MemberNumber = String.Empty;

            Assert.NotNull(memberInfo);
            Assert.True(isObjectValidInit);
            Assert.False(memberInfo.IsValid);
        }
       
        [Fact]
        public async Task MemberInfoER_MemberNumberExceedsMaxLengthOf35()
        {
            var memberInfo = await MemberInfoER.NewMemberInfoER();
            await BuildMemberInfoER(memberInfo);
            Assert.True(memberInfo.IsValid);

            memberInfo.MemberNumber =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(memberInfo);
            Assert.False(memberInfo.IsValid);
            Assert.Equal("MemberNumber",memberInfo.BrokenRulesCollection[0].Property);
            Assert.Equal("MemberNumber can not exceed 35 characters",
                memberInfo.BrokenRulesCollection[0].Description);
 
        }        
        
        [Fact]
        public async Task TestMemberInfoER_DateFirstJoinedRequired()
        {
            var memberInfoToTest = await BuildMemberInfo();
            var memberInfo = await MemberInfoER.GetMemberInfoER(1);
            var isObjectValidInit = memberInfo.IsValid;
            memberInfo.DateFirstJoined = DateTime.MinValue;

            Assert.NotNull(memberInfo);
            Assert.True(isObjectValidInit);
            Assert.False(memberInfo.IsValid);
            Assert.Equal("DateFirstJoined",memberInfo.BrokenRulesCollection[0].Property);
            Assert.Equal("DateFirstJoined required",memberInfo.BrokenRulesCollection[0].Description);
            
        }        

        [Fact]
        public async Task TestMemberInfoER_MemberStatusRequired()
        {
            var memberInfoToTest = await BuildMemberInfo();
            var memberInfo = await MemberInfoER.GetMemberInfoER(1);
            var isObjectValidInit = memberInfo.IsValid;
            memberInfo.MemberStatus = null;

            Assert.NotNull(memberInfo);
            Assert.True(isObjectValidInit);
            Assert.False(memberInfo.IsValid);
            Assert.Equal("MemberStatus",memberInfo.BrokenRulesCollection[0].Property);
            Assert.Equal("MemberStatus required",memberInfo.BrokenRulesCollection[0].Description);
            
        }             
       
        [Fact]
        public async Task TestMemberInfoER_MembershipTypeRequired()
        {
            var memberInfoToTest = await BuildMemberInfo();
            var memberInfo = await MemberInfoER.GetMemberInfoER(1);
            var isObjectValidInit = memberInfo.IsValid;
            memberInfo.MembershipType = null;

            Assert.NotNull(memberInfo);
            Assert.True(isObjectValidInit);
            Assert.False(memberInfo.IsValid);
            Assert.Equal("MembershipType",memberInfo.BrokenRulesCollection[0].Property);
            Assert.Equal("MembershipType required",memberInfo.BrokenRulesCollection[0].Description);
            
        }            
        [Fact]
        public async Task TestMemberInfoER_LastUpdatedByRequired()
        {
            var memberInfoToTest = await BuildMemberInfo();
            var memberInfo = await MemberInfoER.GetMemberInfoER(1);
            var isObjectValidInit = memberInfo.IsValid;
            memberInfo.LastUpdatedBy = string.Empty;

            Assert.NotNull(memberInfo);
            Assert.True(isObjectValidInit);
            Assert.False(memberInfo.IsValid);
            Assert.Equal("LastUpdatedBy",memberInfo.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy required",memberInfo.BrokenRulesCollection[0].Description);
            
        }
     
        [Fact]
        public async Task TestMemberInfoER_LastUpdatedByExceeds255Characters()
        {
            var memberInfoToTest = await BuildMemberInfo();
            var memberInfo = await MemberInfoER.GetMemberInfoER(1);
            var isObjectValidInit = memberInfo.IsValid;
            memberInfo.LastUpdatedBy = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempo" +
                                      "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud" +
                                      "exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure" +
                                      "dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.";

            Assert.NotNull(memberInfo);
            Assert.True(isObjectValidInit);
            Assert.False(memberInfo.IsValid);
            Assert.Equal("LastUpdatedBy",memberInfo.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy can not exceed 255 characters",memberInfo.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestMemberInfoER_LastUpdatedDateRequired()
        {
            var memberInfoToTest = await BuildMemberInfo();
            var memberInfo = await MemberInfoER.GetMemberInfoER(1);
            var isObjectValidInit = memberInfo.IsValid;
            memberInfo.LastUpdatedDate = DateTime.MinValue;

            Assert.NotNull(memberInfo);
            Assert.True(isObjectValidInit);
            Assert.False(memberInfo.IsValid);
            Assert.Equal("LastUpdatedDate",memberInfo.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedDate required",memberInfo.BrokenRulesCollection[0].Description);
            
        }        


        private async Task BuildMemberInfoER(MemberInfoER memberInfo )
        {
            var domainInfo = await BuildMemberInfo();
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

        private async Task<MemberInfo> BuildMemberInfo()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();

            var memberInfo = new MemberInfo();
            memberInfo.Notes = "member info notes";
            memberInfo.MemberNumber = "9876543";
            memberInfo.Person = await dal.Fetch(1);
            var dal2 = dalManager.GetProvider<IMembershipTypeDal>();
            memberInfo.MembershipType = await dal2.Fetch(1);
            var dal3 = dalManager.GetProvider<IMemberStatusDal>();
            memberInfo.MemberStatus = await dal3.Fetch(1);
            var dal4 = dalManager.GetProvider<IPrivacyLevelDal>();
            memberInfo.PrivacyLevel = await dal4.Fetch(1);
            memberInfo.LastUpdatedBy = "edm";
            memberInfo.LastUpdatedDate = DateTime.Now	;

            return memberInfo;
        }
    }
}

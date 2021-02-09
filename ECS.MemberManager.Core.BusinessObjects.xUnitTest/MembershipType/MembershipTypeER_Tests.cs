using System;
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
            var eventObj = await MembershipTypeER.GetMembershipTypeER(1);

            Assert.NotNull(eventObj);
            Assert.IsType<MembershipTypeER>(eventObj);
        }

        [Fact]
        public async Task MembershipTypeER_TestGetNewMembershipTypeER()
        {
            var eventObj = await MembershipTypeER.NewMembershipTypeER();

            Assert.NotNull(eventObj);
            Assert.False(eventObj.IsValid);
        }

        [Fact]
        public async Task MembershipTypeER_TestUpdateExistingMembershipTypeER()
        {
            var eventObj = await MembershipTypeER.GetMembershipTypeER(1);
            eventObj.Notes = "These are updated Notes";
            
            var result =  await eventObj.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes",result.Notes );
        }

        [Fact]
        public async Task MembershipTypeER_TestInsertNewMembershipTypeER()
        {
            var eventObj = await MembershipTypeER.NewMembershipTypeER();
            eventObj.Description = "Type name";
            eventObj.Level = 1;
            eventObj.Notes = "This person is on standby";
            eventObj.LastUpdatedBy = "edm";
            eventObj.LastUpdatedDate = DateTime.Now;

            var savedMembershipType = await eventObj.SaveAsync();
           
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
            var eventObj = await MembershipTypeER.NewMembershipTypeER();
            eventObj.Description = "make valid";
            eventObj.LastUpdatedBy = "edm";
            eventObj.LastUpdatedDate = DateTime.Now;
            var isObjectValidInit = eventObj.IsValid;
            eventObj.Description = string.Empty;

            Assert.NotNull(eventObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventObj.IsValid);
        }
       
        [Fact]
        public async Task MembershipTypeER_TestDescriptionExceedsMaxLengthOf50()
        {
            var eventObj = await MembershipTypeER.NewMembershipTypeER();
            eventObj.LastUpdatedBy = "edm";
            eventObj.LastUpdatedDate = DateTime.Now;
            eventObj.Description = "valid length";
            Assert.True(eventObj.IsValid);
            
            eventObj.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                   "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                   "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                   "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(eventObj);
            Assert.False(eventObj.IsValid);
            Assert.Equal("The field Description must be a string or array type with a maximum length of '50'.",
                eventObj.BrokenRulesCollection[0].Description);
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task MembershipTypeER_TestInvalidSaveMembershipTypeER()
        {
            var eventObj = await MembershipTypeER.NewMembershipTypeER();
            eventObj.Description = String.Empty;
            MembershipTypeER savedMembershipType = null;
                
            Assert.False(eventObj.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedMembershipType =  eventObj.Save() );
        }
    }
}

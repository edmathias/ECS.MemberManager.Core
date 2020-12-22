using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PrivacyLevelER_Tests 
    {
        public PrivacyLevelER_Tests()
        {
            MockDb.ResetMockDb();
        }
        
        [Fact]
        public async Task TestPrivacyLevelER_Get()
        {
            var privacyLevel = await PrivacyLevelER.GetPrivacyLevel(1);

            Assert.Equal(1, privacyLevel.Id);
            Assert.True(privacyLevel.IsValid);
        }

        [Fact]
        public async Task TestPrivacyLevelER_GetNewObject()
        {
            var privacyLevel = await PrivacyLevelER.NewPrivacyLevel();

            Assert.NotNull(privacyLevel);
            Assert.False(privacyLevel.IsValid);
        }

        [Fact]
        public async Task TestPrivacyLevelER_UpdateExistingObjectInDatabase()
        {
            var privacyLevel = await PrivacyLevelER.GetPrivacyLevel(1);
            privacyLevel.Notes = "These are updated Notes";
            
            var result = privacyLevel.Save();

            Assert.NotNull(result);
            Assert.Equal( "These are updated Notes", result.Notes);
        }

        [Fact]
        public async Task TestPrivacyLevelER_InsertNewObjectIntoDatabase()
        {
            var privacyLevel = await PrivacyLevelER.NewPrivacyLevel();
            privacyLevel.Description = "Standby";
            privacyLevel.Notes = "This person is on standby";

            var savedPrivacyLevel = privacyLevel.Save();
           
            Assert.NotNull(savedPrivacyLevel);
            Assert.IsType<PrivacyLevelER>(savedPrivacyLevel);
            Assert.True( savedPrivacyLevel.Id > 0 );
        }

        [Fact]
        public async Task TestPrivacyLevelER_DeleteObjectFromDatabase()
        {
            int beforeCount = MockDb.PrivacyLevels.Count();

            await PrivacyLevelER.DeletePrivacyLevel(1);
            
            Assert.NotEqual(beforeCount,MockDb.PrivacyLevels.Count());
        }
        
        // test invalid state 
        [Fact]
        public async Task TestPrivacyLevelER_DescriptionRequired() 
        {
            var privacyLevel = await PrivacyLevelER.NewPrivacyLevel();
            privacyLevel.Description = "make valid";
            var isObjectValidInit = privacyLevel.IsValid;
            privacyLevel.Description = string.Empty;

            Assert.NotNull(privacyLevel);
            Assert.True(isObjectValidInit);
            Assert.False(privacyLevel.IsValid);
        }
       
        [Fact]
        public async Task TestPrivacyLevelER_DescriptionExceedsMaxLengthOf50()
        {
            var privacyLevel = await PrivacyLevelER.NewPrivacyLevel();
            privacyLevel.Description = "valid length";
            Assert.True(privacyLevel.IsValid);
            
            privacyLevel.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(privacyLevel);
            Assert.False(privacyLevel.IsValid);
            Assert.Equal("The field Description must be a string or array type with a maximum length of '50'.",
                privacyLevel.BrokenRulesCollection[0].Description);
 
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestPrivacyLevelER_TestInvalidSave()
        {
            var privacyLevel = await PrivacyLevelER.NewPrivacyLevel();
            privacyLevel.Description = String.Empty;
                
            Assert.False(privacyLevel.IsValid);
            Assert.Throws<ValidationException>(() => privacyLevel.Save() );
        }
    }
}

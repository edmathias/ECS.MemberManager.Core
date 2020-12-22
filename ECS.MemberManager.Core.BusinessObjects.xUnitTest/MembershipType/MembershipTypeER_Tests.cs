using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MembershipTypeER_Tests 
    {
        public MembershipTypeER_Tests()
        {
            MockDb.ResetMockDb();
        }
        
        [Fact]
        public async Task TestMembershipTypeER_Get()
        {
            var membershipType = await MembershipTypeER.GetMembershipType(1);

            Assert.Equal(1,membershipType.Id);
            Assert.True(membershipType.IsValid);
        }

        [Fact]
        public async Task TestMembershipTypeER_GetNewObject()
        {
            var membershipType = await MembershipTypeER.NewMembershipType();

            Assert.NotNull(membershipType);
            Assert.False(membershipType.IsValid);
        }

        [Fact]
        public async Task TestMembershipTypeER_UpdateExistingObjectInDatabase()
        {
            var membershipType = await MembershipTypeER.GetMembershipType(1);
            membershipType.Notes = "These are updated Notes";
            
            var result = membershipType.Save();

            Assert.NotNull(result);
            Assert.Equal( "These are updated Notes",result.Notes);
        }

        [Fact]
        public async Task TestMembershipTypeER_InsertNewObjectIntoDatabase()
        {
            var membershipType = await MembershipTypeER.NewMembershipType();
            membershipType.Description = "Standby";
            membershipType.Notes = "This person is on standby";
            membershipType.LastUpdatedBy = "edm";
            membershipType.LastUpdatedDate = DateTime.Now;

            var savedMembershipType = membershipType.Save();
           
            Assert.NotNull(savedMembershipType);
            Assert.IsType<MembershipTypeER>(savedMembershipType);
            Assert.True( savedMembershipType.Id > 0 );
        }

        [Fact]
        public async Task TestMembershipTypeER_DeleteObjectFromDatabase()
        {
            int beforeCount = MockDb.MembershipTypes.Count();
            
            await MembershipTypeER.DeleteMembershipType(99);
            
            Assert.NotEqual(beforeCount,MockDb.MembershipTypes.Count());
        }
        
        // test invalid state 
        [Fact]
        public async Task TestMembershipTypeER_DescriptionRequired()
        {
            var membershipType = await MembershipTypeER.NewMembershipType();
            membershipType.Description = "make valid";
            membershipType.Level = 2;
            membershipType.LastUpdatedBy = "edm";
            membershipType.LastUpdatedDate = DateTime.Now;
            var isObjectValidInit = membershipType.IsValid;
            membershipType.Description = string.Empty;

            Assert.NotNull(membershipType);
            Assert.True(isObjectValidInit);
            Assert.False(membershipType.IsValid);
        }
       
        [Fact]
        public async Task TestMembershipTypeER_DescriptionExceedsMaxLengthOf50()
        {
            var membershipType = await MembershipTypeER.NewMembershipType();
            membershipType.Level = 1;
            membershipType.LastUpdatedBy = "edm";
            membershipType.LastUpdatedDate = DateTime.Now;
            membershipType.Description = "valid length";
            Assert.True(membershipType.IsValid);
            
            membershipType.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(membershipType);
            Assert.False(membershipType.IsValid);
            Assert.Equal("The field Description must be a string or array type with a maximum length of '50'.",
                membershipType.BrokenRulesCollection[0].Description);
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestMembershipTypeER_TestInvalidSave()
        {
            var membershipType = await MembershipTypeER.NewMembershipType();
            membershipType.Description = String.Empty;
            bool exceptionThrown = false;
            try
            {
                var savedMembershipType = await membershipType.SaveAsync();
            }
            catch (ValidationException)
            {
                exceptionThrown = true;
            }
            
            Assert.False(membershipType.IsValid);
            Assert.True(exceptionThrown);
        }
    }
}

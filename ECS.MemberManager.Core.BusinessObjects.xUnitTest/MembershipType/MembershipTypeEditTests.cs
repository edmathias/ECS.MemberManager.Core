using System;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MembershipTypeEditTests 
    {
        public MembershipTypeEditTests()
        {
            MockDb.ResetMockDb();
        }
        
        [Fact]
        public async Task TestMembershipTypeEdit_Get()
        {
            var membershipType = await MembershipTypeEdit.GetMembershipTypeEdit(1);

            Assert.Equal(1,membershipType.Id);
            Assert.True(membershipType.IsValid);
        }

        [Fact]
        public async Task TestMembershipTypeEdit_GetNewObject()
        {
            var membershipType = await MembershipTypeEdit.NewMembershipTypeEdit();

            Assert.NotNull(membershipType);
            Assert.False(membershipType.IsValid);
        }

        [Fact]
        public async Task TestMembershipTypeEdit_UpdateExistingObjectInDatabase()
        {
            var membershipType = await MembershipTypeEdit.GetMembershipTypeEdit(1);
            membershipType.Notes = "These are updated Notes";
            
            var result = await membershipType.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal( "These are updated Notes",result.Notes);
        }

        [Fact]
        public async Task TestMembershipTypeEdit_InsertNewObjectIntoDatabase()
        {
            var membershipType = await MembershipTypeEdit.NewMembershipTypeEdit();
            membershipType.Description = "Standby";
            membershipType.Notes = "This person is on standby";
            membershipType.LastUpdatedBy = "edm";
            membershipType.LastUpdatedDate = DateTime.Now;

            var savedMembershipType = await membershipType.SaveAsync();
           
            Assert.NotNull(savedMembershipType);
            Assert.IsType<MembershipTypeEdit>(savedMembershipType);
            Assert.True( savedMembershipType.Id > 0 );
        }

        [Fact]
        public async Task TestMembershipTypeEdit_DeleteObjectFromDatabase()
        {
            const int ID_TO_DELETE = 99;
            await MembershipTypeEdit.DeleteMembershipTypeEdit(ID_TO_DELETE);

            await Assert.ThrowsAsync<DataPortalException>( () => MembershipTypeEdit.GetMembershipTypeEdit(ID_TO_DELETE));
        }
        
        [Fact]
        public async Task MembershipTypeEdit_TestSaveOutOfOrder()
        {
            var membershipType1 = await MembershipTypeEdit.GetMembershipTypeEdit(1);
            var membershipType2 = await MembershipTypeEdit.GetMembershipTypeEdit(1);
            membershipType1.Notes = $"set up timestamp issue {DateTime.Now}";  // turn on IsDirty
            membershipType2.Notes = $"set up timestamp issue {DateTime.Now}";

            var savedMembershipType = await membershipType2.SaveAsync();
            
            Assert.NotEqual(savedMembershipType.RowVersion, membershipType1.RowVersion);
            await Assert.ThrowsAsync<DataPortalException>(() => membershipType1.SaveAsync());
        }
        
        
        // test invalid state 
        [Fact]
        public async Task TestMembershipTypeEdit_DescriptionRequired()
        {
            var membershipType = await MembershipTypeEdit.NewMembershipTypeEdit();
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
        public async Task TestMembershipTypeEdit_DescriptionExceedsMaxLengthOf50()
        {
            var membershipType = await MembershipTypeEdit.NewMembershipTypeEdit();
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
        public async Task TestMembershipTypeEdit_TestInvalidSave()
        {
            var membershipType = await MembershipTypeEdit.NewMembershipTypeEdit();
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

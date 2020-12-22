using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MemberStatusER_Tests 
    {
        public MemberStatusER_Tests()
        {
            MockDb.ResetMockDb();
        }

        [Fact]
        public async Task TestMemberStatusER_Get()
        {
            var memberStatus = await MemberStatusER.GetMemberStatus(1);

            Assert.Equal(1,memberStatus.Id);
            Assert.True(memberStatus.IsValid);
        }

        [Fact]
        public async Task TestMemberStatusER_New()
        {
            var memberStatus = await MemberStatusER.NewMemberStatus();

            Assert.NotNull(memberStatus);
            Assert.False(memberStatus.IsValid);
        }

        [Fact]
        public async Task TestMemberStatusER_Update()
        {
            var memberStatus = await MemberStatusER.GetMemberStatus(1);
            memberStatus.Notes = "These are updated Notes";
            
            var result = await memberStatus.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal( "These are updated Notes",result.Notes);
        }

        [Fact]
        public async Task TestMemberStatusER_Insert()
        {
            var memberStatus = await MemberStatusER.NewMemberStatus();
            memberStatus.Description = "Standby";
            memberStatus.Notes = "This person is on standby";

            var savedMemberStatus = await memberStatus.SaveAsync();
           
            Assert.NotNull(savedMemberStatus);
            Assert.IsType<MemberStatusER>(savedMemberStatus);
            Assert.True( savedMemberStatus.Id > 0 );
        }

        [Fact]
        public async Task TestMemberStatusER_Delete()
        {
            int beforeCount = MockDb.MemberStatuses.Count();
            
            await MemberStatusER.DeleteMemberStatus(99);
            
            Assert.NotEqual(beforeCount,MockDb.MemberStatuses.Count());
        }
        
        // test invalid state 
        [Fact]
        public async Task TestMemberStatusER_DescriptionRequired()
        {
            var memberStatus = await MemberStatusER.NewMemberStatus();
            memberStatus.Description = "make valid";
            var isObjectValidInit = memberStatus.IsValid;
            memberStatus.Description = string.Empty;

            Assert.NotNull(memberStatus);
            Assert.True(isObjectValidInit);
            Assert.False(memberStatus.IsValid);
 
        }
       
        [Fact]
        public async Task TestMemberStatusER_DescriptionExceedsMaxLengthOf50()
        {
            var memberStatus = await MemberStatusER.NewMemberStatus();
            memberStatus.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(memberStatus);
            Assert.False(memberStatus.IsValid);
            Assert.Equal("The field Description must be a string or array type with a maximum length of '50'.",
                memberStatus.BrokenRulesCollection[0].Description);
 
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestMemberStatusER_TestInvalidSave()
        {
            var memberStatus = await MemberStatusER.NewMemberStatus();
            memberStatus.Description = String.Empty;
            bool exceptionThrown = false;
            try
            {
                var savedMemberStatus = await memberStatus.SaveAsync();
            }
            catch (ValidationException )
            {
                exceptionThrown = true;
            }
            
            Assert.False(memberStatus.IsValid);
            Assert.True(exceptionThrown);
        }
    }
}

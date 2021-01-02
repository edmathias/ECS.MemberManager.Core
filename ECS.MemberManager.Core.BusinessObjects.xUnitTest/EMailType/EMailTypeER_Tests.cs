using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    /// <summary>
    /// Summary description for JustMockTest
    /// </summary>
    public class EMailTypeER_Tests 
    {
        public EMailTypeER_Tests()
        {
            MockDb.ResetMockDb();     
        }
        
        [Fact]
        public async Task TestEMailTypeER_Get()
        {
            var eMailType = await EMailTypeER.GetEMailType(1);

            Assert.Equal(1, eMailType.Id);
            Assert.True(eMailType.IsValid);
        }

        [Fact]
        public async Task TestEMailTypeER_New()
        {
            var eMailType = await EMailTypeER.NewEMailType();

            Assert.NotNull(eMailType);
            Assert.False(eMailType.IsValid);
        }

        [Fact]
        public async void TestEMailTypeER_Update()
        {
            var eMailType = await EMailTypeER.GetEMailType(1);
            var notesUpdate =$"These are updated Notes {DateTime.Now}";
            eMailType.Notes = notesUpdate;
            
            var result =  await eMailType.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal( notesUpdate,result.Notes);
        }

        [Fact]
        public async void TestEMailTypeER_Insert()
        {
            var eMailType = await EMailTypeER.NewEMailType();
            eMailType.Description = "Standby";
            eMailType.Notes = "This person is on standby";

            var savedEMailType = await eMailType.SaveAsync();
           
            Assert.NotNull(savedEMailType);
            Assert.IsType<EMailTypeER>(savedEMailType);
            Assert.True( savedEMailType.Id > 0 );
        }

        [Fact]
        public async Task TestEMailTypeER_Delete()
        {
            var emailTypeList = await EMailTypeERL.GetEMailTypeList();
            int beforeCount = emailTypeList.Count;
            
            await EMailTypeER.DeleteEMailType(99);
           
            var emailTypeListAfter = await EMailTypeERL.GetEMailTypeList();
             
            Assert.NotEqual(beforeCount, emailTypeListAfter.Count);
        }
        
        // test invalid state 
        [Fact]
        public async Task TestEMailTypeER_DescriptionRequired()
        {
            var eMailType = await EMailTypeER.NewEMailType();
            eMailType.Description = "make valid";
            var isObjectValidInit = eMailType.IsValid;
            eMailType.Description = string.Empty;

            Assert.NotNull(eMailType);
            Assert.True(isObjectValidInit);
            Assert.False(eMailType.IsValid);
 
        }
       
        [Fact]
        public async Task TestEMailTypeER_DescriptionExceedsMaxLengthOf50()
        {
            var eMailType = await EMailTypeER.NewEMailType();
            eMailType.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(eMailType);
            Assert.False(eMailType.IsValid);
            Assert.Equal("The field Description must be a string or array type with a maximum length of '50'.", 
                eMailType.BrokenRulesCollection[0].Description);
 
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestEMailTypeER_TestInvalidSave()
        {
            var eMailType = await EMailTypeER.NewEMailType();
            eMailType.Description = String.Empty;
            EMailTypeER savedEMailType = null;
            
            Assert.False(eMailType.IsValid);
            Assert.Throws<ValidationException>(() => savedEMailType =  eMailType.Save() );
        }
    }
}

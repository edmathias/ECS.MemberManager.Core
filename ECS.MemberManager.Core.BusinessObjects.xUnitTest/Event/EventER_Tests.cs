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
    public class EventER_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EventER_Tests()
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
        public async Task EventER_TestGetEvent()
        {
            var eventObj = await EventER.GetEventER(1);

            Assert.NotNull(eventObj);
            Assert.IsType<EventER>(eventObj);
        }

        [Fact]
        public async Task EventER_TestGetNewEventER()
        {
            var eventObj = await EventER.NewEventER();

            Assert.NotNull(eventObj);
            Assert.False(eventObj.IsValid);
        }

        [Fact]
        public async Task EventER_TestUpdateExistingEventER()
        {
            var eventObj = await EventER.GetEventER(1);
            eventObj.Notes = "These are updated Notes";
            
            var result =  await eventObj.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes",result.Notes );
        }

        [Fact]
        public async Task EventER_TestInsertNewEventER()
        {
            var eventObj = await EventER.NewEventER();
            eventObj.EventName = "event name";
            eventObj.Notes = "This person is on standby";
            eventObj.LastUpdatedBy = "edm";
            eventObj.LastUpdatedDate = DateTime.Now;
            eventObj.NextDate = DateTime.Now;

            var savedEvent = await eventObj.SaveAsync();
           
            Assert.NotNull(savedEvent);
            Assert.IsType<EventER>(savedEvent);
            Assert.True( savedEvent.Id > 0 );
        }

        [Fact]
        public async Task EventER_TestDeleteObjectFromDatabase()
        {
            const int ID_TO_DELETE = 99;
            
            await EventER.DeleteEventER(ID_TO_DELETE);

            await Assert.ThrowsAsync<DataPortalException>(() => EventER.GetEventER(ID_TO_DELETE));
        }
        
        // test invalid state 
        [Fact]
        public async Task EventER_TestDescriptionRequired() 
        {
            var eventObj = await EventER.NewEventER();
            eventObj.EventName = "event name";
            eventObj.Description = "make valid";
            eventObj.LastUpdatedBy = "edm";
            eventObj.LastUpdatedDate = DateTime.Now;
            var isObjectValidInit = eventObj.IsValid;
            eventObj.EventName = string.Empty;

            Assert.NotNull(eventObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventObj.IsValid);
        }
       
        [Fact]
        public async Task EventER_TestEventNameExceedsMaxLengthOf255()
        {
            var eventObj = await EventER.NewEventER();
            eventObj.LastUpdatedBy = "edm";
            eventObj.LastUpdatedDate = DateTime.Now;
            eventObj.EventName = "valid length";
            Assert.True(eventObj.IsValid);
            
            eventObj.EventName = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                     "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                     "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                     "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(eventObj);
            Assert.False(eventObj.IsValid);
            Assert.Equal("EventName",eventObj.BrokenRulesCollection[0].Property);
            Assert.Equal("EventName can not exceed 255 characters",eventObj.BrokenRulesCollection[0].Description);
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task EventER_TestInvalidSaveEventER()
        {
            var eventObj = await EventER.NewEventER();
            eventObj.Description = String.Empty;
            EventER savedEvent = null;
                
            Assert.False(eventObj.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedEvent =  eventObj.Save() );
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventEdit_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EventEdit_Tests()
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
        public async Task TestEventEdit_TestGetEvent()
        {
            var getEvent = await EventEdit.GetEventEdit(1);

            Assert.NotNull(getEvent);
            Assert.True(getEvent.IsValid);
            Assert.IsType<EventEdit>(getEvent);

        }

        [Fact]
        public async Task EventEdit_TestNewEvent()
        {
            var newEvent = await EventEdit.NewEventEdit();

            Assert.NotNull(newEvent);
            Assert.False(newEvent.IsValid);
        }

        [Fact]
        public async Task EventEdit_TestUpdateEvent()
        {
            var eventEdit = await EventEdit.GetEventEdit(1);
            var notesUpdate = $"These are updated Notes {DateTime.Now}";
            eventEdit.Notes = notesUpdate;
            
            var result = await eventEdit.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal( notesUpdate, result.Notes);
        }

        [Fact]
        public async Task EventEdit_TestInsertEvent()
        {
            var newEvent = await EventEdit.NewEventEdit();
            CreateValidEvent(newEvent);

            var savedEvent = await newEvent.SaveAsync();
           
            Assert.NotNull(savedEvent);
            Assert.IsType<EventEdit>(savedEvent);
            Assert.True( savedEvent.Id > 0 );
        }

 
        [Fact]
        public async Task EventEdit_TestDeleteEvent()
        {
            const int ID_TO_DELETE = 99;
            
            var beforeList = await EventEditList.GetEventEditList();
            var beforeCount = beforeList.Count;
            
            await EventEdit.DeleteEventEdit(ID_TO_DELETE);

            var afterList = await EventEditList.GetEventEditList();
            
            Assert.NotEqual(beforeCount, afterList.Count);
        }
        
        // test invalid state 
        [Fact]
        public async Task EventEdit_TestEventNameRequired()
        {
            var validEvent = await EventEdit.NewEventEdit();
            CreateValidEvent(validEvent);
            var isObjectValidInit = validEvent.IsValid;
            validEvent.EventName = string.Empty;
            
            Assert.True(isObjectValidInit);
            Assert.False(validEvent.IsValid);
        }
    
         
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task EventEdit_TestInvalidSaveEvent()
        {
            var eventToSave = await EventEdit.NewEventEdit();
            
            await Assert.ThrowsAsync<ValidationException>(() => eventToSave.SaveAsync() );
        } 
        
        private static void CreateValidEvent(EventEdit newEvent)
        {
            newEvent.EventName = "This awesome event";
            newEvent.Description = "description of awesome event";
            newEvent.IsOneTime = true;
            newEvent.NextDate = DateTime.Now.AddDays(7);
            newEvent.LastUpdatedBy = "edm";
            newEvent.LastUpdatedDate = DateTime.Now;
            newEvent.Notes = "Notes for this event";
        }
        
    } 
}

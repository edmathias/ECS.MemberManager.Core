using System;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventER_Tests 
    {
        [Fact]
        public async Task TestEventER_Get()
        {
            var getEvent = await EventER.GetEvent(1);
            

            Assert.Equal(1, getEvent.Id);
            Assert.True(getEvent.IsValid);
        }

        [Fact]
        public async Task TestEventER_New()
        {
            var newEvent = await EventER.NewEvent();

            Assert.NotNull(newEvent);
            Assert.False(newEvent.IsValid);
        }

        [Fact]
        public async Task TestEventER_Update()
        {
            var eventToUpdate = await EventER.GetEvent(1);
            eventToUpdate.Notes = "These are updated Notes";
            
            var result = await eventToUpdate.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal( "These are updated Notes",result.Notes);
        }

        [Fact]
        public async Task TestEventER_Insert()
        {
            var newEvent = await EventER.NewEvent();
            CreateValidEvent(newEvent);

            var savedEvent = await newEvent.SaveAsync();
           
            Assert.NotNull(savedEvent);
            Assert.IsType<EventER>(savedEvent);
            Assert.True( savedEvent.Id > 0 );
        }

 
        [Fact]
        public async Task TestEventER_Delete()
        {
            int beforeCount = MockDb.Events.Count();
            
            await EventER.DeleteEvent(99);
            
            Assert.NotEqual(MockDb.Events.Count(),beforeCount);
        }
        
        // test invalid state 
        [Fact]
        public async Task TestEventER_EventNameRequired()
        {
            var validEvent = await EventER.NewEvent();
            CreateValidEvent(validEvent);
            var isObjectValidInit = validEvent.IsValid;
            validEvent.EventName = string.Empty;
            
            Assert.True(isObjectValidInit);
            Assert.False(validEvent.IsValid);
        }
    
         
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestEventER_TestInvalidSave()
        {
            var eventToSave = await EventER.NewEvent();
            
            Assert.Throws<ValidationException>(() => eventToSave.Save() );
        } 
        
        private static void CreateValidEvent(EventER newEvent)
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

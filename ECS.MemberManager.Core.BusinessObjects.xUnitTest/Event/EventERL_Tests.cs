using System;
using System.Linq;
using Csla;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventERL_Tests
    {
        public EventERL_Tests()
        {
            MockDb.ResetMockDb();
        }
        
        [Fact]
        private async void EventERL_TestGetEventList()
        {
            var listToTest = MockDb.Events;
            
            var eventErl = await EventERL.GetEventList(listToTest);

            Assert.NotNull(eventErl);
            Assert.Equal(MockDb.Events.Count, eventErl.Count);
        }
        
        [Fact]
        private async void EventERL_TestDeleteEventsEntry()
        {
            var listToTest = MockDb.Events;
            var listCount = listToTest.Count;
            
            var idToDelete = MockDb.Events.Max(a => a.Id);
            var eventErl = await EventERL.GetEventList(listToTest);

            var eventToDelete = eventErl.First(a => a.Id == idToDelete);

            // remove is deferred delete
            eventErl.Remove(eventToDelete); 

            var eventListAfterDelete = await eventErl.SaveAsync();
            
            Assert.NotEqual(listCount,eventListAfterDelete.Count);
        }

        [Fact]
        private async void EventERL_TestUpdateEventsEntry()
        {
            var eventList = await EventERL.GetEventList(MockDb.Events);
            var countBeforeUpdate = eventList.Count;
            var idToUpdate = MockDb.Events.Min(a => a.Id);
            var eventToUpdate = eventList.First(a => a.Id == idToUpdate);

            eventToUpdate.Description = "This was updated";
            await eventList.SaveAsync();
            
            var updatedEventsList = await EventERL.GetEventList(MockDb.Events);
            
            Assert.Equal("This was updated",updatedEventsList.First(a => a.Id == idToUpdate).Description);
            Assert.Equal(countBeforeUpdate, updatedEventsList.Count);
        }

        [Fact]
        private async void EventERL_TestAddEventsEntry()
        {
            var eventList = await EventERL.GetEventList(MockDb.Events);
            var countBeforeAdd = eventList.Count;
            
            var eventToAdd = eventList.AddNew();
            BuildEvent(eventToAdd); 

            await eventList.SaveAsync();
            
            var updatedEventsList = await EventERL.GetEventList(MockDb.Events);
            
            Assert.NotEqual(countBeforeAdd, updatedEventsList.Count);
        }

        private void BuildEvent(EventEC eventToBuild)
        {
            eventToBuild.EventName = "new event";
            eventToBuild.NextDate = new SmartDate().Add(TimeSpan.FromDays(365));
            eventToBuild.IsOneTime = false;
            eventToBuild.Description = "doc type description";
            eventToBuild.Notes = "document type notes";
            eventToBuild.LastUpdatedBy = "edm";
            eventToBuild.LastUpdatedDate = DateTime.Now;
        }
    }
}
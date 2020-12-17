using System;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventERL_Tests
    {
        [Fact]
        private async void TestEventERL_GetEventList()
        {
            var listToTest = MockDb.Events;
            
            var eventErl = await EventERL.GetEventList(listToTest);

            Assert.NotNull(eventErl);
            Assert.Equal(MockDb.Events.Count, eventErl.Count);
        }
        
        [Fact]
        private async void TestEventERL_DeleteEventEntry()
        {
            var listToTest = MockDb.Events;
            var idToDelete = MockDb.Events.Max(a => a.Id);
            
            var eventErl = await EventERL.GetEventList(listToTest);

            var eventToDelete = eventErl.First(a => a.Id == idToDelete);

            // remove is deferred delete
            eventErl.Remove(eventToDelete); 

            var eventListAfterDelete = await eventErl.SaveAsync();
            
            Assert.NotNull(eventListAfterDelete);
            Assert.Null(eventListAfterDelete.FirstOrDefault(pl => pl.Id == idToDelete));
        }
        
    }
}
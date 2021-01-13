using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventInfo_Tests
    {

        [Fact]
        public async void EventInfo_TestGetById()
        {
            var eventInfo = await EventInfo.GetEventInfo(1);
            
            Assert.NotNull(eventInfo);
            Assert.IsType<EventInfo>(eventInfo);
            Assert.Equal(1, eventInfo.Id);
        }

        [Fact]
        public async void EventInfo_TestGetChild()
        {
            const int ID_VALUE = 999;
            
            var eventType = new Event()
            {
                Id = ID_VALUE,
                Description = "Test event type",
                Notes = "event type notes"
            };

            var eventTypeInfo = await EventInfo.GetEventInfo(eventType);
            
            Assert.NotNull(eventTypeInfo);
            Assert.IsType<EventInfo>(eventTypeInfo);
            Assert.Equal(ID_VALUE, eventTypeInfo.Id);

        }
    }
}
﻿using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventROR_Tests : CslaBaseTest
    {
        [Fact]
        public async void EventROR_TestGetById()
        {
            var eventObj = await EventROR.GetEventROR(1);

            Assert.NotNull(eventObj);
            Assert.IsType<EventROR>(eventObj);
            Assert.Equal(1, eventObj.Id);
        }
    }
}
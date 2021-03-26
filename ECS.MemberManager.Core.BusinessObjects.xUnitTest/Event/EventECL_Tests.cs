using System;
using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventECL_Tests : CslaBaseTest
    {
        [Fact]
        private async void EventECL_TestEventECL()
        {
            var eventObjEdit = await EventECL.NewEventECL();

            Assert.NotNull(eventObjEdit);
            Assert.IsType<EventECL>(eventObjEdit);
        }


        [Fact]
        private async void EventECL_TestGetEventECL()
        {
            var childData = MockDb.Events;

            var listToTest = await EventECL.GetEventECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }

        private void BuildEvent(EventEC eventObj)
        {
            eventObj.EventName = "event name";
            eventObj.Description = "event description";
            eventObj.Notes = "event notes";
            eventObj.LastUpdatedBy = "edm";
            eventObj.LastUpdatedDate = DateTime.Now;
            eventObj.NextDate = DateTime.Now;
        }
    }
}
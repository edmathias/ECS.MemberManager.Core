using System;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventROC_Tests
    {
        [Fact]
        public async void EventROC_TestGetChild()
        {
            const int ID_VALUE = 999;

            var docType = BuildEvent();
            docType.Id = ID_VALUE;

            var eventObj = await EventROC.GetEventROC(docType);
            
            Assert.NotNull(eventObj);
            Assert.IsType<EventROC>(eventObj);
            Assert.Equal(eventObj.Id, eventObj.Id);
            Assert.Equal(eventObj.Description, eventObj.Description);
            Assert.Equal(eventObj.Notes, eventObj.Notes);
            Assert.Equal(eventObj.LastUpdatedBy, eventObj.LastUpdatedBy);
            Assert.Equal(eventObj.LastUpdatedDate, eventObj.LastUpdatedDate);
            Assert.Equal(eventObj.RowVersion, eventObj.RowVersion);
        }

        private Event BuildEvent()
        {
            var eventObj = new Event();
            eventObj.Id = 1;
            eventObj.Description = "test description";
            eventObj.LastUpdatedBy = "edm";
            eventObj.LastUpdatedDate = DateTime.Now;
            eventObj.Notes = "notes for doctype";

            return eventObj;
        }        
        

    }
}
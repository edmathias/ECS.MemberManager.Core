using System;
using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventMemberROC_Tests : CslaBaseTest
    {
        [Fact]
        public async Task TestEventMemberROC_GetEventMemberROC()
        {
            var eventMemberObjToLoad = BuildEventMember();
            var eventMemberObj = await EventMemberROC.GetEventMemberROC(eventMemberObjToLoad);

            Assert.NotNull(eventMemberObj);
            Assert.IsType<EventMemberROC>(eventMemberObj);
            Assert.Equal(1, eventMemberObj.Id);
        }

        private EventMember BuildEventMember()
        {
            var eventDocumentObj = new EventMember();
            eventDocumentObj.Id = 1;
            eventDocumentObj.Event = new Event() {Id = 1};
            eventDocumentObj.MemberInfo = new MemberInfo() {Id = 1};
            eventDocumentObj.Role = "Organizer";
            eventDocumentObj.LastUpdatedBy = "edm";
            eventDocumentObj.LastUpdatedDate = DateTime.Now;
            eventDocumentObj.Notes = "notes for doctype";

            return eventDocumentObj;
        }
    }
}
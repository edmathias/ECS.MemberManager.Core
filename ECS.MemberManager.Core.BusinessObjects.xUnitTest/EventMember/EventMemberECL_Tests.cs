using System;
using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventMemberECL_Tests : CslaBaseTest
    {
        [Fact]
        private async void EventMemberECL_TestEventMemberECL()
        {
            var eventMemberObjEdit = await EventMemberECL.NewEventMemberECL();

            Assert.NotNull(eventMemberObjEdit);
            Assert.IsType<EventMemberECL>(eventMemberObjEdit);
        }


        [Fact]
        private async void EventMemberECL_TestGetEventMemberECL()
        {
            var childData = MockDb.EventMembers;
            
            var listToTest = await EventMemberECL.GetEventMemberECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
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
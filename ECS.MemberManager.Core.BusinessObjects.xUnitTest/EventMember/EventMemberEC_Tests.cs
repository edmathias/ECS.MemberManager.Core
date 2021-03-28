using System;
using System.IO;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventMemberEC_Tests : CslaBaseTest
    {
        [Fact]
        public async Task TestEventMemberEC_NewEventMemberEC()
        {
            var eventDocumentObj = await EventMemberEC.NewEventMemberEC();

            Assert.NotNull(eventDocumentObj);
            Assert.IsType<EventMemberEC>(eventDocumentObj);
            Assert.False(eventDocumentObj.IsValid);
        }

        [Fact]
        public async Task TestEventMemberEC_GetEventMemberEC()
        {
            var eventDocumentObjToLoad = BuildEventMember();
            var eventDocumentObj = await EventMemberEC.GetEventMemberEC(eventDocumentObjToLoad);

            Assert.NotNull(eventDocumentObj);
            Assert.IsType<EventMemberEC>(eventDocumentObj);
            Assert.Equal(eventDocumentObjToLoad.Id, eventDocumentObj.Id);
            Assert.Equal(eventDocumentObjToLoad.Event.Id, eventDocumentObj.Event.Id);
            Assert.Equal(eventDocumentObjToLoad.MemberInfo.Id, eventDocumentObj.MemberInfo.Id);
            Assert.Equal(eventDocumentObjToLoad.Role, eventDocumentObj.Role);
            Assert.Equal(eventDocumentObjToLoad.LastUpdatedBy, eventDocumentObj.LastUpdatedBy);
            Assert.Equal(new SmartDate(eventDocumentObjToLoad.LastUpdatedDate), eventDocumentObj.LastUpdatedDate);
            Assert.Equal(eventDocumentObjToLoad.Notes, eventDocumentObj.Notes);
            Assert.Equal(eventDocumentObjToLoad.RowVersion, eventDocumentObj.RowVersion);
            Assert.True(eventDocumentObj.IsValid);
        }

        [Fact]
        public async Task TestEventMemberEC_EventRequired()
        {
            var eventDocumentObjToTest = BuildEventMember();
            var eventDocumentObj = await EventMemberEC.GetEventMemberEC(eventDocumentObjToTest);
            var isObjectValidInit = eventDocumentObj.IsValid;
            eventDocumentObj.Event = null;

            Assert.NotNull(eventDocumentObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventDocumentObj.IsValid);
            Assert.Equal("Event", eventDocumentObj.BrokenRulesCollection[0].Property);
            Assert.Equal("Event required", eventDocumentObj.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestEventMemberEC_MemberInfoRequired()
        {
            var eventDocumentObjToTest = BuildEventMember();
            var eventDocumentObj = await EventMemberEC.GetEventMemberEC(eventDocumentObjToTest);
            var isObjectValidInit = eventDocumentObj.IsValid;
            eventDocumentObj.MemberInfo = null;

            Assert.NotNull(eventDocumentObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventDocumentObj.IsValid);
            Assert.Equal("MemberInfo", eventDocumentObj.BrokenRulesCollection[0].Property);
            Assert.Equal("MemberInfo required", eventDocumentObj.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestEventMemberEC_RoleCannotExceed50Chars()
        {
            var eventDocumentObjToTest = BuildEventMember();
            var eventDocumentObj = await EventMemberEC.GetEventMemberEC(eventDocumentObjToTest);
            var isObjectValidInit = eventDocumentObj.IsValid;
            eventDocumentObj.Role = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                    "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                    "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                    "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(eventDocumentObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventDocumentObj.IsValid);
            Assert.Equal("Role", eventDocumentObj.BrokenRulesCollection[0].Property);
            Assert.Equal("Role can not exceed 50 characters", eventDocumentObj.BrokenRulesCollection[0].Description);
        }


        [Fact]
        public async Task TestEventMemberEC_LastUpdatedByRequired()
        {
            var eventDocumentObjToTest = BuildEventMember();
            var eventDocumentObj = await EventMemberEC.GetEventMemberEC(eventDocumentObjToTest);
            var isObjectValidInit = eventDocumentObj.IsValid;
            eventDocumentObj.LastUpdatedBy = string.Empty;

            Assert.NotNull(eventDocumentObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventDocumentObj.IsValid);
            Assert.Equal("LastUpdatedBy", eventDocumentObj.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy required", eventDocumentObj.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestEventMemberEC_LastUpdatedDateRequired()
        {
            var eventDocumentObjToTest = BuildEventMember();
            var eventDocumentObj = await EventMemberEC.GetEventMemberEC(eventDocumentObjToTest);
            var isObjectValidInit = eventDocumentObj.IsValid;
            eventDocumentObj.LastUpdatedDate = DateTime.MinValue;

            Assert.NotNull(eventDocumentObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventDocumentObj.IsValid);
            Assert.Equal("LastUpdatedDate", eventDocumentObj.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedDate required", eventDocumentObj.BrokenRulesCollection[0].Description);
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
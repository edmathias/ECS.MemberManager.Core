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
    public class EventEC_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EventEC_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");

            if (testLibrary == "Mock")
                MockDb.ResetMockDb();
            else
            {
                if (!IsDatabaseBuilt)
                {
                    var adoDb = new ADODb();
                    adoDb.BuildMemberManagerADODb();
                    IsDatabaseBuilt = true;
                }
            }
        }

        [Fact]
        public async Task EventEC_NewEventEC()
        {
            var eventObj = await EventEC.NewEventEC();

            Assert.NotNull(eventObj);
            Assert.IsType<EventEC>(eventObj);
            Assert.False(eventObj.IsValid);
        }
        
        [Fact]
        public async Task EventEC_GetEventEC()
        {
            var eventObjToLoad = BuildEvent();
            var eventObj = await EventEC.GetEventEC(eventObjToLoad);

            Assert.NotNull(eventObj);
            Assert.IsType<EventEC>(eventObj);
            Assert.Equal(eventObjToLoad.Id,eventObj.Id);
            Assert.Equal(eventObjToLoad.Description,eventObj.Description);
            Assert.Equal(eventObjToLoad.LastUpdatedBy, eventObj.LastUpdatedBy);
            Assert.Equal(new SmartDate(eventObjToLoad.LastUpdatedDate), eventObj.LastUpdatedDate);
            Assert.Equal(eventObjToLoad.Notes, eventObj.Notes);
            Assert.Equal(eventObjToLoad.RowVersion, eventObj.RowVersion);
            Assert.True(eventObj.IsValid);
        }

        [Fact]
        public async Task EventEC_EventNameRequired()
        {
            var eventObjToTest = BuildEvent();
            var eventObj = await EventEC.GetEventEC(eventObjToTest);
            var isObjectValidInit = eventObj.IsValid;
            eventObj.EventName = string.Empty; 

            Assert.NotNull(eventObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventObj.IsValid);
            Assert.Equal("EventName",eventObj.BrokenRulesCollection[0].Property);
            Assert.Equal("EventName required",eventObj.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task EventEC_EventNameExceedsMaxLengthOf255Chars()
        {
            var eventObjToTest = BuildEvent();
            var eventObj = await EventEC.GetEventEC(eventObjToTest);
            var isObjectValidInit = eventObj.IsValid;
            eventObj.EventName = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(eventObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventObj.IsValid);
            Assert.Equal("EventName",eventObj.BrokenRulesCollection[0].Property);
            Assert.Equal("EventName can not exceed 255 characters",eventObj.BrokenRulesCollection[0].Description);
        }
        

        [Fact]
        public async Task EventEC_LastUpdatedByRequired()
        {
            var eventObjToTest = BuildEvent();
            var eventObj = await EventEC.GetEventEC(eventObjToTest);
            var isObjectValidInit = eventObj.IsValid;
            eventObj.LastUpdatedBy = string.Empty;

            Assert.NotNull(eventObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventObj.IsValid);
            Assert.Equal("LastUpdatedBy",eventObj.BrokenRulesCollection[0].Property);
        }
     
        [Fact]
        public async Task EventEC_LastUpdatedDateRequired()
        {
            var eventObjToTest = BuildEvent();
            var eventObj = await EventEC.GetEventEC(eventObjToTest);
            var isObjectValidInit = eventObj.IsValid;
            eventObj.LastUpdatedDate = DateTime.MinValue; 

            Assert.NotNull(eventObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventObj.IsValid);
            Assert.Equal("LastUpdatedDate",eventObj.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedDate required",eventObj.BrokenRulesCollection[0].Description);
        }        
        private Event BuildEvent()
        {
            var eventObj = new Event();
            eventObj.Id = 1;
            eventObj.EventName = "test event name";
            eventObj.Description = "test description";
            eventObj.LastUpdatedBy = "edm";
            eventObj.LastUpdatedDate = DateTime.Now;
            eventObj.Notes = "notes for doctype";

            return eventObj;
        }        
    }
}
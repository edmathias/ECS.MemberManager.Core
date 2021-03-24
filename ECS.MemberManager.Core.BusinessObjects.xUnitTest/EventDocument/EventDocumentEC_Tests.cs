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
    public class EventDocumentEC_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EventDocumentEC_Tests()
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
        public async Task TestEventDocumentEC_NewEventDocumentEC()
        {
            var eventDocumentObj = await EventDocumentEC.NewEventDocumentEC();

            Assert.NotNull(eventDocumentObj);
            Assert.IsType<EventDocumentEC>(eventDocumentObj);
            Assert.False(eventDocumentObj.IsValid);
        }

        [Fact]
        public async Task TestEventDocumentEC_GetEventDocumentEC()
        {
            var eventDocumentObjToLoad = BuildEventDocument();
            var eventDocumentObj = await EventDocumentEC.GetEventDocumentEC(eventDocumentObjToLoad);

            Assert.NotNull(eventDocumentObj);
            Assert.IsType<EventDocumentEC>(eventDocumentObj);
            Assert.Equal(eventDocumentObjToLoad.Id, eventDocumentObj.Id);
            Assert.Equal(eventDocumentObjToLoad.Event.Id, eventDocumentObj.Event.Id);
            Assert.Equal(eventDocumentObjToLoad.DocumentType.Id, eventDocumentObj.DocumentType.Id);
            Assert.Equal(eventDocumentObjToLoad.DocumentName, eventDocumentObj.DocumentName);
            Assert.Equal(eventDocumentObjToLoad.PathAndFileName, eventDocumentObj.PathAndFileName);
            Assert.Equal(eventDocumentObjToLoad.LastUpdatedBy, eventDocumentObj.LastUpdatedBy);
            Assert.Equal(new SmartDate(eventDocumentObjToLoad.LastUpdatedDate), eventDocumentObj.LastUpdatedDate);
            Assert.Equal(eventDocumentObjToLoad.Notes, eventDocumentObj.Notes);
            Assert.Equal(eventDocumentObjToLoad.RowVersion, eventDocumentObj.RowVersion);
            Assert.True(eventDocumentObj.IsValid);
        }

        [Fact]
        public async Task TestEventDocumentEC_EventRequired()
        {
            var eventDocumentObjToTest = BuildEventDocument();
            var eventDocumentObj = await EventDocumentEC.GetEventDocumentEC(eventDocumentObjToTest);
            var isObjectValidInit = eventDocumentObj.IsValid;
            eventDocumentObj.Event = null;

            Assert.NotNull(eventDocumentObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventDocumentObj.IsValid);
            Assert.Equal("Event", eventDocumentObj.BrokenRulesCollection[0].Property);
            Assert.Equal("Event required", eventDocumentObj.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestEventDocumentEC_DocumentNameRequired()
        {
            var eventDocumentObjToTest = BuildEventDocument();
            var eventDocumentObj = await EventDocumentEC.GetEventDocumentEC(eventDocumentObjToTest);
            var isObjectValidInit = eventDocumentObj.IsValid;
            eventDocumentObj.DocumentName = string.Empty;

            Assert.NotNull(eventDocumentObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventDocumentObj.IsValid);
            Assert.Equal("DocumentName", eventDocumentObj.BrokenRulesCollection[0].Property);
            Assert.Equal("DocumentName required", eventDocumentObj.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestEventDocumentEC_DocumentNameMaxLengthNotExceed50Chars()
        {
            var eventDocumentObjToTest = BuildEventDocument();
            var eventDocumentObj = await EventDocumentEC.GetEventDocumentEC(eventDocumentObjToTest);
            var isObjectValidInit = eventDocumentObj.IsValid;
            eventDocumentObj.DocumentName =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(eventDocumentObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventDocumentObj.IsValid);
            Assert.Equal("DocumentName", eventDocumentObj.BrokenRulesCollection[0].Property);
            Assert.Equal("DocumentName can not exceed 50 characters",
                eventDocumentObj.BrokenRulesCollection[0].Description);
        }


        [Fact]
        public async Task TestEventDocumentEC_LastUpdatedByRequired()
        {
            var eventDocumentObjToTest = BuildEventDocument();
            var eventDocumentObj = await EventDocumentEC.GetEventDocumentEC(eventDocumentObjToTest);
            var isObjectValidInit = eventDocumentObj.IsValid;
            eventDocumentObj.LastUpdatedBy = string.Empty;

            Assert.NotNull(eventDocumentObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventDocumentObj.IsValid);
            Assert.Equal("LastUpdatedBy", eventDocumentObj.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy required", eventDocumentObj.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestEventDocumentEC_LastUpdatedDateRequired()
        {
            var eventDocumentObjToTest = BuildEventDocument();
            var eventDocumentObj = await EventDocumentEC.GetEventDocumentEC(eventDocumentObjToTest);
            var isObjectValidInit = eventDocumentObj.IsValid;
            eventDocumentObj.LastUpdatedDate = DateTime.MinValue;

            Assert.NotNull(eventDocumentObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventDocumentObj.IsValid);
            Assert.Equal("LastUpdatedDate", eventDocumentObj.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedDate required", eventDocumentObj.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestEventDocumentEC_PathAndFileNameRequired()
        {
            var eventDocumentObjToTest = BuildEventDocument();
            var eventDocumentObj = await EventDocumentEC.GetEventDocumentEC(eventDocumentObjToTest);
            var isObjectValidInit = eventDocumentObj.IsValid;
            eventDocumentObj.PathAndFileName = string.Empty;

            Assert.NotNull(eventDocumentObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventDocumentObj.IsValid);
            Assert.Equal("PathAndFileName", eventDocumentObj.BrokenRulesCollection[0].Property);
            Assert.Equal("PathAndFileName required", eventDocumentObj.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestEventDocumentEC_PathAndFileNameNotExceed255Chars()
        {
            var eventDocumentObjToTest = BuildEventDocument();
            var eventDocumentObj = await EventDocumentEC.GetEventDocumentEC(eventDocumentObjToTest);
            var isObjectValidInit = eventDocumentObj.IsValid;
            eventDocumentObj.PathAndFileName =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(eventDocumentObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventDocumentObj.IsValid);
            Assert.Equal("PathAndFileName", eventDocumentObj.BrokenRulesCollection[0].Property);
            Assert.Equal("PathAndFileName can not exceed 255 characters",
                eventDocumentObj.BrokenRulesCollection[0].Description);
        }

        private EventDocument BuildEventDocument()
        {
            var eventDocumentObj = new EventDocument();
            eventDocumentObj.Id = 1;
            eventDocumentObj.Event = new Event() {Id = 1};
            eventDocumentObj.DocumentType = new DocumentType() {Id = 1};
            eventDocumentObj.DocumentName = "test doc name";
            eventDocumentObj.PathAndFileName = "c:\\pathandfilename";
            eventDocumentObj.LastUpdatedBy = "edm";
            eventDocumentObj.LastUpdatedDate = DateTime.Now;
            eventDocumentObj.Notes = "notes for doctype";

            return eventDocumentObj;
        }
    }
}
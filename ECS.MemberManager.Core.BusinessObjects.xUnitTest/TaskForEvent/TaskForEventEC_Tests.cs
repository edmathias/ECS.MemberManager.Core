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
    public class TaskForEventEC_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public TaskForEventEC_Tests()
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
        public async Task TestTaskForEventEC_NewTaskForEventEC()
        {
            var eventObj = await TaskForEventEC.NewTaskForEventEC();

            Assert.NotNull(eventObj);
            Assert.IsType<TaskForEventEC>(eventObj);
            Assert.False(eventObj.IsValid);
        }

        [Fact]
        public async Task TestTaskForEventEC_GetTaskForEventEC()
        {
            var eventObjToLoad = BuildTaskForEvent();
            var eventObj = await TaskForEventEC.GetTaskForEventEC(eventObjToLoad);

            Assert.NotNull(eventObj);
            Assert.IsType<TaskForEventEC>(eventObj);
            Assert.Equal(eventObjToLoad.Id, eventObj.Id);
            Assert.Equal(eventObjToLoad.TaskName, eventObj.TaskName);
            Assert.Equal(new SmartDate(eventObjToLoad.PlannedDate), eventObj.PlannedDate);
            Assert.Equal(new SmartDate(eventObjToLoad.ActualDate), eventObj.ActualDate);
            Assert.Equal(eventObjToLoad.LastUpdatedBy, eventObj.LastUpdatedBy);
            Assert.Equal(new SmartDate(eventObjToLoad.LastUpdatedDate), eventObj.LastUpdatedDate);
            Assert.Equal(eventObjToLoad.Notes, eventObj.Notes);
            Assert.Equal(eventObjToLoad.RowVersion, eventObj.RowVersion);
            Assert.True(eventObj.IsValid);
        }

        [Fact]
        public async Task TestTaskForEventEC_TaskForEventNameLessThan50Chars()
        {
            var eventObjToTest = BuildTaskForEvent();
            var eventObj = await TaskForEventEC.GetTaskForEventEC(eventObjToTest);
            var isObjectValidInit = eventObj.IsValid;
            eventObj.TaskName = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(eventObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventObj.IsValid);
            Assert.Equal("TaskName", eventObj.BrokenRulesCollection[0].Property);
        }

        [Fact]
        public async Task TestTaskForEventEC_LastUpdatedByRequired()
        {
            var eventObjToTest = BuildTaskForEvent();
            var eventObj = await TaskForEventEC.GetTaskForEventEC(eventObjToTest);
            var isObjectValidInit = eventObj.IsValid;
            eventObj.LastUpdatedBy = string.Empty;

            Assert.NotNull(eventObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventObj.IsValid);
            Assert.Equal("LastUpdatedBy", eventObj.BrokenRulesCollection[0].Property);
        }

        private TaskForEvent BuildTaskForEvent()
        {
            var eventObj = new TaskForEvent();
            eventObj.Id = 1;
            eventObj.Event = new Event {Id = 1};
            eventObj.TaskName = "task description";
            eventObj.PlannedDate = DateTime.Now;
            eventObj.ActualDate = DateTime.Now;
            eventObj.Information = "information";
            eventObj.LastUpdatedBy = "edm";
            eventObj.LastUpdatedDate = DateTime.Now;
            eventObj.Notes = "notes for doctype";

            return eventObj;
        }
    }
}
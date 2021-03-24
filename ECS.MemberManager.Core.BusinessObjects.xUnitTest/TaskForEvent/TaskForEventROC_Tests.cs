using System;
using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TaskForEventROC_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public TaskForEventROC_Tests()
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
        public async void TaskForEventROC_TestGetChild()
        {
            const int ID_VALUE = 999;

            var taskForEvent = BuildTaskForEvent();
            taskForEvent.Id = ID_VALUE;

            var eventObj = await TaskForEventROC.GetTaskForEventROC(taskForEvent);

            Assert.NotNull(eventObj);
            Assert.IsType<TaskForEventROC>(eventObj);
            Assert.Equal(eventObj.Id, eventObj.Id);
            Assert.Equal(eventObj.Event.Id, eventObj.Event.Id);
            Assert.Equal(eventObj.TaskName, eventObj.TaskName);
            Assert.Equal(eventObj.PlannedDate, eventObj.PlannedDate);
            Assert.Equal(eventObj.ActualDate, eventObj.ActualDate);
            Assert.Equal(eventObj.Information, eventObj.Information);
            Assert.Equal(eventObj.Notes, eventObj.Notes);
            Assert.Equal(eventObj.LastUpdatedBy, eventObj.LastUpdatedBy);
            Assert.Equal(eventObj.LastUpdatedDate, eventObj.LastUpdatedDate);
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
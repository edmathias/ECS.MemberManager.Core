using System;
using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TaskForEventECL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public TaskForEventECL_Tests()
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
        private async void TaskForEventECL_TestTaskForEventECL()
        {
            var eventObjEdit = await TaskForEventECL.NewTaskForEventECL();

            Assert.NotNull(eventObjEdit);
            Assert.IsType<TaskForEventECL>(eventObjEdit);
        }


        [Fact]
        private async void TaskForEventECL_TestGetTaskForEventECL()
        {
            var childData = MockDb.TaskForEvents;

            var listToTest = await TaskForEventECL.GetTaskForEventECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }

        private async Task BuildTaskForEvent(TaskForEventEC eventToBuild)
        {
            eventToBuild.Event = await EventEC.GetEventEC(new Event {Id = 1});
            eventToBuild.TaskName = "task description";
            eventToBuild.PlannedDate = DateTime.Now;
            eventToBuild.ActualDate = DateTime.Now;
            eventToBuild.Information = "information";
            eventToBuild.LastUpdatedBy = "edm";
            eventToBuild.LastUpdatedDate = DateTime.Now;
            eventToBuild.Notes = "notes for doctype";
        }
    }
}
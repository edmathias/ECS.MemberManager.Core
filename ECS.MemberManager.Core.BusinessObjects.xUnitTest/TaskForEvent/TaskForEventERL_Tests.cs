using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TaskForEventERL_Tests : CslaBaseTest
    {
        [Fact]
        private async void TaskForEventERL_TestNewTaskForEventERL()
        {
            var eventEdit = await TaskForEventERL.NewTaskForEventERL();

            Assert.NotNull(eventEdit);
            Assert.IsType<TaskForEventERL>(eventEdit);
        }

        [Fact]
        private async void TaskForEventERL_TestGetTaskForEventERL()
        {
            var eventEdit =
                await TaskForEventERL.GetTaskForEventERL();

            Assert.NotNull(eventEdit);
            Assert.Equal(3, eventEdit.Count);
        }

        [Fact]
        private async void TaskForEventERL_TestDeleteTaskForEventERL()
        {
            const int ID_TO_DELETE = 99;
            var eventList =
                await TaskForEventERL.GetTaskForEventERL();
            var listCount = eventList.Count;
            var eventToDelete = eventList.First(cl => cl.Id == ID_TO_DELETE);
            // remove is deferred delete
            var isDeleted = eventList.Remove(eventToDelete);

            var eventListAfterDelete = await eventList.SaveAsync();

            Assert.NotNull(eventListAfterDelete);
            Assert.IsType<TaskForEventERL>(eventListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount, eventListAfterDelete.Count);
        }

        [Fact]
        private async void TaskForEventERL_TestUpdateTaskForEventERL()
        {
            const int ID_TO_UPDATE = 1;

            var eventList =
                await TaskForEventERL.GetTaskForEventERL();
            var countBeforeUpdate = eventList.Count;
            var eventToUpdate = eventList.First(cl => cl.Id == ID_TO_UPDATE);
            eventToUpdate.Notes = "Updated Notes";

            var updatedList = await eventList.SaveAsync();

            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void TaskForEventERL_TestAddTaskForEventERL()
        {
            var eventList =
                await TaskForEventERL.GetTaskForEventERL();
            var countBeforeAdd = eventList.Count;

            var eventToAdd = eventList.AddNew();
            await BuildTaskForEvent(eventToAdd);

            var updatedTaskForEventList = await eventList.SaveAsync();

            Assert.NotEqual(countBeforeAdd, updatedTaskForEventList.Count);
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
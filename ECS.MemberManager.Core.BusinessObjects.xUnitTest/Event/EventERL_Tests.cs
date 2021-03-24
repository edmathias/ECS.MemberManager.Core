using System;
using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventERL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EventERL_Tests()
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
        private async void EventERL_TestNewEventERL()
        {
            var eventEdit = await EventERL.NewEventERL();

            Assert.NotNull(eventEdit);
            Assert.IsType<EventERL>(eventEdit);
        }

        [Fact]
        private async void EventERL_TestGetEventERL()
        {
            var eventEdit =
                await EventERL.GetEventERL();

            Assert.NotNull(eventEdit);
            Assert.Equal(3, eventEdit.Count);
        }

        [Fact]
        private async void EventERL_TestDeleteEventERL()
        {
            const int ID_TO_DELETE = 99;
            var eventList =
                await EventERL.GetEventERL();
            var listCount = eventList.Count;
            var eventToDelete = eventList.First(cl => cl.Id == ID_TO_DELETE);
            // remove is deferred delete
            var isDeleted = eventList.Remove(eventToDelete);

            var eventListAfterDelete = await eventList.SaveAsync();

            Assert.NotNull(eventListAfterDelete);
            Assert.IsType<EventERL>(eventListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount, eventListAfterDelete.Count);
        }

        [Fact]
        private async void EventERL_TestUpdateEventERL()
        {
            const int ID_TO_UPDATE = 1;

            var eventList =
                await EventERL.GetEventERL();
            var countBeforeUpdate = eventList.Count;
            var eventToUpdate = eventList.First(cl => cl.Id == ID_TO_UPDATE);
            eventToUpdate.Notes = "Updated Notes";

            var updatedList = await eventList.SaveAsync();

            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void EventERL_TestAddEventERL()
        {
            var eventList =
                await EventERL.GetEventERL();
            var countBeforeAdd = eventList.Count;

            var eventToAdd = eventList.AddNew();
            BuildEvent(eventToAdd);

            var updatedEventList = await eventList.SaveAsync();

            Assert.NotEqual(countBeforeAdd, updatedEventList.Count);
        }

        private void BuildEvent(EventEC eventToBuild)
        {
            eventToBuild.EventName = "event name";
            eventToBuild.Description = "description for doctype";
            eventToBuild.LastUpdatedBy = "test";
            eventToBuild.LastUpdatedDate = DateTime.Now;
            eventToBuild.NextDate = DateTime.Now;
            eventToBuild.Notes = "notes for doctype";
        }
    }
}
using System;
using System.IO;
using System.Linq;
using Csla;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventEditList_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EventEditList_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");
            
            if(testLibrary == "Mock")
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
        private async void EventEditList_TestGetEventEditList()
        {
            var eventEditList = await EventEditList.GetEventEditList();

            Assert.NotNull(eventEditList);
            Assert.True(eventEditList.Count>0);
        }
        
        [Fact]
        private async void EventEditList_TestDeleteEventsEntry()
        {
            const int ID_TO_DELETE = 99;
            
            var eventEditList = await EventEditList.GetEventEditList();
            var listCount = eventEditList.Count;

            var eventToDelete = eventEditList.First(a => a.Id == ID_TO_DELETE);

            // remove is deferred delete
            eventEditList.Remove(eventToDelete); 

            var eventListAfterDelete = await eventEditList.SaveAsync();
            
            Assert.NotEqual(listCount,eventListAfterDelete.Count);
        }

        [Fact]
        private async void EventEditList_TestUpdateEventsEntry()
        {
            const int ID_TO_UPDATE = 1;
            var eventList = await EventEditList.GetEventEditList();
            var countBeforeUpdate = eventList.Count;
            var eventToUpdate = eventList.First(a => a.Id == ID_TO_UPDATE);

            eventToUpdate.Description = "This was updated";
            await eventList.SaveAsync();
            
            var updatedEventsList = await EventEditList.GetEventEditList();
            
            Assert.Equal("This was updated",updatedEventsList.First(a => a.Id == ID_TO_UPDATE).Description);
            Assert.Equal(countBeforeUpdate, updatedEventsList.Count);
        }

        [Fact]
        private async void EventEditList_TestAddEventsEntry()
        {
            var eventList = await EventEditList.GetEventEditList();
            var countBeforeAdd = eventList.Count;
            
            var eventToAdd = eventList.AddNew();
            BuildEvent(eventToAdd); 

            var updatedEventsList = await eventList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedEventsList.Count);
        }

        private void BuildEvent(EventEdit eventToBuild)
        {
            eventToBuild.EventName = "new event";
            eventToBuild.NextDate = DateTime.Now.Add(TimeSpan.FromDays(365));
            eventToBuild.IsOneTime = false;
            eventToBuild.Description = "doc type description";
            eventToBuild.Notes = "document type notes";
            eventToBuild.LastUpdatedBy = "edm";
            eventToBuild.LastUpdatedDate = DateTime.Now;
        }
    }
}
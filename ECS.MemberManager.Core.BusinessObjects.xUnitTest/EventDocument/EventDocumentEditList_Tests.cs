using System;
using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventDocumentEditList_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EventDocumentEditList_Tests()
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
        private async void EventDocumentEditList_TestNewEventDocumentList()
        {
            var eventDocumentEditList = await EventDocumentEditList.NewEventDocumentEditList();

            Assert.NotNull(eventDocumentEditList);
            Assert.IsType<EventDocumentEditList>(eventDocumentEditList);
        }
        
        [Fact]
        private async void EventDocumentEditList_TestGetEventDocumentEditList()
        {
            var eventDocumentEditList = await EventDocumentEditList.GetEventDocumentEditList();

            Assert.NotNull(eventDocumentEditList);
            Assert.Equal(3, eventDocumentEditList.Count);
        }
        
        [Fact]
        private async void EventDocumentEditList_TestDeleteEventDocumentsEntry()
        {
            var eventDocumentEditList = await EventDocumentEditList.GetEventDocumentEditList();
            var listCount = eventDocumentEditList.Count;
            var eventDocumentToDelete = eventDocumentEditList.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = eventDocumentEditList.Remove(eventDocumentToDelete); 

            var eventDocumentListAfterDelete = await eventDocumentEditList.SaveAsync();

            Assert.NotNull(eventDocumentListAfterDelete);
            Assert.IsType<EventDocumentEditList>(eventDocumentListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,eventDocumentListAfterDelete.Count);
        }

        [Fact]
        private async void EventDocumentEditList_TestUpdateEventDocumentsEntry()
        {
            const int idToUpdate = 1;
            
            var eventDocumentEditList = await EventDocumentEditList.GetEventDocumentEditList();
            var countBeforeUpdate = eventDocumentEditList.Count;
            var eventDocumentToUpdate = eventDocumentEditList.First(a => a.Id == idToUpdate);
            var notesUpdate = $"This was updated {DateTime.Now}";
            eventDocumentToUpdate.Notes = notesUpdate; 

            var updatedList = await eventDocumentEditList.SaveAsync();
            
            Assert.Equal( notesUpdate,updatedList.First(a => a.Id == idToUpdate).Notes);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void EventDocumentEditList_TestAddEventDocumentsEntry()
        {
            var eventDocumentEditList = await EventDocumentEditList.GetEventDocumentEditList();
            var countBeforeAdd = eventDocumentEditList.Count;
            
            var eventDocumentToAdd = eventDocumentEditList.AddNew();
            BuildValidEventDocument(eventDocumentToAdd);

            var updatedEventDocumentsList = await eventDocumentEditList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedEventDocumentsList.Count);
        }

        private void BuildValidEventDocument(EventDocumentEdit eventDocument)
        {
            eventDocument.Notes = "notes 1";
            eventDocument.DocumentName = "name of document";
            eventDocument.EventId = 1;
            eventDocument.DocumentTypeId = 1;
            eventDocument.PathAndFileName = "path and file name";
            eventDocument.LastUpdatedBy = "edm";
            eventDocument.LastUpdatedDate = DateTime.Now;
        }
    }
}
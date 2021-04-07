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
    public class EventDocumentERL_Tests : CslaBaseTest
    {
        [Fact]
        private async void EventDocumentERL_TestEventDocumentERL()
        {
            var eventDocumentObjEdit = await EventDocumentERL.NewEventDocumentERL();

            Assert.NotNull(eventDocumentObjEdit);
            Assert.IsType<EventDocumentERL>(eventDocumentObjEdit);
        }

        [Fact]
        private async void EventDocumentERL_TestGetEventDocumentERL()
        {
            var listToTest = await EventDocumentERL.GetEventDocumentERL();

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }

        [Fact]
        private async void EventDocumentERL_TestDeleteEventDocumentEntry()
        {
            var eventDocumentObjEditList = await EventDocumentERL.GetEventDocumentERL();
            var listCount = eventDocumentObjEditList.Count;

            var eventDocumentObj = eventDocumentObjEditList.First(a => a.Id == 99);
            // remove is deferred delete
            eventDocumentObjEditList.Remove(eventDocumentObj);

            var eventDocumentObjListAfterDelete = await eventDocumentObjEditList.SaveAsync();

            Assert.NotEqual(listCount, eventDocumentObjListAfterDelete.Count);
        }

        [Fact]
        private async void EventDocumentERL_TestUpdateEventDocumentEntry()
        {
            var eventDocumentObjList = await EventDocumentERL.GetEventDocumentERL();
            var countBeforeUpdate = eventDocumentObjList.Count;
            var idToUpdate = eventDocumentObjList.Min(a => a.Id);
            var eventDocumentObjToUpdate = eventDocumentObjList.First(a => a.Id == idToUpdate);

            eventDocumentObjToUpdate.DocumentName = "This was updated";
            await eventDocumentObjList.SaveAsync();

            var updatedEventDocumentsList = await EventDocumentERL.GetEventDocumentERL();

            Assert.Equal("This was updated", updatedEventDocumentsList.First(a => a.Id == idToUpdate).DocumentName);
            Assert.Equal(countBeforeUpdate, updatedEventDocumentsList.Count);
        }

        [Fact]
        private async void EventDocumentERL_TestAddEventDocumentEntry()
        {
            var eventDocumentObjList = await EventDocumentERL.GetEventDocumentERL();
            var countBeforeAdd = eventDocumentObjList.Count;

            var eventDocumentObjToAdd = eventDocumentObjList.AddNew();
            await BuildEventDocument(eventDocumentObjToAdd);

            var eventDocumentObjEditList = await eventDocumentObjList.SaveAsync();

            Assert.NotEqual(countBeforeAdd, eventDocumentObjEditList.Count);
        }

        private async Task BuildEventDocument(EventDocumentEC eventDocumentObj)
        {
            eventDocumentObj.DocumentName = "eventDocument name";
            eventDocumentObj.Event = await EventEC.GetEventEC(new Event() {Id = 1});
            eventDocumentObj.DocumentType = await DocumentTypeEC.GetDocumentTypeEC(new DocumentType() {Id = 1});
            eventDocumentObj.Notes = "eventDocument notes";
            eventDocumentObj.PathAndFileName = "C:\\pathandfilename";
            eventDocumentObj.LastUpdatedBy = "edm";
            eventDocumentObj.LastUpdatedDate = DateTime.Now;
            eventDocumentObj.Notes = "notes for eventdocument";
        }
    }
}
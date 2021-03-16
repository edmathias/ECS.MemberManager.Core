using System;
using System.IO;
using System.Linq;
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
    public class EventMemberERL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EventMemberERL_Tests()
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
        private async void EventMemberERL_TestEventMemberERL()
        {
            var eventMemberObjEdit = await EventMemberERL.NewEventMemberERL();

            Assert.NotNull(eventMemberObjEdit);
            Assert.IsType<EventMemberERL>(eventMemberObjEdit);
        }

        [Fact]
        private async void EventMemberERL_TestGetEventMemberERL()
        {
            var listToTest = await EventMemberERL.GetEventMemberERL();

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }

        [Fact]
        private async void EventMemberERL_TestDeleteEventMemberEntry()
        {
            var eventMemberObjEditList = await EventMemberERL.GetEventMemberERL();
            var listCount = eventMemberObjEditList.Count;

            var eventMemberObj = eventMemberObjEditList.First(a => a.Id == 99);
            // remove is deferred delete
            eventMemberObjEditList.Remove(eventMemberObj);

            var eventMemberObjListAfterDelete = await eventMemberObjEditList.SaveAsync();

            Assert.NotEqual(listCount, eventMemberObjListAfterDelete.Count);
        }

        [Fact]
        private async void EventMemberERL_TestUpdateEventMemberEntry()
        {
            var eventMemberObjList = await EventMemberERL.GetEventMemberERL();
            var countBeforeUpdate = eventMemberObjList.Count;
            var idToUpdate = eventMemberObjList.Min(a => a.Id);
            var eventMemberObjToUpdate = eventMemberObjList.First(a => a.Id == idToUpdate);

            eventMemberObjToUpdate.Role = "This was updated";
            await eventMemberObjList.SaveAsync();

            var updatedEventMembersList = await EventMemberERL.GetEventMemberERL();

            Assert.Equal("This was updated", updatedEventMembersList.First(a => a.Id == idToUpdate).Role);
            Assert.Equal(countBeforeUpdate, updatedEventMembersList.Count);
        }

        [Fact]
        private async void EventMemberERL_TestAddEventMemberEntry()
        {
            var eventMemberObjList = await EventMemberERL.GetEventMemberERL();
            var countBeforeAdd = eventMemberObjList.Count;

            var eventMemberObjToAdd = await BuildEventMember(); 
            eventMemberObjList.Add(eventMemberObjToAdd);

            var eventMemberObjEditList = await eventMemberObjList.SaveAsync();

            Assert.NotEqual(countBeforeAdd, eventMemberObjEditList.Count);
        }

        private async Task<EventMemberEC> BuildEventMember()
        {
            var eventDocumentObj = await EventMemberEC.NewEventMemberEC();
            eventDocumentObj.Event = await EventEC.GetEventEC(new Event() {Id = 1});
            eventDocumentObj.MemberInfo = await MemberInfoEC.GetMemberInfoEC(new MemberInfo() {Id = 1});
            eventDocumentObj.Role = "Organizer";
            eventDocumentObj.LastUpdatedBy = "edm";
            eventDocumentObj.LastUpdatedDate = DateTime.Now;
            eventDocumentObj.Notes = "notes for doctype";

            return eventDocumentObj;
        }        
    }
}
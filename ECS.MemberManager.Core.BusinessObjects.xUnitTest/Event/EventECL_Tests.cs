using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventECL_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EventECL_Tests()
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
        private async void EventECL_TestEventECL()
        {
            var eventObjEdit = await EventECL.NewEventECL();

            Assert.NotNull(eventObjEdit);
            Assert.IsType<EventECL>(eventObjEdit);
        }

        
        [Fact]
        private async void EventECL_TestGetEventECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDal>();
            var childData = await dal.Fetch();
            
            var listToTest = await EventECL.GetEventECL(childData);
            
            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
        
        [Fact]
        private async void EventECL_TestDeleteEventEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDal>();
            var childData = await dal.Fetch();
            
            var eventObjEditList = await EventECL.GetEventECL(childData);

            var eventObj = eventObjEditList.First(a => a.Id == 99);

            // remove is deferred delete
            eventObjEditList.Remove(eventObj); 

            var eventObjListAfterDelete = await eventObjEditList.SaveAsync();
            
            Assert.NotEqual(childData.Count,eventObjListAfterDelete.Count);
        }

        [Fact]
        private async void EventECL_TestUpdateEventEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDal>();
            var childData = await dal.Fetch();
            
            var eventObjList = await EventECL.GetEventECL(childData);
            var countBeforeUpdate = eventObjList.Count;
            var idToUpdate = eventObjList.Min(a => a.Id);
            var eventObjToUpdate = eventObjList.First(a => a.Id == idToUpdate);

            eventObjToUpdate.Description = "This was updated";
            await eventObjList.SaveAsync();

            var updatedList = await dal.Fetch();
            var updatedEventsList = await EventECL.GetEventECL(updatedList);
            
            Assert.Equal("This was updated",updatedEventsList.First(a => a.Id == idToUpdate).Description);
            Assert.Equal(countBeforeUpdate, updatedEventsList.Count);
        }

        [Fact]
        private async void EventECL_TestAddEventEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDal>();
            var childData = await dal.Fetch();

            var eventObjList = await EventECL.GetEventECL(childData);
            var countBeforeAdd = eventObjList.Count;
            
            var eventObjToAdd = eventObjList.AddNew();
            BuildEvent(eventObjToAdd); 

            var eventObjEditList = await eventObjList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, eventObjEditList.Count);
        }

        private void BuildEvent(EventEC eventObj)
        {
            eventObj.EventName = "event name";
            eventObj.Description = "event description";
            eventObj.Notes = "event notes";
            eventObj.LastUpdatedBy = "edm";
            eventObj.LastUpdatedDate = DateTime.Now;
            eventObj.NextDate = DateTime.Now;
        }
        
    }
}

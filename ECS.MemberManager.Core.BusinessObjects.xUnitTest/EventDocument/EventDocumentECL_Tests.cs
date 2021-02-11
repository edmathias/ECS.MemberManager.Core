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
    public class EventDocumentECL_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EventDocumentECL_Tests()
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
        private async void EventDocumentECL_TestEventDocumentECL()
        {
            var eventDocumentObjEdit = await EventDocumentECL.NewEventDocumentECL();

            Assert.NotNull(eventDocumentObjEdit);
            Assert.IsType<EventDocumentECL>(eventDocumentObjEdit);
        }

        
        [Fact]
        private async void EventDocumentECL_TestGetEventDocumentECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDocumentDal>();
            var childData = await dal.Fetch();
            
            var listToTest = await EventDocumentECL.GetEventDocumentECL(childData);
            
            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
        
        [Fact]
        private async void EventDocumentECL_TestDeleteEventDocumentEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDocumentDal>();
            var childData = await dal.Fetch();
            
            var eventDocumentObjEditList = await EventDocumentECL.GetEventDocumentECL(childData);

            var eventDocumentObj = eventDocumentObjEditList.First(a => a.Id == 99);

            // remove is deferred delete
            eventDocumentObjEditList.Remove(eventDocumentObj); 

            var eventDocumentObjListAfterDelete = await eventDocumentObjEditList.SaveAsync();
            
            Assert.NotEqual(childData.Count,eventDocumentObjListAfterDelete.Count);
        }

        [Fact]
        private async void EventDocumentECL_TestUpdateEventDocumentEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDocumentDal>();
            var childData = await dal.Fetch();
            
            var eventDocumentObjList = await EventDocumentECL.GetEventDocumentECL(childData);
            var countBeforeUpdate = eventDocumentObjList.Count;
            var idToUpdate = eventDocumentObjList.Min(a => a.Id);
            var eventDocumentObjToUpdate = eventDocumentObjList.First(a => a.Id == idToUpdate);

            eventDocumentObjToUpdate.DocumentName = "This was updated";
            await eventDocumentObjList.SaveAsync();

            var updatedList = await dal.Fetch();
            var updatedEventDocumentsList = await EventDocumentECL.GetEventDocumentECL(updatedList);
            
            Assert.Equal("This was updated",updatedEventDocumentsList.First(a => a.Id == idToUpdate).DocumentName);
            Assert.Equal(countBeforeUpdate, updatedEventDocumentsList.Count);
        }

        [Fact]
        private async void EventDocumentECL_TestAddEventDocumentEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDocumentDal>();
            var childData = await dal.Fetch();

            var eventDocumentObjList = await EventDocumentECL.GetEventDocumentECL(childData);
            var countBeforeAdd = eventDocumentObjList.Count;
            
            var eventDocumentObjToAdd = eventDocumentObjList.AddNew();
            await BuildEventDocument(eventDocumentObjToAdd); 

            var eventDocumentObjEditList = await eventDocumentObjList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, eventDocumentObjEditList.Count);
        }

        private async Task BuildEventDocument(EventDocumentEC eventDocumentObj)
        {
            eventDocumentObj.DocumentName = "eventDocument name";
            eventDocumentObj.Event = await EventEC.GetEventEC(new Event() { Id = 1 });
            eventDocumentObj.DocumentType = await DocumentTypeEC.GetDocumentTypeEC(new DocumentType() {Id = 1});
            eventDocumentObj.Notes = "eventDocument notes";
            eventDocumentObj.PathAndFileName = "C:\\pathandfilename";
            eventDocumentObj.LastUpdatedBy = "edm";
            eventDocumentObj.LastUpdatedDate = DateTime.Now;
            eventDocumentObj.Notes = "notes for eventdocument";
        }
        
    }
}

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
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
        private async void TaskForEventECL_TestTaskForEventECL()
        {
            var eventObjEdit = await TaskForEventECL.NewTaskForEventECL();

            Assert.NotNull(eventObjEdit);
            Assert.IsType<TaskForEventECL>(eventObjEdit);
        }

        
        [Fact]
        private async void TaskForEventECL_TestGetTaskForEventECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITaskForEventDal>();
            var childData = await dal.Fetch();
            
            var listToTest = await TaskForEventECL.GetTaskForEventECL(childData);
            
            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
        
        [Fact]
        private async void TaskForEventECL_TestDeleteTaskForEventEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITaskForEventDal>();
            var childData = await dal.Fetch();
            
            var eventObjEditList = await TaskForEventECL.GetTaskForEventECL(childData);

            var eventObj = eventObjEditList.First(a => a.Id == 99);

            // remove is deferred delete
            eventObjEditList.Remove(eventObj); 

            var eventObjListAfterDelete = await eventObjEditList.SaveAsync();
            
            Assert.NotEqual(childData.Count,eventObjListAfterDelete.Count);
        }

        [Fact]
        private async void TaskForEventECL_TestUpdateTaskForEventEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITaskForEventDal>();
            var childData = await dal.Fetch();
            
            var eventObjList = await TaskForEventECL.GetTaskForEventECL(childData);
            var countBeforeUpdate = eventObjList.Count;
            var idToUpdate = eventObjList.Min(a => a.Id);
            var eventObjToUpdate = eventObjList.First(a => a.Id == idToUpdate);

            eventObjToUpdate.TaskName = "This was updated";
            await eventObjList.SaveAsync();

            var updatedList = await dal.Fetch();
            var updatedTaskForEventsList = await TaskForEventECL.GetTaskForEventECL(updatedList);
            
            Assert.Equal("This was updated",updatedTaskForEventsList.First(a => a.Id == idToUpdate).TaskName);
            Assert.Equal(countBeforeUpdate, updatedTaskForEventsList.Count);
        }

        [Fact]
        private async void TaskForEventECL_TestAddTaskForEventEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITaskForEventDal>();
            var childData = await dal.Fetch();

            var eventObjList = await TaskForEventECL.GetTaskForEventECL(childData);
            var countBeforeAdd = eventObjList.Count;
            
            var eventObjToAdd = eventObjList.AddNew();
            await BuildTaskForEvent(eventObjToAdd); 

            var eventObjEditList = await eventObjList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, eventObjEditList.Count);
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

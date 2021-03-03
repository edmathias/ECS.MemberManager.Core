using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TaskForEventER_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public TaskForEventER_Tests()
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
        public async Task TaskForEventER_TestGetTaskForEvent()
        {
            var eventObj = await TaskForEventER.GetTaskForEventER(1);

            Assert.NotNull(eventObj);
            Assert.IsType<TaskForEventER>(eventObj);
        }

        [Fact]
        public async Task TaskForEventER_TestGetNewTaskForEventER()
        {
            var eventObj = await TaskForEventER.NewTaskForEventER();

            Assert.NotNull(eventObj);
            Assert.False(eventObj.IsValid);
        }

        [Fact]
        public async Task TaskForEventER_TestUpdateExistingTaskForEventER()
        {
            var eventObj = await TaskForEventER.GetTaskForEventER(1);
            eventObj.Notes = "These are updated Notes";
            
            var result =  await eventObj.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes",result.Notes );
        }

        [Fact]
        public async Task TaskForEventER_TestInsertNewTaskForEventER()
        {
            var eventObj = await TaskForEventER.NewTaskForEventER();
            eventObj.TaskName = "event name";
            eventObj.Notes = "This person is on standby";
            eventObj.LastUpdatedBy = "edm";
            eventObj.LastUpdatedDate = DateTime.Now;
            eventObj.PlannedDate = DateTime.Now;
            eventObj.ActualDate = DateTime.Now;
            eventObj.Information = "Information line";
            var domainEvent = new Event() {Id = 1};
            eventObj.Event = await EventEC.GetEventEC(domainEvent); 

            var savedTaskForEvent = await eventObj.SaveAsync();
           
            Assert.NotNull(savedTaskForEvent);
            Assert.IsType<TaskForEventER>(savedTaskForEvent);
            Assert.True( savedTaskForEvent.Id > 0 );
        }

        [Fact]
        public async Task TaskForEventER_TestDeleteObjectFromDatabase()
        {
            const int ID_TO_DELETE = 99;
            
            await TaskForEventER.DeleteTaskForEventER(ID_TO_DELETE);

            await Assert.ThrowsAsync<DataPortalException>(() => TaskForEventER.GetTaskForEventER(ID_TO_DELETE));
        }
        
        // test invalid state 
        [Fact]
        public async Task TaskForEventER_TestTaskNameRequired() 
        {
            var eventObj = await TaskForEventER.NewTaskForEventER();
            eventObj.TaskName = "event name";
            eventObj.Notes = "This person is on standby";
            eventObj.LastUpdatedBy = "edm";
            eventObj.LastUpdatedDate = DateTime.Now;
            eventObj.PlannedDate = DateTime.Now;
            eventObj.ActualDate = DateTime.Now;
            eventObj.Information = "Information line";

            var isObjectValidInit = eventObj.IsValid;
            eventObj.TaskName = string.Empty;

            Assert.NotNull(eventObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventObj.IsValid);
            Assert.Equal("TaskName required",
                eventObj.BrokenRulesCollection[0].Description);
            
        }
       
        [Fact]
        public async Task TaskForEventER_TestTaskForEventNameExceedsMaxLengthOf50()
        {
            var eventObj = await TaskForEventER.NewTaskForEventER();
            eventObj.LastUpdatedBy = "edm";
            eventObj.LastUpdatedDate = DateTime.Now;
            eventObj.TaskName = "valid length";
            Assert.True(eventObj.IsValid);
            
            eventObj.TaskName = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                     "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                     "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                     "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(eventObj);
            Assert.False(eventObj.IsValid);
            Assert.Equal("TaskName can not exceed 50 characters",
                eventObj.BrokenRulesCollection[0].Description);
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TaskForEventER_TestInvalidSaveTaskForEventER()
        {
            var eventObj = await TaskForEventER.NewTaskForEventER();
            eventObj.TaskName = String.Empty;
            TaskForEventER savedTaskForEvent = null;
                
            Assert.False(eventObj.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() =>  eventObj.Save());
        }
    }
}

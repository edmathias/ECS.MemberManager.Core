using System;
using System.IO;
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
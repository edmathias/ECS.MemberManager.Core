using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TaskForEventROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void EventInfoList_TestGetEventInfoList()
        {
            var childData = MockDb.TaskForEvents;

            var eventInfoList = await TaskForEventROCL.GetTaskForEventROCL(childData);

            Assert.NotNull(eventInfoList);
            Assert.True(eventInfoList.IsReadOnly);
            Assert.Equal(3, eventInfoList.Count);
        }
    }
}
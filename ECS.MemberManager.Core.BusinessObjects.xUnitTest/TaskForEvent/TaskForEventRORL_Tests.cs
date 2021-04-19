using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TaskForEventRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void TaskForEventRORL_TestGetTaskForEventRORL()
        {
            var taskForEventList = await TaskForEventRORL.GetTaskForEventRORL();

            Assert.NotNull(taskForEventList);
            Assert.True(taskForEventList.IsReadOnly);
            Assert.Equal(3, taskForEventList.Count);
        }
    }
}
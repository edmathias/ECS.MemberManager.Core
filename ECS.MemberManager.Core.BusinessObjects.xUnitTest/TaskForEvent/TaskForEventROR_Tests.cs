using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TaskForEventROR_Tests
    {
        [Fact]
        public async Task TaskForEventROR_TestGetTaskForEvent()
        {
            var eventObj = await TaskForEventROR.GetTaskForEventROR(1);

            Assert.NotNull(eventObj);
            Assert.IsType<TaskForEventROR>(eventObj);
        }
    }
}
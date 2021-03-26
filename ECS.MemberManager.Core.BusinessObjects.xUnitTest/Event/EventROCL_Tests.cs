using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void EventROCL_TestGetEventROCL()
        {
            var childData = MockDb.Events;

            var eventInfoList = await EventROCL.GetEventROCL(childData);

            Assert.NotNull(eventInfoList);
            Assert.True(eventInfoList.IsReadOnly);
            Assert.Equal(3, eventInfoList.Count);
        }
    }
}
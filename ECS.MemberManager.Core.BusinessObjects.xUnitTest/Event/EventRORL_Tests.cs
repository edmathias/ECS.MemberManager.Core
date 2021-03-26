using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void EventRORL_TestGetEventRORL()
        {
            var eventInfoList = await EventRORL.GetEventRORL();

            Assert.NotNull(eventInfoList);
            Assert.True(eventInfoList.IsReadOnly);
            Assert.Equal(3, eventInfoList.Count);
        }
    }
}
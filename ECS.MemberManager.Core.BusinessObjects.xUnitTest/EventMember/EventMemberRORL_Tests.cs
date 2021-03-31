using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventMemberRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void EventMemberRORL_TestGetEventMemberRORL()
        {
            var listToTest = await EventMemberRORL.GetEventMemberRORL();

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
    }
}
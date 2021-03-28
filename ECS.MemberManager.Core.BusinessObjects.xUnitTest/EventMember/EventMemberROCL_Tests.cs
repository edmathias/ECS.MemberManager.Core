using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventMemberROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void EventMemberROCL_TestGetEventMemberROCL()
        {
            var childData = MockDb.EventMembers;

            var listToTest = await EventMemberROCL.GetEventMemberROCL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
    }
}
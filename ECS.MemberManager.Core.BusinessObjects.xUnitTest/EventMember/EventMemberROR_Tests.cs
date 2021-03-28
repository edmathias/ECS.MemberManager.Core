using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventMemberROR_Tests : CslaBaseTest
    {
        [Fact]
        public async Task EventMemberROR_TestGetEventMember()
        {
            var eventMemberObj = await EventMemberROR.GetEventMemberROR(1);

            Assert.NotNull(eventMemberObj);
            Assert.IsType<EventMemberROR>(eventMemberObj);
            Assert.Equal(1, eventMemberObj.Id);
        }
    }
}
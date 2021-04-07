using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventDocumentROR_Tests : CslaBaseTest
    {
        [Fact]
        public async Task EventDocumentROR_TestGetEventDocument()
        {
            var eventDocumentObj = await EventDocumentROR.GetEventDocumentROR(1);

            Assert.NotNull(eventDocumentObj);
            Assert.IsType<EventDocumentROR>(eventDocumentObj);
            Assert.Equal(1, eventDocumentObj.Id);
        }
    }
}
using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventDocumentRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void EventDocumentRORL_TestGetEventDocumentRORL()
        {
            var listToTest = await EventDocumentRORL.GetEventDocumentRORL();

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
    }
}
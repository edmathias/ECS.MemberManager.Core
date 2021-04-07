using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventDocumentROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void EventDocumentROCL_TestGetEventDocumentROCL()
        {
            var childData = MockDb.EventDocuments;

            var listToTest = await EventDocumentROCL.GetEventDocumentROCL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
    }
}
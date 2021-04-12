using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Sdk;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OfficeRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void OfficeRORL_TestGetOfficeRORL()
        {
            var listToTest = await OfficeRORL.GetOfficeRORL();

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
    }
}
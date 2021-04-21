using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TermInOfficeRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void TermInOfficeRORL_TestGetTermInOfficeRORL()
        {
            var eventEdit =
                await TermInOfficeRORL.GetTermInOfficeRORL();

            Assert.NotNull(eventEdit);
            Assert.Equal(3, eventEdit.Count);
        }
    }
}
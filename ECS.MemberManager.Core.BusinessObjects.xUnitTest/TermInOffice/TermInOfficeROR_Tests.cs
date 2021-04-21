using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TermInOfficeROR_Tests : CslaBaseTest
    {
        [Fact]
        public async Task TermInOfficeROR_TestGetTermInOffice()
        {
            var termObj = await TermInOfficeROR.GetTermInOfficeROR(1);

            Assert.NotNull(termObj);
            Assert.IsType<TermInOfficeROR>(termObj);
            Assert.Equal(1, termObj.Id);
            Assert.True(termObj.IsValid);
        }
    }
}
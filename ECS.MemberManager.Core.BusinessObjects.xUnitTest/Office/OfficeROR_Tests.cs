using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OfficeROR_Tests : CslaBaseTest
    {
        [Fact]
        public async Task OfficeROR_TestGetOffice()
        {
            var officeObj = await OfficeROR.GetOfficeROR(1);

            Assert.NotNull(officeObj);
            Assert.IsType<OfficeROR>(officeObj);
        }
    }
}
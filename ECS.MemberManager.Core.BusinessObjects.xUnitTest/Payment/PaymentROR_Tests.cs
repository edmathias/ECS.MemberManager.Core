using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentROR_Tests : CslaBaseTest
    {
        [Fact]
        public async Task PaymentROR_Get()
        {
            var organization = await PaymentROR.GetPaymentROR(1);

            Assert.NotNull(organization);
            Assert.IsType<PaymentROR>(organization);
            Assert.Equal(1, organization.Id);
            Assert.True(organization.IsValid);
        }
    }
}
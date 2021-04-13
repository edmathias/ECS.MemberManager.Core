using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OrganizationTypeRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void OrganizationTypeRORL_TestGetOrganizationTypeRORL()
        {
            var categoryOfOrganizationTypeInfoList = await OrganizationTypeRORL.GetOrganizationTypeRORL();

            Assert.NotNull(categoryOfOrganizationTypeInfoList);
            Assert.True(categoryOfOrganizationTypeInfoList.IsReadOnly);
            Assert.Equal(3, categoryOfOrganizationTypeInfoList.Count);
        }
    }
}
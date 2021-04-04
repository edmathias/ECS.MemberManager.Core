using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OrganizationTypeROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void OrganizationTypeROCL_TestGetOrganizationTypeROCL()
        {
            var categoryList = MockDb.OrganizationTypes;

            var categoryOfOrganizationInfoList =
                await OrganizationTypeROCL.GetOrganizationTypeROCL(categoryList);

            Assert.NotNull(categoryOfOrganizationInfoList);
            Assert.True(categoryOfOrganizationInfoList.IsReadOnly);
            Assert.Equal(3, categoryOfOrganizationInfoList.Count);
        }
    }
}
using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OfficeROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void OfficeROCL_TestGetOfficeROCL()
        {
            var childData = MockDb.Offices;

            var listToTest = await OfficeROCL.GetOfficeROCL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
    }
}
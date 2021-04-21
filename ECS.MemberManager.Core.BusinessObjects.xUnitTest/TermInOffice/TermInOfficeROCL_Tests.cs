using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TermInOfficeROCL_Tests : CslaBaseTest
    {

        [Fact]
        private async void TermInOfficeROCL_TestGetTermInOfficeROCL()
        {
            var childData = MockDb.TermInOffices;

            var listToTest = await TermInOfficeROCL.GetTermInOfficeROCL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
    }
}
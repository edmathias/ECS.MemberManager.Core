using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TermInOfficeECL_Tests : CslaBaseTest
    {
        [Fact]
        private async void TermInOfficeECL_TestTermInOfficeECL()
        {
            var eventObjEdit = await TermInOfficeECL.NewTermInOfficeECL();

            Assert.NotNull(eventObjEdit);
            Assert.IsType<TermInOfficeECL>(eventObjEdit);
        }


        [Fact]
        private async void TermInOfficeECL_TestGetTermInOfficeECL()
        {
            var childData = MockDb.TermInOffices;

            var listToTest = await TermInOfficeECL.GetTermInOfficeECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
    }
}
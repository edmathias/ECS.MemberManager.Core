using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EMailTypeECL_Tests : CslaBaseTest
    {
        [Fact]
        private async void EMailTypeECL_TestEMailTypeECL()
        {
            var eMailTypeEdit = await EMailTypeECL.NewEMailTypeECL();

            Assert.NotNull(eMailTypeEdit);
            Assert.IsType<EMailTypeECL>(eMailTypeEdit);
        }


        [Fact]
        private async void EMailTypeECL_TestGetEMailTypeECL()
        {
            var childData = MockDb.EMailTypes;
            var listToTest = await EMailTypeECL.GetEMailTypeECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }

        private void BuildEMailType(EMailTypeEC eMailType)
        {
            eMailType.Description = "doc type description";
            eMailType.Notes = "document type notes";
        }
    }
}
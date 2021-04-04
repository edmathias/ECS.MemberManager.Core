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
    public class MemberStatusECL_Tests : CslaBaseTest
    {
        [Fact]
        private async void MemberStatusECL_TestMemberStatusECL()
        {
            var memberStatusEdit = await MemberStatusECL.NewMemberStatusECL();

            Assert.NotNull(memberStatusEdit);
            Assert.IsType<MemberStatusECL>(memberStatusEdit);
        }


        [Fact]
        private async void MemberStatusECL_TestGetMemberStatusECL()
        {
            var childData = MockDb.MemberStatuses;
            var listToTest = await MemberStatusECL.GetMemberStatusECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }


        private void BuildMemberStatus(MemberStatusEC memberStatus)
        {
            memberStatus.Description = "doc type description";
            memberStatus.Notes = "document type notes";
        }
    }
}
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MemberInfoECL_Tests : CslaBaseTest
    {
        [Fact]
        private async void MemberInfoECL_TestMemberInfoECL()
        {
            var memberInfoObjEdit = await MemberInfoECL.NewMemberInfoECL();

            Assert.NotNull(memberInfoObjEdit);
            Assert.IsType<MemberInfoECL>(memberInfoObjEdit);
        }


        [Fact]
        private async void MemberInfoECL_TestGetMemberInfoECL()
        {
            var childData = MockDb.MemberInfo;

            var listToTest = await MemberInfoECL.GetMemberInfoECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }


        private Task<MemberInfo> BuildMemberInfo()
        {
            var memberInfo = new MemberInfo();
            memberInfo.Notes = "member info notes";
            memberInfo.MemberNumber = "9876543";
            memberInfo.Person = MockDb.Persons.Take(1).First();
            memberInfo.DateFirstJoined = DateTime.Now;
            memberInfo.MembershipType = MockDb.MembershipTypes.Take(1).First();
            memberInfo.PrivacyLevel = MockDb.PrivacyLevels.Take(1).First();
            memberInfo.MemberStatus = MockDb.MemberStatuses.Take(1).First();
            memberInfo.LastUpdatedBy = "edm";
            memberInfo.LastUpdatedDate = DateTime.Now;

            return Task.FromResult(memberInfo);
        }
    }
}
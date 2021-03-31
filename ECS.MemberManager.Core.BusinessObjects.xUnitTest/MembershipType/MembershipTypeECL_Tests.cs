using System;
using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MembershipTypeECL_Tests : CslaBaseTest
    {
        [Fact]
        private async void MembershipTypeECL_TestMembershipTypeECL()
        {
            var eventObjEdit = await MembershipTypeECL.NewMembershipTypeECL();

            Assert.NotNull(eventObjEdit);
            Assert.IsType<MembershipTypeECL>(eventObjEdit);
        }


        [Fact]
        private async void MembershipTypeECL_TestGetMembershipTypeECL()
        {
            var childData = MockDb.MembershipTypes;
            var listToTest = await MembershipTypeECL.GetMembershipTypeECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }

        private void BuildMembershipType(MembershipTypeEC eventObj)
        {
            eventObj.Description = "event description";
            eventObj.Level = 1;
            eventObj.LastUpdatedBy = "edm";
            eventObj.LastUpdatedDate = DateTime.Now;
            eventObj.Notes = "event notes";
        }
    }
}
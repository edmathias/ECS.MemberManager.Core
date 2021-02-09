using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MembershipTypeROR_Tests
    {

        [Fact]
        public async void MembershipTypeROR_TestGetById()
        {
            var membershipTypeObj = await MembershipTypeROR.GetMembershipTypeROR(1);
            
            Assert.NotNull(membershipTypeObj);
            Assert.IsType<MembershipTypeROR>(membershipTypeObj);
            Assert.Equal(1, membershipTypeObj.Id);
        }
    }
}
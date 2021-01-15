using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MembershipTypeInfo_Tests
    {

        [Fact]
        public async void MembershipTypeInfo_TestGetById()
        {
            var membershipTypeInfo = await MembershipTypeInfo.GetMembershipTypeInfo(1);
            
            Assert.NotNull(membershipTypeInfo);
            Assert.IsType<MembershipTypeInfo>(membershipTypeInfo);
            Assert.Equal(1, membershipTypeInfo.Id);
        }

        [Fact]
        public async void MembershipTypeInfo_TestGetChild()
        {
            const int ID_VALUE = 999;
            
            var membershipType = new MembershipType()
            {
                Id = ID_VALUE,
                Description = "membership type description",
                Level = 1,
                LastUpdatedBy = "edm",
                LastUpdatedDate = DateTime.Now,
                Notes = "membershipType type notes"
            };

            var membershipTypeTypeInfo = await MembershipTypeInfo.GetMembershipTypeInfo(membershipType);
            
            Assert.NotNull(membershipTypeTypeInfo);
            Assert.IsType<MembershipTypeInfo>(membershipTypeTypeInfo);
            Assert.Equal(ID_VALUE, membershipTypeTypeInfo.Id);

        }
    }
}
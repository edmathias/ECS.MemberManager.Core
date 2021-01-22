using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MemberStatusInfo_Tests
    {
        [Fact]
        public async void MemberStatusInfo_TestGetById()
        {
            var organizationTypeInfo = await MemberStatusInfo.GetMemberStatusInfo(1);
            
            Assert.NotNull(organizationTypeInfo);
            Assert.IsType<MemberStatusInfo>(organizationTypeInfo);
            Assert.Equal(1, organizationTypeInfo.Id);
        }

        [Fact]
        public async void MemberStatusInfo_TestGetChild()
        {
            const int ID_VALUE = 999;
            
            var organizationType = new MemberStatus()
            {
                Id = ID_VALUE,
                Description = "organization type description",
                Notes = "organization type notes"
            };

            var organizationTypeInfo = await MemberStatusInfo.GetMemberStatusInfo(organizationType);
            
            Assert.NotNull(organizationTypeInfo);
            Assert.IsType<MemberStatusInfo>(organizationTypeInfo);
            Assert.Equal(ID_VALUE, organizationTypeInfo.Id);
            Assert.Equal("organization type description",organizationTypeInfo.Description);
            Assert.Equal("organization type notes",organizationTypeInfo.Notes);

        }
    }
}
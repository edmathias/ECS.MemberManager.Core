using System;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MembershipTypeROC_Tests
    {
        [Fact]
        public async void MembershipTypeROC_TestGetChild()
        {
            const int ID_VALUE = 999;

            var membershipType = BuildMembershipType();
            membershipType.Id = ID_VALUE;

            var membershipTypeObj = await MembershipTypeROC.GetMembershipTypeROC(membershipType);

            Assert.NotNull(membershipTypeObj);
            Assert.IsType<MembershipTypeROC>(membershipTypeObj);
            Assert.Equal(membershipTypeObj.Id, membershipTypeObj.Id);
            Assert.Equal(membershipTypeObj.Description, membershipTypeObj.Description);
            Assert.Equal(membershipTypeObj.Notes, membershipTypeObj.Notes);
            Assert.Equal(membershipTypeObj.LastUpdatedBy, membershipTypeObj.LastUpdatedBy);
            Assert.Equal(membershipTypeObj.LastUpdatedDate, membershipTypeObj.LastUpdatedDate);
            Assert.Equal(membershipTypeObj.RowVersion, membershipTypeObj.RowVersion);
        }

        private MembershipType BuildMembershipType()
        {
            var membershipTypeObj = new MembershipType();
            membershipTypeObj.Id = 1;
            membershipTypeObj.Description = "test description";
            membershipTypeObj.LastUpdatedBy = "edm";
            membershipTypeObj.LastUpdatedDate = DateTime.Now;
            membershipTypeObj.Notes = "notes for doctype";

            return membershipTypeObj;
        }
    }
}
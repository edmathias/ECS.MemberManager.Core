using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MemberStatusROR_Tests
    {

        [Fact]
        public async void MemberStatusROR_TestGetById()
        {
            var category = await MemberStatusROR.GetMemberStatusROR(1);
            
            Assert.NotNull(category);
            Assert.IsType<MemberStatusROR>(category);
            Assert.Equal(1, category.Id);
        }
    }
}
using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfPersonROR_Tests
    {

        [Fact]
        public async void CategoryOfPersonROR_TestGetById()
        {
            var address = await CategoryOfPersonROR.GetCategoryOfPersonROR(1);
            
            Assert.NotNull(address);
            Assert.IsType<CategoryOfPersonROR>(address);
            Assert.Equal(1, address.Id);
        }
    }
}
using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfPersonInfo_Tests
    {

        [Fact]
        public async void CategoryOfPersonInfo_TestGetById()
        {
            var addressInfo = await CategoryOfPersonROR.GetCategoryOfPersonROR(1);
            
            Assert.NotNull(addressInfo);
            Assert.IsType<CategoryOfPersonROR>(addressInfo);
            Assert.Equal(1, addressInfo.Id);
        }
    }
}
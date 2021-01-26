using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfOrganizationROR_Tests
    {

        [Fact]
        public async void CategoryOfOrganizationInfo_TestGetById()
        {
            var categoryOfOrganizationInfo = await CategoryOfOrganizationROR.GetCategoryOfOrganizationROR(1);
            
            Assert.NotNull(categoryOfOrganizationInfo);
            Assert.IsType<CategoryOfOrganizationROR>(categoryOfOrganizationInfo);
            Assert.Equal(1, categoryOfOrganizationInfo.Id);
        }
    }
}
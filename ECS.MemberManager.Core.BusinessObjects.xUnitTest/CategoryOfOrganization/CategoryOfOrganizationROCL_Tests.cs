using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfOrganizationROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void CategoryOfOrganizationROCL_TestGetCategoryOfOrganizationROCL()
        {
            var categoryList = BuildCategoryOfOrganizationList();

            var categoryOfOrganizationInfoList =
                await CategoryOfOrganizationROCL.GetCategoryOfOrganizationROCL(categoryList);

            Assert.NotNull(categoryOfOrganizationInfoList);
            Assert.True(categoryOfOrganizationInfoList.IsReadOnly);
            Assert.Equal(3, categoryOfOrganizationInfoList.Count);
        }

        private List<CategoryOfOrganization> BuildCategoryOfOrganizationList()
        {
            return new List<CategoryOfOrganization>()
            {
                new CategoryOfOrganization()
                {
                    Id = 1,
                    Category = "Org Category 1",
                    DisplayOrder = 0,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new CategoryOfOrganization()
                {
                    Id = 2,
                    Category = "Org Category 2",
                    DisplayOrder = 1,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new CategoryOfOrganization()
                {
                    Id = 99,
                    Category = "Org to delete",
                    DisplayOrder = 1,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
            };
        }
    }
}
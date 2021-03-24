using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfPersonECL_Tests : CslaBaseTest
    {
        [Fact]
        private async void CategoryOfPersonECL_TestNewCategoryOfPersonECL()
        {
            var categoryOfPersonEdit = await CategoryOfPersonECL.NewCategoryOfPersonECL();

            Assert.NotNull(categoryOfPersonEdit);
            Assert.IsType<CategoryOfPersonECL>(categoryOfPersonEdit);
        }

        [Fact]
        private async void CategoryOfPersonECL_TestGetCategoryOfPersonECL()
        {
            var childData = BuildCategoryOfPersonList();
            var categoryOfPersonEdit = await CategoryOfPersonECL.GetCategoryOfPersonECL(childData);

            Assert.NotNull(categoryOfPersonEdit);
            Assert.Equal(3, categoryOfPersonEdit.Count);
        }

        private void BuildCategoryOfPerson(CategoryOfPersonEC categoryToBuild)
        {
            categoryToBuild.Category = "test";
            categoryToBuild.DisplayOrder = 1;
        }

        private List<CategoryOfPerson> BuildCategoryOfPersonList()
        {
            return new List<CategoryOfPerson>()
            {
                new CategoryOfPerson()
                {
                    Id = 1,
                    Category = "Person Category 1",
                    DisplayOrder = 0,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new CategoryOfPerson()
                {
                    Id = 2,
                    Category = "Org Category 2",
                    DisplayOrder = 1,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new CategoryOfPerson()
                {
                    Id = 99,
                    Category = "Org Category 2",
                    DisplayOrder = 1,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
            };
        }
    }
}
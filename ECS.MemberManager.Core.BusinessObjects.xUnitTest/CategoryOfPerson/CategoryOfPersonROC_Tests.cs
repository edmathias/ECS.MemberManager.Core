using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfPersonROC_Tests
    {

        [Fact]
        public async void CategoryOfPersonROC_TestGetChild()
        {
            const int ID_VALUE = 999;
            
            var categoryOfPersonType = new CategoryOfPerson()
            {
                Id = ID_VALUE,
                Category = "person category 1",
                DisplayOrder = 1
            };

            var categoryOfPersonTypeInfo = await CategoryOfPersonROC.GetCategoryOfPersonROC(categoryOfPersonType);
            
            Assert.NotNull(categoryOfPersonTypeInfo);
            Assert.IsType<CategoryOfPersonROC>(categoryOfPersonTypeInfo);
            Assert.Equal(categoryOfPersonType.Id, categoryOfPersonTypeInfo.Id);
            Assert.Equal(categoryOfPersonType.Category, categoryOfPersonTypeInfo.Category);
            Assert.Equal(categoryOfPersonType.DisplayOrder, categoryOfPersonTypeInfo.DisplayOrder);
         }
    }
}
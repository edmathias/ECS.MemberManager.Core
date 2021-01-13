using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfPersonInfoTests
    {

        [Fact]
        public async void CategoryOfPersonInfo_TestGetById()
        {
            var addressInfo = await CategoryOfPersonInfo.GetCategoryOfPersonInfo(1);
            
            Assert.NotNull(addressInfo);
            Assert.IsType<CategoryOfPersonInfo>(addressInfo);
            Assert.Equal(1, addressInfo.Id);
        }

        [Fact]
        public async void CategoryOfPersonInfo_TestGetChild()
        {
            const int ID_VALUE = 999;
            
            var addressType = new CategoryOfPerson()
            {
                Id = ID_VALUE,
                Category = "Category name",
                DisplayOrder = 1
            };

            var addressTypeInfo = await CategoryOfPersonInfo.GetCategoryOfPersonInfo(addressType);
            
            Assert.NotNull(addressTypeInfo);
            Assert.IsType<CategoryOfPersonInfo>(addressTypeInfo);
            Assert.Equal(ID_VALUE, addressTypeInfo.Id);

        }
    }
}
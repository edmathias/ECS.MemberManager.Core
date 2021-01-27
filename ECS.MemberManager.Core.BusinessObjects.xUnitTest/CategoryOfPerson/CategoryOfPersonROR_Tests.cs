﻿using System;
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
            var category = await CategoryOfPersonROR.GetCategoryOfPersonROR(1);
            
            Assert.NotNull(category);
            Assert.IsType<CategoryOfPersonROR>(category);
            Assert.Equal(1, category.Id);
        }
    }
}
using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TitleROR_Tests
    {

        [Fact]
        public async void TitleROR_TestGetById()
        {
            var title = await TitleROR.GetTitleROR(1);
            
            Assert.NotNull(title);
            Assert.IsType<TitleROR>(title);
            Assert.Equal(1, title.Id);
        }
    }
}
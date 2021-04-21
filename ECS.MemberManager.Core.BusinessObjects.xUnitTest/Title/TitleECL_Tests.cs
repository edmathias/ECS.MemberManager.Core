using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TitleECL_Tests : CslaBaseTest
    {
        [Fact]
        private async void TitleECL_TestTitleECL()
        {
            var titleEdit = await TitleECL.NewTitleECL();

            Assert.NotNull(titleEdit);
            Assert.IsType<TitleECL>(titleEdit);
        }

        [Fact]
        private async void TitleECL_TestGetTitleECL()
        {
            var childData = MockDb.Titles;

            var listToTest = await TitleECL.GetTitleECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }

        private void BuildTitle(TitleEC title)
        {
            title.Abbreviation = "abbr";
            title.Description = "test description";
            title.DisplayOrder = 1;
        }
    }
}
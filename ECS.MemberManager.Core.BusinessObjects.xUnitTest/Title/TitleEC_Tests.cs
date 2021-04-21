using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TitleEC_Tests : CslaBaseTest
    {
        [Fact]
        public async Task TestTitleEC_NewTitleEC()
        {
            var title = await TitleEC.NewTitleEC();

            Assert.NotNull(title);
            Assert.IsType<TitleEC>(title);
            Assert.False(title.IsValid);
        }

        [Fact]
        public async Task TestTitleEC_GetTitleEC()
        {
            var titleToLoad = BuildTitle();
            var title = await TitleEC.GetTitleEC(titleToLoad);

            Assert.NotNull(title);
            Assert.IsType<TitleEC>(title);
            Assert.Equal(titleToLoad.Id, title.Id);
            Assert.Equal(titleToLoad.Abbreviation, title.Abbreviation);
            Assert.Equal(titleToLoad.Description, title.Description);
            Assert.Equal(titleToLoad.DisplayOrder, title.DisplayOrder);
            Assert.Equal(titleToLoad.RowVersion, title.RowVersion);
            Assert.True(title.IsValid);
        }

        [Fact]
        public async Task TestTitleEC_AbbreviationRequired()
        {
            var titleToTest = BuildTitle();
            var title = await TitleEC.GetTitleEC(titleToTest);
            var isObjectValidInit = title.IsValid;
            title.Abbreviation = string.Empty;

            Assert.NotNull(title);
            Assert.True(isObjectValidInit);
            Assert.False(title.IsValid);
            Assert.Equal("Abbreviation", title.BrokenRulesCollection[0].Property);
            Assert.Equal("Abbreviation required", title.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestTitleEC_DescriptionLessThan50Chars()
        {
            var titleToTest = BuildTitle();
            var title = await TitleEC.GetTitleEC(titleToTest);
            var isObjectValidInit = title.IsValid;
            title.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(title);
            Assert.True(isObjectValidInit);
            Assert.False(title.IsValid);
            Assert.Equal("Description", title.BrokenRulesCollection[0].Property);
        }

        private Title BuildTitle()
        {
            var title = new Title();
            title.Id = 1;
            title.Abbreviation = "abbreviation";
            title.Description = "test description";
            title.DisplayOrder = 1;

            return title;
        }
    }
}
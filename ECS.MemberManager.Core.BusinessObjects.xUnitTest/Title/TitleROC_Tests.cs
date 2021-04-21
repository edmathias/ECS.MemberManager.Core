using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TitleROC_Tests : CslaBaseTest
    {
        [Fact]
        public async void TitleROC_TestGetChild()
        {
            const int ID_VALUE = 999;

            var titlebuild = BuildTitle();
            titlebuild.Id = ID_VALUE;

            var title = await TitleROC.GetTitleROC(titlebuild);

            Assert.NotNull(title);
            Assert.IsType<TitleROC>(title);
            Assert.Equal(titlebuild.Abbreviation, title.Abbreviation);
            Assert.Equal(titlebuild.Id, title.Id);
            Assert.Equal(titlebuild.Description, title.Description);
            Assert.Equal(titlebuild.RowVersion, title.RowVersion);
        }

        private Title BuildTitle()
        {
            var title = new Title();
            title.Id = 1;
            title.Description = "test description";
            title.Abbreviation = "abbrev";
            title.DisplayOrder = 1;

            return title;
        }
    }
}
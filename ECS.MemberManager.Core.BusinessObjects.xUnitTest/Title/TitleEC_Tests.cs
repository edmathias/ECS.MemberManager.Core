using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TitleEC_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public TitleEC_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");

            if (testLibrary == "Mock")
                MockDb.ResetMockDb();
            else
            {
                if (!IsDatabaseBuilt)
                {
                    var adoDb = new ADODb();
                    adoDb.BuildMemberManagerADODb();
                    IsDatabaseBuilt = true;
                }
            }
        }

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
            Assert.Equal(titleToLoad.Id,title.Id);
            Assert.Equal(titleToLoad.Abbreviation, title.Abbreviation);
            Assert.Equal(titleToLoad.Description,title.Description);
            Assert.Equal(titleToLoad.DisplayOrder, title.DisplayOrder);
            Assert.Equal(titleToLoad.RowVersion, title.RowVersion);
            Assert.True(title.IsValid);
        }

        [Fact]
        public async Task TestTitleEC_DescriptionRequired()
        {
            var titleToTest = BuildTitle();
            var title = await TitleEC.GetTitleEC(titleToTest);
            var isObjectValidInit = title.IsValid;
            title.Description = string.Empty;

            Assert.NotNull(title);
            Assert.True(isObjectValidInit);
            Assert.False(title.IsValid);
            Assert.Equal("Description",title.BrokenRulesCollection[0].Property);
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
            Assert.Equal("Description",title.BrokenRulesCollection[0].Property);
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
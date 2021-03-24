using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TitleECL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public TitleECL_Tests()
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
        private async void TitleECL_TestTitleECL()
        {
            var titleEdit = await TitleECL.NewTitleECL();

            Assert.NotNull(titleEdit);
            Assert.IsType<TitleECL>(titleEdit);
        }

        [Fact]
        private async void TitleECL_TestGetTitleECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();
            var childData = await dal.Fetch();

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
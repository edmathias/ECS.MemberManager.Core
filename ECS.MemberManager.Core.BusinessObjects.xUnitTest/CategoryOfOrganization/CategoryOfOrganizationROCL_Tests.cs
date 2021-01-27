using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfOrganizationROCL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public CategoryOfOrganizationROCL_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");
            
            if(testLibrary == "Mock")
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
        private async void CategoryOfOrganizationROCL_TestGetCategoryOfOrganizationROCL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();
            var categoryList = await dal.Fetch();
            
            var categoryOfOrganizationInfoList = 
                await CategoryOfOrganizationROCL.GetCategoryOfOrganizationROCL(categoryList);
            
            Assert.NotNull(categoryOfOrganizationInfoList);
            Assert.True(categoryOfOrganizationInfoList.IsReadOnly);
            Assert.Equal(3, categoryOfOrganizationInfoList.Count);
        }
      
    }
}
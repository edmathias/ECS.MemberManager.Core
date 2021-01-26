using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfPersonInfoChildList_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public CategoryOfPersonInfoChildList_Tests()
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
        private async void CategoryOfPersonInfoChildList_TestGetCategoryOfPersonInfoChildList()
        {
            var categoryOfPersonInfoList = await CategoryOfPersonROCL.GetCategoryOfPersonROCL();
            
            Assert.NotNull(categoryOfPersonInfoList);
            Assert.True(categoryOfPersonInfoList.IsReadOnly);
            Assert.Equal(3, categoryOfPersonInfoList.Count);
        }
        
        private CategoryOfPerson BuildCategoryOfPerson()
        {
            var categoryToBuild = new CategoryOfPerson();
            categoryToBuild.Category = "test";
            categoryToBuild.DisplayOrder = 1;

            return categoryToBuild;
        }
      
    }
}
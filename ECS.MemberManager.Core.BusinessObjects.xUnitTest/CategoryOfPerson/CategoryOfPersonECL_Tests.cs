using System;
using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfPersonECL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public CategoryOfPersonECL_Tests()
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
        private async void CategoryOfPersonECL_TestNewCategoryOfPersonECL()
        {
            var categoryOfPersonEdit = await CategoryOfPersonECL.NewCategoryOfPersonECL();

            Assert.NotNull(categoryOfPersonEdit);
            Assert.IsType<CategoryOfPersonECL>(categoryOfPersonEdit);
        }
        
        [Fact]
        private async void CategoryOfPersonECL_TestGetCategoryOfPersonECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfPersonDal>();
            var childData = await dal.Fetch();
            var categoryOfPersonEdit = await CategoryOfPersonECL.GetCategoryOfPersonECL(childData);

            Assert.NotNull(categoryOfPersonEdit);
            Assert.Equal(3, categoryOfPersonEdit.Count);
        }

        private void BuildCategoryOfPerson(CategoryOfPersonEC categoryToBuild)
        {
            categoryToBuild.Category = "test";
            categoryToBuild.DisplayOrder = 1;
        }
    }
}
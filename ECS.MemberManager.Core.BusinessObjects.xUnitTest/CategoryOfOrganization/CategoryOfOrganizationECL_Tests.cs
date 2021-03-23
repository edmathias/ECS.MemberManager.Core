using System;
using System.Collections.Generic;
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
    public class CategoryOfOrganizationECL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public CategoryOfOrganizationECL_Tests()
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
        private async void CategoryOfOrganizationECL_TestNewCategoryOfOrganizationECL()
        {
            var categoryOfOrganizationErl = await CategoryOfOrganizationECL.NewCategoryOfOrganizationECL();

            Assert.NotNull(categoryOfOrganizationErl);
            Assert.IsType<CategoryOfOrganizationECL>(categoryOfOrganizationErl);
        }
        
        [Fact]
        private async void CategoryOfOrganizationECL_TestGetCategoryOfOrganizationECL()
        {
            var categoryOfOrganizations = BuildCategoryOfOrganizations();
            var categoryOfOrganizationECL = await CategoryOfOrganizationECL.GetCategoryOfOrganizationECL(categoryOfOrganizations);

            Assert.NotNull(categoryOfOrganizationECL);
            Assert.Equal(3, categoryOfOrganizationECL.Count);
        }

        private void BuildValidCategoryOfOrganization(CategoryOfOrganizationEC categoryOfOrganization)
        {
            categoryOfOrganization.Category = "org category";
            categoryOfOrganization.DisplayOrder = 1;
        }

        private List<CategoryOfOrganization> BuildCategoryOfOrganizations()
        {
            return new List<CategoryOfOrganization>()
            {
                new CategoryOfOrganization()
                {
                    Id = 1,
                    Category = "Org Category 1",
                    DisplayOrder = 0,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new CategoryOfOrganization()
                {
                    Id = 2,
                    Category = "Org Category 2",
                    DisplayOrder = 1,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new CategoryOfOrganization()
                {
                    Id = 99,
                    Category = "Org to delete",
                    DisplayOrder = 1,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
            };
            
        }
 
    }
}
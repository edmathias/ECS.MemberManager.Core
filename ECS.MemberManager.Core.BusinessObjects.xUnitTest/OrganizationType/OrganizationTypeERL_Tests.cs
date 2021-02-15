using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OrganizationTypeERL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public OrganizationTypeERL_Tests()
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
        private async void OrganizationTypeERL_TestNewOrganizationTypeERL()
        {
            var categoryOfOrganizationEdit = await OrganizationTypeERL.NewOrganizationTypeERL();

            Assert.NotNull(categoryOfOrganizationEdit);
            Assert.IsType<OrganizationTypeERL>(categoryOfOrganizationEdit);
        }
        
        [Fact]
        private async void OrganizationTypeERL_TestGetOrganizationTypeERL()
        {
            var categoryOfOrganizationEdit = 
                await OrganizationTypeERL.GetOrganizationTypeERL();

            Assert.NotNull(categoryOfOrganizationEdit);
            Assert.Equal(3, categoryOfOrganizationEdit.Count);
        }
        
        [Fact]
        private async void OrganizationTypeERL_TestDeleteOrganizationTypeERL()
        {
            const int ID_TO_DELETE = 99;
            var categoryList = 
                await OrganizationTypeERL.GetOrganizationTypeERL();
            var listCount = categoryList.Count;
            var categoryToDelete = categoryList.First(cl => cl.Id == ID_TO_DELETE);
            // remove is deferred delete
            var isDeleted = categoryList.Remove(categoryToDelete); 

            var categoryOfOrganizationListAfterDelete = await categoryList.SaveAsync();

            Assert.NotNull(categoryOfOrganizationListAfterDelete);
            Assert.IsType<OrganizationTypeERL>(categoryOfOrganizationListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,categoryOfOrganizationListAfterDelete.Count);
        }

        [Fact]
        private async void OrganizationTypeERL_TestUpdateOrganizationTypeERL()
        {
            const int ID_TO_UPDATE = 1;
            
            var categoryList = 
                await OrganizationTypeERL.GetOrganizationTypeERL();
            var countBeforeUpdate = categoryList.Count;
            var categoryOfOrganizationToUpdate = categoryList.First(cl => cl.Id == ID_TO_UPDATE);
            categoryOfOrganizationToUpdate.Name = "Updated category";
            
            var updatedList = await categoryList.SaveAsync();
            
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void OrganizationTypeERL_TestAddOrganizationTypeERL()
        {
            var categoryList = 
                await OrganizationTypeERL.GetOrganizationTypeERL();
            var countBeforeAdd = categoryList.Count;
            
            var categoryOfOrganizationToAdd = categoryList.AddNew();
            await BuildOrganizationType(categoryOfOrganizationToAdd);

            var updatedCategoryList = await categoryList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedCategoryList.Count);
        }

        private async Task BuildOrganizationType(OrganizationTypeEC categoryToBuild)
        {
            categoryToBuild.Name = "org name";
            categoryToBuild.Notes = "notes for org type";
            categoryToBuild.CategoryOfOrganization = await CategoryOfOrganizationEC.GetCategoryOfOrganizationEC(
                new CategoryOfOrganization
                {
                    Id = 1,
                    Category = "category of org name",
                    DisplayOrder = 1
                });
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OrganizationTypeECL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public OrganizationTypeECL_Tests()
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
        private async void OrganizationTypeECL_TestNewOrganizationTypeECL()
        {
            var organizationTypeECL = await OrganizationTypeECL.NewOrganizationTypeECL();

            Assert.NotNull(organizationTypeECL);
            Assert.IsType<OrganizationTypeECL>(organizationTypeECL);
        }
        
        [Fact]
        private async void OrganizationTypeECL_TestGetOrganizationTypeECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();
            var organizationTypes = await dal.Fetch();

            var organizationTypeECL = await OrganizationTypeECL.GetOrganizationTypeECL(organizationTypes);

            Assert.NotNull(organizationTypeECL);
            Assert.Equal(3, organizationTypeECL.Count);
        }
        
        [Fact]
        private async void OrganizationTypeECL_TestDeleteOrganizationTypeEC()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();
            var organizationTypes = await dal.Fetch();

            var organizationTypeErl = await OrganizationTypeECL.GetOrganizationTypeECL(organizationTypes);
            var listCount = organizationTypeErl.Count;
            var organizationTypeToDelete = organizationTypeErl.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = organizationTypeErl.Remove(organizationTypeToDelete); 

            var organizationTypeListAfterDelete = await organizationTypeErl.SaveAsync();

            Assert.NotNull(organizationTypeListAfterDelete);
            Assert.IsType<OrganizationTypeECL>(organizationTypeListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,organizationTypeListAfterDelete.Count);
        }

        [Fact]
        private async void OrganizationTypeECL_TestUpdateOrganizationECEntry()
        {
            const int idToUpdate = 1;
            
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();
            var organizationTypes = await dal.Fetch();
            
            var organizationTypeECL = await OrganizationTypeECL.GetOrganizationTypeECL(organizationTypes);
            var countBeforeUpdate = organizationTypeECL.Count;
            var organizationTypeToUpdate = organizationTypeECL.First(a => a.Id == idToUpdate);
            organizationTypeToUpdate.Name = "This was updated";

            var updatedList = await organizationTypeECL.SaveAsync();
            
            Assert.Equal("This was updated",updatedList.First(a => a.Id == idToUpdate).Name);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void OrganizationTypeECL_TestAddOrganizationTypeECEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();
            var organizationTypes = await dal.Fetch();

            var organizationTypeECL = await OrganizationTypeECL.GetOrganizationTypeECL(organizationTypes);
            var countBeforeAdd = organizationTypeECL.Count;
            
            var organizationTypeToAdd = organizationTypeECL.AddNew();
            await BuildValidOrganizationType(organizationTypeToAdd);

            var updatedList = await organizationTypeECL.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedList.Count);
        }

        private async Task BuildValidOrganizationType(OrganizationTypeEC organizationType)
        {
            organizationType.Name = "org name";
            organizationType.Notes = "org notes";
            organizationType.CategoryOfOrganization = await CategoryOfOrganizationEC.GetCategoryOfOrganizationEC( new CategoryOfOrganization
            {
              Id  = 1,
              Category = "category name",
              DisplayOrder = 1
            });
             
        }
        
 
    }
}
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Xunit;
using DalManager = ECS.MemberManager.Core.DataAccess.ADO.DalManager;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OrganizationTypeEditList_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public OrganizationTypeEditList_Tests()
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
        private async void OrganizationTypeEditList_TestNewOrganizationTypeList()
        {
            var organizationTypeEdit = await OrganizationTypeEditList.NewOrganizationTypeEditList();

            Assert.NotNull(organizationTypeEdit);
            Assert.IsType<OrganizationTypeEditList>(organizationTypeEdit);
        }
        
        [Fact]
        private async void OrganizationTypeEditList_TestGetOrganizationTypeEditList()
        {
            var organizationTypeEdit = await OrganizationTypeEditList.GetOrganizationTypeEditList();

            Assert.NotNull(organizationTypeEdit);
            Assert.Equal(3, organizationTypeEdit.Count);
        }
        
        [Fact]
        private async void OrganizationTypeEditList_TestDeleteOrganizationTypesEntry()
        {
            var organizationTypeEditList = await OrganizationTypeEditList.GetOrganizationTypeEditList();
            var listCount = organizationTypeEditList.Count;
            var organizationTypeToDelete = organizationTypeEditList.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = organizationTypeEditList.Remove(organizationTypeToDelete); 

            var organizationTypeListAfterDelete = await organizationTypeEditList.SaveAsync();

            Assert.NotNull(organizationTypeListAfterDelete);
            Assert.IsType<OrganizationTypeEditList>(organizationTypeListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,organizationTypeListAfterDelete.Count);
        }

        [Fact]
        private async void OrganizationTypeEditList_TestUpdateOrganizationTypesEntry()
        {
            const int idToUpdate = 1;
            
            var organizationTypeEditList = await OrganizationTypeEditList.GetOrganizationTypeEditList();
            var countBeforeUpdate = organizationTypeEditList.Count;
            var organizationTypeToUpdate = organizationTypeEditList.First(a => a.Id == idToUpdate);
            organizationTypeToUpdate.Name = "This was updated";

            var updatedList = await organizationTypeEditList.SaveAsync();
            
            Assert.Equal("This was updated",updatedList.First(a => a.Id == idToUpdate).Name);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void OrganizationTypeEditList_TestAddOrganizationTypesEntry()
        {
            var organizationTypeEditList = await OrganizationTypeEditList.GetOrganizationTypeEditList();
            var countBeforeAdd = organizationTypeEditList.Count;
            
            var organizationTypeToAdd = organizationTypeEditList.AddNew();
            await BuildOrganizationType(organizationTypeToAdd);

            var updatedOrganizationTypesList = await organizationTypeEditList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedOrganizationTypesList.Count);
        }

        private async Task BuildOrganizationType(OrganizationTypeEdit organizationTypeToBuild)
        {
            organizationTypeToBuild.Name = "organization type description";
            organizationTypeToBuild.Notes = "notes for organization type";
            organizationTypeToBuild.CategoryOfOrganization =
                await CategoryOfOrganizationEdit.GetCategoryOfOrganizationEdit(1);
        }
    }
}
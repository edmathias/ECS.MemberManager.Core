using System;
using System.IO;
using System.Linq;
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
    public class OrganizationEditList_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public OrganizationEditList_Tests()
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
        private async void OrganizationEditList_TestNewOrganizationList()
        {
            var organizationEdit = await OrganizationEditList.NewOrganizationEditList();

            Assert.NotNull(organizationEdit);
            Assert.IsType<OrganizationEditList>(organizationEdit);
        }
        
        [Fact]
        private async void OrganizationEditList_TestGetOrganizationEditList()
        {
            var organizationEdit = await OrganizationEditList.GetOrganizationEditList();

            Assert.NotNull(organizationEdit);
            Assert.Equal(3, organizationEdit.Count);
        }
        
        [Fact]
        private async void OrganizationEditList_TestDeleteOrganizationsEntry()
        {
            var organizationEdit = await OrganizationEditList.GetOrganizationEditList();
            var listCount = organizationEdit.Count;
            var organizationToDelete = organizationEdit.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = organizationEdit.Remove(organizationToDelete); 

            var organizationListAfterDelete = await organizationEdit.SaveAsync();

            Assert.NotNull(organizationListAfterDelete);
            Assert.IsType<OrganizationEditList>(organizationListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,organizationListAfterDelete.Count);
        }

        [Fact]
        private async void OrganizationEditList_TestUpdateOrganizationsEntry()
        {
            const int idToUpdate = 1;
            
            var organizationEditList = await OrganizationEditList.GetOrganizationEditList();
            var countBeforeUpdate = organizationEditList.Count;
            var organizationToUpdate = organizationEditList.First(a => a.Id == idToUpdate);
            organizationToUpdate.Name = "This was updated";

            var updatedList = await organizationEditList.SaveAsync();
            
            Assert.Equal("This was updated",updatedList.First(a => a.Id == idToUpdate).Name);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void OrganizationEditList_TestAddOrganizationsEntry()
        {
            var organizationEditList = await OrganizationEditList.GetOrganizationEditList();
            var countBeforeAdd = organizationEditList.Count;
            
            var organizationToAdd = organizationEditList.AddNew();
            BuildOrganization(organizationToAdd);

            var updatedOrganizationsList = await organizationEditList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedOrganizationsList.Count);
        }

        private void BuildOrganization(OrganizationEdit organizationToBuild)
        {
            organizationToBuild.Name = "organization name";
            organizationToBuild.OrganizationTypeId = 1;
            organizationToBuild.Notes = "notes for org";
            organizationToBuild.LastUpdatedBy = "edm";
            organizationToBuild.LastUpdatedDate = DateTime.Now;
            organizationToBuild.DateOfFirstContact = DateTime.Now;
        }
        
 
    }
}
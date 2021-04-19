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
    public class OrganizationERL_Tests : CslaBaseTest
    {
        [Fact]
        private async void OrganizationERL_TestNewOrganizationList()
        {
            var organizationEdit = await OrganizationERL.NewOrganizationERL();

            Assert.NotNull(organizationEdit);
            Assert.IsType<OrganizationERL>(organizationEdit);
        }

        [Fact]
        private async void OrganizationERL_TestGetOrganizationERL()
        {
            var organizationEdit = await OrganizationERL.GetOrganizationERL();

            Assert.NotNull(organizationEdit);
            Assert.Equal(3, organizationEdit.Count);
        }

        [Fact]
        private async void OrganizationERL_TestDeleteOrganizationsEntry()
        {
            var organizationEdit = await OrganizationERL.GetOrganizationERL();
            var listCount = organizationEdit.Count;
            var organizationToDelete = organizationEdit.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = organizationEdit.Remove(organizationToDelete);

            var organizationListAfterDelete = await organizationEdit.SaveAsync();

            Assert.NotNull(organizationListAfterDelete);
            Assert.IsType<OrganizationERL>(organizationListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount, organizationListAfterDelete.Count);
        }

        [Fact]
        private async void OrganizationERL_TestUpdateOrganizationsEntry()
        {
            const int idToUpdate = 1;

            var organizationEditList = await OrganizationERL.GetOrganizationERL();
            var countBeforeUpdate = organizationEditList.Count;
            var organizationToUpdate = organizationEditList.First(a => a.Id == idToUpdate);
            organizationToUpdate.Name = "This was updated";

            var updatedList = await organizationEditList.SaveAsync();

            Assert.Equal("This was updated", updatedList.First(a => a.Id == idToUpdate).Name);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void OrganizationERL_TestAddOrganizationsEntry()
        {
            var organizationEditList = await OrganizationERL.GetOrganizationERL();
            var countBeforeAdd = organizationEditList.Count;

            var organizationToAdd = organizationEditList.AddNew();
            await BuildOrganization(organizationToAdd);

            var updatedOrganizationsList = await organizationEditList.SaveAsync();

            Assert.NotEqual(countBeforeAdd, updatedOrganizationsList.Count);
        }

        private async Task BuildOrganization(OrganizationEC organizationToBuild)
        {
            organizationToBuild.Name = "organization name";
            organizationToBuild.OrganizationType =
                await OrganizationTypeEC.GetOrganizationTypeEC(new OrganizationType() {Id = 1});
            organizationToBuild.CategoryOfOrganization =
                await CategoryOfOrganizationEC.GetCategoryOfOrganizationEC(new CategoryOfOrganization() {Id = 1});
            organizationToBuild.Notes = "notes for org";
            organizationToBuild.LastUpdatedBy = "edm";
            organizationToBuild.LastUpdatedDate = DateTime.Now;
            organizationToBuild.DateOfFirstContact = DateTime.Now;
        }
    }
}
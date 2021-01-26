using System;
using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class ContactForSponsorECL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public ContactForSponsorECL_Tests()
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
        private async void ContactForSponsorECL_TestNewContactForSponsorECL()
        {
            var contactForSponsorEdit = await ContactForSponsorECL.NewContactForSponsorECL();

            Assert.NotNull(contactForSponsorEdit);
            Assert.IsType<ContactForSponsorECL>(contactForSponsorEdit);
        }
        
        [Fact]
        private async void ContactForSponsorECL_TestGetContactForSponsorECL()
        {
            var contacts = MockDb.ContactForSponsors.ToList();
            var contactForSponsorEdit = await ContactForSponsorECL.GetContactForSponsorECL(contacts); 
            Assert.NotNull(contactForSponsorEdit);
            Assert.Equal(3, contactForSponsorEdit.Count);
        }
        
        [Fact]
        private async void ContactForSponsorECL_TestDeleteContactForSponsorEditEntry()
        {
            var contacts = MockDb.ContactForSponsors.ToList();
            var contactForSponsorEdit = await ContactForSponsorECL.GetContactForSponsorECL(contacts);
            var listCount = contactForSponsorEdit.Count;
            var contactForSponsorToDelete = contactForSponsorEdit.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = contactForSponsorEdit.Remove(contactForSponsorToDelete); 

            var contactForSponsorListAfterDelete = await contactForSponsorEdit.SaveAsync();

            Assert.NotNull(contactForSponsorListAfterDelete);
            Assert.IsType<ContactForSponsorECL>(contactForSponsorListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,contactForSponsorListAfterDelete.Count);
        }

        [Fact]
        private async void ContactForSponsorECL_TestUpdateContactForSponsorEditEntry()
        {
            const int idToUpdate = 1;
            
            var contacts = MockDb.ContactForSponsors.ToList();
            var contactForSponsorEditList = await ContactForSponsorECL.GetContactForSponsorECL(contacts);
            var countBeforeUpdate = contactForSponsorEditList.Count;
            var contactForSponsorToUpdate = contactForSponsorEditList.First(a => a.Id == idToUpdate);
            contactForSponsorToUpdate.LastUpdatedBy= "edm";
            
            var updatedList = await contactForSponsorEditList.SaveAsync();
            
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void ContactForSponsorECL_TestAddContactForSponsorEditEntry()
        {
            var contacts = MockDb.ContactForSponsors.ToList();
            var contactForSponsorEditList = await ContactForSponsorECL.GetContactForSponsorECL(contacts);
            var countBeforeAdd = contactForSponsorEditList.Count;
            
            var contactForSponsorToAdd = contactForSponsorEditList.AddNew();
            BuildContactForSponsor(contactForSponsorToAdd);

            var updatedContactForSponsorECL = await contactForSponsorEditList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedContactForSponsorECL.Count);
        }

        private void BuildContactForSponsor(ContactForSponsorEC categoryToBuild)
        {
            categoryToBuild.Id = 0;
            categoryToBuild.Notes = "contact notes";
            categoryToBuild.Purpose = "contact purpose";
            categoryToBuild.PersonId = 1;
            categoryToBuild.SponsorId = 1;
            categoryToBuild.DateWhenContacted = DateTime.Now;
            categoryToBuild.LastUpdatedBy = "edm";
            categoryToBuild.LastUpdatedDate = DateTime.Now;
            categoryToBuild.RecordOfDiscussion = "blah blah blah";
        }
        
    }
}
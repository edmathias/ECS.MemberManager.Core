using System;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class ContactForSponsorERL_Tests : CslaBaseTest
    {
        [Fact]
        private async void ContactForSponsorERL_TestNewContactForSponsorERL()
        {
            var contactForSponsorERL = await ContactForSponsorERL.NewContactForSponsorERL();

            Assert.NotNull(contactForSponsorERL);
            Assert.IsType<ContactForSponsorERL>(contactForSponsorERL);
        }


        [Fact]
        private async void ContactForSponsorERL_GetContactForSponsorList()
        {
            var contactForSponsorErl = await ContactForSponsorERL.GetContactForSponsorERL();

            Assert.NotNull(contactForSponsorErl);
            Assert.True(contactForSponsorErl.Count > 0);
        }

        [Fact]
        private async void ContactForSponsorERL_DeleteContactForSponsorEntry()
        {
            const int ID_TO_DELETE = 99;
            var contactList =
                await ContactForSponsorERL.GetContactForSponsorERL();
            var listCount = contactList.Count;
            var categoryToDelete = contactList.First(cl => cl.Id == ID_TO_DELETE);
            // remove is deferred delete
            var isDeleted = contactList.Remove(categoryToDelete);

            var contactForSponsorListAfterDelete = await contactList.SaveAsync();

            Assert.NotNull(contactForSponsorListAfterDelete);
            Assert.IsType<ContactForSponsorERL>(contactForSponsorListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount, contactForSponsorListAfterDelete.Count);
        }


        [Fact]
        private async void ContactForSponsorERL_TestUpdateContactForSponsorERL()
        {
            const int ID_TO_UPDATE = 1;

            var contactList =
                await ContactForSponsorERL.GetContactForSponsorERL();
            var countBeforeUpdate = contactList.Count;
            var contactForSponsorToUpdate = contactList.First(cl => cl.Id == ID_TO_UPDATE);
            contactForSponsorToUpdate.Notes = "Updated notes";

            var updatedList = await contactList.SaveAsync();

            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void ContactForSponsorERL_TestAddContactForSponsorERL()
        {
            var contactList =
                await ContactForSponsorERL.GetContactForSponsorERL();
            var countBeforeAdd = contactList.Count;

            var contactForSponsorToAdd = contactList.AddNew();
            await BuildContactForSponsor(contactForSponsorToAdd);

            var updatedCategoryList = await contactList.SaveAsync();

            Assert.NotEqual(countBeforeAdd, updatedCategoryList.Count);
        }

        private async Task BuildContactForSponsor(ContactForSponsorEC contactForSponsor)
        {
            var sponsor = MockDb.Sponsors.First();
            var person = MockDb.Persons.First();
            contactForSponsor.Sponsor = await SponsorEC.GetSponsorEC(sponsor);
            contactForSponsor.Person = await PersonEC.GetPersonEC(person);
            contactForSponsor.DateWhenContacted = DateTime.Now;
            contactForSponsor.Purpose = "purpose for contact";
            contactForSponsor.RecordOfDiscussion = "this was discussed";
            contactForSponsor.Notes = "notes for contact";
            contactForSponsor.LastUpdatedBy = "edm";
            contactForSponsor.LastUpdatedDate = DateTime.Now;
            contactForSponsor.Notes = "notes for doctype";
        }

    }
}
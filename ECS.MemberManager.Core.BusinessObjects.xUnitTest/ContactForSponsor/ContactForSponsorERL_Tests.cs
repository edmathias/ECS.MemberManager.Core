using System;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    [Collection("ContactForSponsor")]
    public class ContactForSponsorERL_Tests
    {
        public ContactForSponsorERL_Tests()
        {
            MockDb.ResetMockDb();
        }
        
        [Fact]
        private async void ContactForSponsorERL_GetAddressList()
        {
            var listToTest = MockDb.ContactForSponsors;
            
            var contactForSponsorErl = await ContactForSponsorERL.GetContactForSponsorList(listToTest);

            Assert.NotNull(contactForSponsorErl);
            Assert.Equal(MockDb.ContactForSponsors.Count, contactForSponsorErl.Count);
        }
        
        [Fact]
        private async void ContactForSponsorERL_DeleteAddressEntry()
        {
            var listToTest = MockDb.ContactForSponsors;
            var listCount = listToTest.Count;
            
            var idToDelete = MockDb.ContactForSponsors.Max(a => a.Id);
            var contactForSponsorErl = await ContactForSponsorERL.GetContactForSponsorList(listToTest);

            var contactForSponsor = contactForSponsorErl.First(a => a.Id == idToDelete);

            // remove is deferred delete
            contactForSponsorErl.Remove(contactForSponsor); 

            var contactForSponsorListAfterDelete = await contactForSponsorErl.SaveAsync();
            
            Assert.NotEqual(listCount,contactForSponsorListAfterDelete.Count);
        }

        [Fact]
        private async void ContactForSponsorERL_UpdateAddressEntry()
        {
            var contactForSponsorList = await ContactForSponsorERL.GetContactForSponsorList(MockDb.ContactForSponsors);
            var countBeforeUpdate = contactForSponsorList.Count;
            var idToUpdate = MockDb.ContactForSponsors.Min(a => a.Id);
            var contactForSponsorToUpdate = contactForSponsorList.First(a => a.Id == idToUpdate);

            contactForSponsorToUpdate.Notes = "This was updated";
            await contactForSponsorList.SaveAsync();
            
            var updatedAddressList = await ContactForSponsorERL.GetContactForSponsorList(MockDb.ContactForSponsors);
            
            Assert.Equal("This was updated",updatedAddressList.First(a => a.Id == idToUpdate).Notes);
            Assert.Equal(countBeforeUpdate, updatedAddressList.Count);
        }

        [Fact]
        private async void ContactForSponsorERL_AddAddressEntry()
        {
            var contactForSponsorList = await ContactForSponsorERL.GetContactForSponsorList(MockDb.ContactForSponsors);
            var countBeforeAdd = contactForSponsorList.Count;
            
            var contactForSponsorToAdd = contactForSponsorList.AddNew();
            BuildAddress(contactForSponsorToAdd);

            await contactForSponsorList.SaveAsync();
            
            var updatedAddressList = await ContactForSponsorERL.GetContactForSponsorList(MockDb.ContactForSponsors);
            
            Assert.NotEqual(countBeforeAdd, updatedAddressList.Count);
            
        }
        
        void BuildAddress(ContactForSponsorEC contactForSponsor)
        {
            contactForSponsor.LastUpdatedBy = "edm";
            contactForSponsor.Notes = "This person is on standby";
        }
        
    }
}
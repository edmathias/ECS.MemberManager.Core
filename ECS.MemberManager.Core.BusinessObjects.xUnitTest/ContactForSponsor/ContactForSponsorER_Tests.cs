using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class ContactForSponsorER_Tests
    {
        public ContactForSponsorER_Tests()
        {
            MockDb.ResetMockDb();
        }
        
        [Fact]
        public async Task ContactForSponsorER_TestGetContactForSponsor()
        {
            var fetchId = MockDb.ContactForSponsors.Min(dt => dt.Id);
            var contactForSponsor = await ContactForSponsorER.GetContactForSponsor(fetchId);
            var expectedContact = MockDb.ContactForSponsors.First(a => a.Id == fetchId);
            
            Assert.Equal(fetchId,contactForSponsor.Id);
            Assert.True(contactForSponsor.IsValid);
            Assert.Equal(expectedContact.Notes,contactForSponsor.Notes);
            Assert.Equal(expectedContact.LastUpdatedBy,contactForSponsor.LastUpdatedBy);
        }

        [Fact]
        public async Task ContactForSponsorER_TestNewContactForSponsor()
        {
            var contactForSponsor = await ContactForSponsorER.NewContactForSponsor();

            Assert.NotNull(contactForSponsor);
            Assert.False(contactForSponsor.IsValid);
        }

        [Fact]
        public async void ContactForSponsorER_TestUpdateContactForSponsor()
        {
            var fetchId = MockDb.ContactForSponsors.Min(dt => dt.Id);
            var contactForSponsor = await ContactForSponsorER.GetContactForSponsor(fetchId);
            contactForSponsor.Notes = "These are updated Notes";

            var result = await contactForSponsor.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes",result.Notes );
        }

        [Fact]
        public async Task ContactForSponsorER_TestInsertContactForSponsor()
        {
            var contactForSponsor = await ContactForSponsorER.NewContactForSponsor();
            BuildContactForSponsor(contactForSponsor);

            var savedContactForSponsor = await contactForSponsor.SaveAsync();

            Assert.NotNull(savedContactForSponsor);
            Assert.IsType<ContactForSponsorER>(savedContactForSponsor);
            Assert.True(savedContactForSponsor.Id > 0);
        }

        [Fact]
        public async Task ContactForSponsorER_TestDeleteContactForSponsor()
        {
            var deleteId = MockDb.ContactForSponsors.Max(a => a.Id);
            
            var beforeCount = MockDb.ContactForSponsors.Count();

            await ContactForSponsorER.DeleteContactForSponsorER(deleteId);

            Assert.NotEqual(MockDb.ContactForSponsors.Count(),beforeCount );
        }

        // test invalid state 
         
        [Fact]
        public async Task ContactForSponsorER_TestInvalidSaveContactForSponsor()
        {
            var ContactForSponsor = await ContactForSponsorER.NewContactForSponsor();
            ContactForSponsorER savedContactForSponsor = null;

            Assert.False(ContactForSponsor.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedContactForSponsor = ContactForSponsor.Save());
        }

        void BuildContactForSponsor(ContactForSponsorER contactForSponsorER)
        {
            contactForSponsorER.LastUpdatedDate = DateTime.Now;
            contactForSponsorER.LastUpdatedBy = "edm";
            contactForSponsorER.Notes = "This person is on standby";
        }
    }
}
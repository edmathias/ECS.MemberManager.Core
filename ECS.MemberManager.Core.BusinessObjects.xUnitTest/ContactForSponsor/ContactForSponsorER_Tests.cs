using System;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class ContactForSponsorER_Tests : CslaBaseTest
    {
        [Fact]
        public async Task ContactForSponsorER_TestGetContactForSponsor()
        {
            var contactForSponsor = await ContactForSponsorER.GetContactForSponsorER(1);

            Assert.NotNull(contactForSponsor);
            Assert.IsType<ContactForSponsorER>(contactForSponsor);
        }

        [Fact]
        public async Task ContactForSponsorER_TestGetNewContactForSponsorER()
        {
            var contactForSponsor = await ContactForSponsorER.NewContactForSponsorER();

            Assert.NotNull(contactForSponsor);
            Assert.False(contactForSponsor.IsValid);
        }

        [Fact]
        public async Task ContactForSponsorER_TestUpdateExistingContactForSponsorER()
        {
            var contactForSponsor = await ContactForSponsorER.GetContactForSponsorER(1);
            contactForSponsor.Notes = "These are updated Notes";

            var result = await contactForSponsor.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes", result.Notes);
        }

        [Fact]
        public async Task ContactForSponsorER_TestUpdatePersonId()
        {
            const int CONTACT_ID = 1;
            const int NEW_PERSON_ID = 2;
            var contactForSponsor = await ContactForSponsorER.GetContactForSponsorER(1);

            var newPerson = MockDb.Persons.First(p => p.Id == NEW_PERSON_ID);
            contactForSponsor.Person = await PersonEC.GetPersonEC(newPerson);

            var result = await contactForSponsor.SaveAsync();

            var fetchResult = await ContactForSponsorER.GetContactForSponsorER(CONTACT_ID);

            Assert.NotNull(fetchResult);
            Assert.Equal(2, fetchResult.Person.Id);
        }

        [Fact]
        public async Task ContactForSponsorER_TestUpdateSponsorId()
        {
            const int CONTACT_ID = 1;
            const int NEW_SPONSOR_ID = 2;
            var contactForSponsor = await ContactForSponsorER.GetContactForSponsorER(1);

            var newPerson = MockDb.Sponsors.First(s => s.Id == NEW_SPONSOR_ID);
            contactForSponsor.Sponsor = await SponsorEC.GetSponsorEC(newPerson);

            var result = await contactForSponsor.SaveAsync();

            var fetchResult = await ContactForSponsorER.GetContactForSponsorER(CONTACT_ID);

            Assert.NotNull(fetchResult);
            Assert.Equal(2, fetchResult.Sponsor.Id);
        }

        [Fact]
        public async Task ContactForSponsorER_TestInsertNewContactForSponsorER()
        {
            var contactForSponsor = await ContactForSponsorER.NewContactForSponsorER();
            contactForSponsor.Sponsor = await BuildSponsorEC();
            contactForSponsor.Person = await BuildPersonEC();
            contactForSponsor.DateWhenContacted = DateTime.Now;
            contactForSponsor.Purpose = "purpose for contact";
            contactForSponsor.RecordOfDiscussion = "this was discussed";
            contactForSponsor.Notes = "notes for contact";
            contactForSponsor.LastUpdatedBy = "edm";
            contactForSponsor.LastUpdatedDate = DateTime.Now;
            contactForSponsor.Notes = "notes for doctype";

            var savedContactForSponsor = await contactForSponsor.SaveAsync();

            Assert.NotNull(savedContactForSponsor);
            Assert.IsType<ContactForSponsorER>(savedContactForSponsor);
            Assert.True(savedContactForSponsor.Id > 0);
        }

        [Fact]
        public async Task ContactForSponsorER_TestDeleteObjectFromDatabase()
        {
            const int ID_TO_DELETE = 99;

            await ContactForSponsorER.DeleteContactForSponsorER(ID_TO_DELETE);

            await Assert.ThrowsAsync<DataPortalException>(
                () => ContactForSponsorER.GetContactForSponsorER(ID_TO_DELETE));
        }


        [Fact]
        public async Task ContactForSponsorER_TestPurposeExceedsMaxLengthOf255()
        {
            var contactForSponsor = await ContactForSponsorER.NewContactForSponsorER();
            contactForSponsor.LastUpdatedBy = "edm";
            contactForSponsor.LastUpdatedDate = DateTime.Now;
            contactForSponsor.Purpose = "valid length";
            Assert.True(contactForSponsor.IsValid);

            contactForSponsor.Purpose =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(contactForSponsor);
            Assert.False(contactForSponsor.IsValid);
            Assert.Equal("Purpose", contactForSponsor.BrokenRulesCollection[0].Property);
            Assert.Equal("Purpose can not exceed 255 characters",
                contactForSponsor.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task ContactForSponsorEC_LastUpdatedByRequired()
        {
            var contactToTest = BuildContactForSponsor();
            var contact = await ContactForSponsorEC.GetContactForSponsorEC(contactToTest);
            var isObjectValidInit = contact.IsValid;
            contact.LastUpdatedBy = string.Empty;

            Assert.NotNull(contact);
            Assert.True(isObjectValidInit);
            Assert.False(contact.IsValid);
            Assert.Equal("LastUpdatedBy", contact.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy required", contact.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task ContactForSponsorER_TestLastUpdatedByExceedsMaxLengthOf255()
        {
            var contactForSponsor = await ContactForSponsorER.NewContactForSponsorER();
            contactForSponsor.LastUpdatedBy = "edm";
            contactForSponsor.LastUpdatedDate = DateTime.Now;
            contactForSponsor.Purpose = "valid length";
            var isObjectValidInit = contactForSponsor.IsValid;
            contactForSponsor.LastUpdatedBy =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(contactForSponsor);
            Assert.True(isObjectValidInit);
            Assert.False(contactForSponsor.IsValid);
            Assert.Equal("LastUpdatedBy", contactForSponsor.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy can not exceed 255 characters",
                contactForSponsor.BrokenRulesCollection[0].Description);
        }

        // test exception if attempt to save in invalid state
        [Fact]
        public async Task ContactForSponsorEC_LastUpdatedDateRequired()
        {
            var contactToTest = BuildContactForSponsor();
            var contact = await ContactForSponsorEC.GetContactForSponsorEC(contactToTest);
            var isObjectValidInit = contact.IsValid;
            contact.LastUpdatedDate = DateTime.MinValue;

            Assert.NotNull(contact);
            Assert.True(isObjectValidInit);
            Assert.False(contact.IsValid);
            Assert.Equal("LastUpdatedDate", contact.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedDate required", contact.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task ContactForSponsorER_TestInvalidSaveContactForSponsorER()
        {
            var contactForSponsor = await ContactForSponsorER.NewContactForSponsorER();
            contactForSponsor.LastUpdatedBy = String.Empty;
            ContactForSponsorER savedContactForSponsor = null;

            Assert.False(contactForSponsor.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedContactForSponsor = contactForSponsor.Save());
        }

        private ContactForSponsor BuildContactForSponsor()
        {
            var contactForSponsor = new ContactForSponsor();
            contactForSponsor.Id = 1;
            contactForSponsor.Sponsor = new Sponsor() {Id = 1};
            contactForSponsor.Person = new Person() {Id = 1};
            contactForSponsor.DateWhenContacted = DateTime.Now;
            contactForSponsor.Purpose = "purpose for contact";
            contactForSponsor.RecordOfDiscussion = "this was discussed";
            contactForSponsor.Notes = "notes for contact";
            contactForSponsor.LastUpdatedBy = "edm";
            contactForSponsor.LastUpdatedDate = DateTime.Now;
            contactForSponsor.Notes = "notes for doctype";

            return contactForSponsor;
        }

        private async Task<SponsorEC> BuildSponsorEC()
        {
            var sponsor = MockDb.Sponsors.First();

            return await SponsorEC.GetSponsorEC(sponsor);
        }

        private async Task<PersonEC> BuildPersonEC()
        {
            var person = MockDb.Persons.First();

            return await PersonEC.GetPersonEC(person);
        }

        private async Task<PersonEC> BuildNewPersonEC()
        {
            var newPerson = await PersonEC.NewPersonEC();
            newPerson.Title = await TitleEC.GetTitleEC(new Title() {Id = 1});
            newPerson.LastName = "Jones";
            newPerson.MiddleName = String.Empty;
            newPerson.FirstName = "Jack";
            newPerson.DateOfFirstContact = new SmartDate(DateTime.Now);
            newPerson.BirthDate = new SmartDate(DateTime.Now);
            newPerson.LastUpdatedBy = "markk";
            newPerson.LastUpdatedDate = new SmartDate(DateTime.Now);
            newPerson.Code = "new code";
            newPerson.Notes = "new notes";
            newPerson.EMail = await EMailEC.GetEMailEC(new EMail() {Id = 1});

            return newPerson;
        }
    }
}
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
    public class ContactForSponsorECL_Tests : CslaBaseTest
    {
        [Fact]
        private async void ContactForSponsorECL_TestContactForSponsorECL()
        {
            var contactForSponsorEdit = await ContactForSponsorECL.NewContactForSponsorECL();

            Assert.NotNull(contactForSponsorEdit);
            Assert.IsType<ContactForSponsorECL>(contactForSponsorEdit);
        }


        [Fact]
        private async void ContactForSponsorECL_TestGetContactForSponsorECL()
        {
            var childData = MockDb.ContactForSponsors;

            var listToTest = await ContactForSponsorECL.GetContactForSponsorECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }

        private async Task<ContactForSponsor> BuildContactForSponsor()
        {
            var contactForSponsor = new ContactForSponsor();
            contactForSponsor.Id = 999;
            contactForSponsor.Sponsor = await BuildSponsor();
            contactForSponsor.Person = await BuildPerson();
            contactForSponsor.DateWhenContacted = DateTime.Now;
            contactForSponsor.Purpose = "purpose for contact";
            contactForSponsor.RecordOfDiscussion = "this was discussed";
            contactForSponsor.Notes = "notes for contact";
            contactForSponsor.LastUpdatedBy = "edm";
            contactForSponsor.LastUpdatedDate = DateTime.Now;
            contactForSponsor.Notes = "notes for doctype";

            return contactForSponsor;
        }

        private Task<Sponsor> BuildSponsor()
        {
            var childData = MockDb.Sponsors;

            return Task.FromResult(childData.First());
        }

        private Task<Person> BuildPerson()
        {
            var childData = MockDb.Persons;

            return Task.FromResult(childData.First());
        }
    }
}
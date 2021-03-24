using System;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class ContactForSponsorEC_Tests : CslaBaseTest
    {
        [Fact]
        public async Task ContactForSponsorEC_NewContactForSponsorEC()
        {
            var category = await ContactForSponsorEC.NewContactForSponsorEC();

            Assert.NotNull(category);
            Assert.IsType<ContactForSponsorEC>(category);
            Assert.False(category.IsValid);
        }

        [Fact]
        public async Task ContactForSponsorEC_GetContactForSponsorEC()
        {
            var contactForSponsorToLoad = MockDb.ContactForSponsors.Single(cs => cs.Id == 1);
            var contactForSponsor = await ContactForSponsorEC.GetContactForSponsorEC(contactForSponsorToLoad);

            Assert.NotNull(contactForSponsor);
            Assert.IsType<ContactForSponsorEC>(contactForSponsor);
            Assert.Equal(contactForSponsorToLoad.Id, contactForSponsor.Id);
            Assert.Equal(new SmartDate(contactForSponsorToLoad.DateWhenContacted), contactForSponsor.DateWhenContacted);
            Assert.Equal(contactForSponsorToLoad.LastUpdatedBy, contactForSponsor.LastUpdatedBy);
            Assert.Equal(new SmartDate(contactForSponsorToLoad.LastUpdatedDate), contactForSponsor.LastUpdatedDate);
            Assert.Equal(contactForSponsorToLoad.Notes, contactForSponsor.Notes);
            Assert.Equal(contactForSponsorToLoad.RowVersion, contactForSponsor.RowVersion);
            Assert.True(contactForSponsor.IsValid);
        }

        [Fact]
        public async Task ContactForSponsorEC_PurposeLessThan255Chars()
        {
            var contactForSponsorToLoad = MockDb.ContactForSponsors.Single(cs => cs.Id == 1);
            var category = await ContactForSponsorEC.GetContactForSponsorEC(contactForSponsorToLoad);
            var isObjectValidInit = category.IsValid;
            category.Purpose = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                               "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                               "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                               "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(category);
            Assert.True(isObjectValidInit);
            Assert.False(category.IsValid);
            Assert.Equal("Purpose", category.BrokenRulesCollection[0].Property);
        }

        [Fact]
        public async Task ContactForSponsorEC_LastUpdatedByRequired()
        {
            var contactForSponsorToLoad = MockDb.ContactForSponsors.Single(cs => cs.Id == 1);
            var category = await ContactForSponsorEC.GetContactForSponsorEC(contactForSponsorToLoad);
            var isObjectValidInit = category.IsValid;
            category.LastUpdatedBy = string.Empty;

            Assert.NotNull(category);
            Assert.True(isObjectValidInit);
            Assert.False(category.IsValid);
            Assert.Equal("LastUpdatedBy", category.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy required", category.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task ContactForSponsorEC_LastUpdatedDateRequired()
        {
            var contactForSponsorToLoad = MockDb.ContactForSponsors.Single(cs => cs.Id == 1);
            var category = await ContactForSponsorEC.GetContactForSponsorEC(contactForSponsorToLoad);
            var isObjectValidInit = category.IsValid;
            category.LastUpdatedDate = DateTime.MinValue;

            Assert.NotNull(category);
            Assert.True(isObjectValidInit);
            Assert.False(category.IsValid);
            Assert.Equal("LastUpdatedDate", category.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedDate required", category.BrokenRulesCollection[0].Description);
        }
    }
}
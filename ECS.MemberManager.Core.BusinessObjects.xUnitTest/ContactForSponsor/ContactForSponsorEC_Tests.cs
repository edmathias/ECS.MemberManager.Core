using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class ContactForSponsorEC_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public ContactForSponsorEC_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");

            if (testLibrary == "Mock")
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
            var contactForSponsorToLoad = await BuildContactForSponsor();
            var contactForSponsor = await ContactForSponsorEC.GetContactForSponsorEC(contactForSponsorToLoad);

            Assert.NotNull(contactForSponsor);
            Assert.IsType<ContactForSponsorEC>(contactForSponsor);
            Assert.Equal(contactForSponsorToLoad.Id,contactForSponsor.Id);
            Assert.Equal(new SmartDate(contactForSponsorToLoad.DateWhenContacted),contactForSponsor.DateWhenContacted);
            Assert.Equal(contactForSponsorToLoad.LastUpdatedBy, contactForSponsor.LastUpdatedBy);
            Assert.Equal(new SmartDate(contactForSponsorToLoad.LastUpdatedDate), contactForSponsor.LastUpdatedDate);
            Assert.Equal(contactForSponsorToLoad.Notes, contactForSponsor.Notes);
            Assert.Equal(contactForSponsorToLoad.RowVersion, contactForSponsor.RowVersion);
            Assert.True(contactForSponsor.IsValid);
        }

        [Fact]
        public async Task ContactForSponsorEC_PurposeLessThan255Chars()
        {
            var categoryToTest = await BuildContactForSponsor();
            var category = await ContactForSponsorEC.GetContactForSponsorEC(categoryToTest);
            var isObjectValidInit = category.IsValid;
            category.Purpose = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(category);
            Assert.True(isObjectValidInit);
            Assert.False(category.IsValid);
            Assert.Equal("Purpose",category.BrokenRulesCollection[0].Property);
        }

        [Fact]
        public async Task ContactForSponsorEC_LastUpdatedByRequired()
        {
            var categoryToTest = await BuildContactForSponsor();
            var category = await ContactForSponsorEC.GetContactForSponsorEC(categoryToTest);
            var isObjectValidInit = category.IsValid;
            category.LastUpdatedBy = string.Empty;

            Assert.NotNull(category);
            Assert.True(isObjectValidInit);
            Assert.False(category.IsValid);
            Assert.Equal("LastUpdatedBy",category.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy required",category.BrokenRulesCollection[0].Description);
        }
    
                
        
        private async Task<ContactForSponsor> BuildContactForSponsor()
        {
            var contactForSponsor = new ContactForSponsor();
            contactForSponsor.Id = 1;
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

        private async Task<Sponsor> BuildSponsor()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ISponsorDal>();

            var sponsors = await dal.Fetch();

            return sponsors.First();
        }

        private async Task<Person> BuildPerson()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();

            var persons = await dal.Fetch();

            return persons.First();
        }
    }
}
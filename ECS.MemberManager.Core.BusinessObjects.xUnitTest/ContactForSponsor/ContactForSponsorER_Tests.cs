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
using DalManager = ECS.MemberManager.Core.DataAccess.ADO.DalManager;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class ContactForSponsorER_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public ContactForSponsorER_Tests()
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
            
            var result =  await contactForSponsor.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes",result.Notes );
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
            Assert.True( savedContactForSponsor.Id > 0 );
        }

        [Fact]
        public async Task ContactForSponsorER_TestDeleteObjectFromDatabase()
        {
            const int ID_TO_DELETE = 99;
            
            await ContactForSponsorER.DeleteContactForSponsorER(ID_TO_DELETE);

            await Assert.ThrowsAsync<DataPortalException>(() => ContactForSponsorER.GetContactForSponsorER(ID_TO_DELETE));
        }
        

        [Fact]
        public async Task ContactForSponsorER_TestPurposeExceedsMaxLengthOf255()
        {
            var contactForSponsor = await ContactForSponsorER.NewContactForSponsorER();
            contactForSponsor.LastUpdatedBy = "edm";
            contactForSponsor.LastUpdatedDate = DateTime.Now;
            contactForSponsor.Purpose = "valid length";
            Assert.True(contactForSponsor.IsValid);
            
            contactForSponsor.Purpose = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(contactForSponsor);
            Assert.False(contactForSponsor.IsValid);
            Assert.Equal("Purpose",contactForSponsor.BrokenRulesCollection[0].Property);
            Assert.Equal("Purpose can not exceed 255 characters",contactForSponsor.BrokenRulesCollection[0].Description);
        }       

        [Fact]
        public async Task ContactForSponsorEC_LastUpdatedByRequired()
        {
            var contactToTest =  BuildContactForSponsor();
            var contact = await ContactForSponsorEC.GetContactForSponsorEC(contactToTest);
            var isObjectValidInit = contact.IsValid;
            contact.LastUpdatedBy = string.Empty;

            Assert.NotNull(contact);
            Assert.True(isObjectValidInit);
            Assert.False(contact.IsValid);
            Assert.Equal("LastUpdatedBy",contact.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy required",contact.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task ContactForSponsorER_TestLastUpdatedByExceedsMaxLengthOf255()
        {
            var contactForSponsor = await ContactForSponsorER.NewContactForSponsorER();
            contactForSponsor.LastUpdatedBy = "edm";
            contactForSponsor.LastUpdatedDate = DateTime.Now;
            contactForSponsor.Purpose = "valid length";
            var isObjectValidInit = contactForSponsor.IsValid;
            contactForSponsor.LastUpdatedBy = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                              "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                              "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                              "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(contactForSponsor);
            Assert.True(isObjectValidInit);
            Assert.False(contactForSponsor.IsValid);
            Assert.Equal("LastUpdatedBy",contactForSponsor.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy can not exceed 255 characters",contactForSponsor.BrokenRulesCollection[0].Description);
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
            Assert.Equal("LastUpdatedDate",contact.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedDate required",contact.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task ContactForSponsorER_TestInvalidSaveContactForSponsorER()
        {
            var contactForSponsor = await ContactForSponsorER.NewContactForSponsorER();
            contactForSponsor.LastUpdatedBy = String.Empty;
            ContactForSponsorER savedContactForSponsor = null;
                
            Assert.False(contactForSponsor.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedContactForSponsor =  contactForSponsor.Save() );
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
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ISponsorDal>();

            var sponsors = await dal.Fetch();
            var sponsor = sponsors.First();

            return await SponsorEC.GetSponsorEC(sponsor);
        }

        private async Task<PersonEC> BuildPersonEC()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();

            var persons = await dal.Fetch();
            var sponsor = persons.First();

            return await PersonEC.GetPersonEC(sponsor);
        }
        
        
    }
}

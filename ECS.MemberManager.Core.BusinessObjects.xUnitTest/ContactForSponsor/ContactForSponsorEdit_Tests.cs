using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class ContactForSponsorEdit_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public ContactForSponsorEdit_Tests()
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
        public async Task ContactForSponsorEdit_TestGetContactForSponsorEdit()
        {
            const int ID_TO_FETCH = 1;
            
            var contactForSponsor = await ContactForSponsorEdit.GetContactForSponsorEdit(ID_TO_FETCH);

            Assert.NotNull(contactForSponsor);
            Assert.Equal(ID_TO_FETCH,contactForSponsor.Id);
            Assert.True(contactForSponsor.IsValid);
        }

        [Fact]
        public async Task ContactForSponsorEdit_TestNewContactForSponsorEdit()
        {
            var contactForSponsor = await ContactForSponsorEdit.NewContactForSponsorEdit();

            Assert.NotNull(contactForSponsor);
            Assert.False(contactForSponsor.IsValid);
        }

        [Fact]
        public async void ContactForSponsorEdit_TestUpdateContactForSponsorEdit()
        {
            const int ID_TO_FETCH = 1;
            var contactForSponsor = await ContactForSponsorEdit.GetContactForSponsorEdit(ID_TO_FETCH);
            contactForSponsor.Notes = "These are updated Notes";

            var result = await contactForSponsor.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes",result.Notes );
        }

        [Fact]
        public async Task ContactForSponsorEdit_TestInsertContactForSponsorEdit()
        {
            var contactForSponsor = await ContactForSponsorEdit.NewContactForSponsorEdit();
            BuildContactForSponsorEdit(contactForSponsor);

            var savedContactForSponsor = await contactForSponsor.SaveAsync();

            Assert.NotNull(savedContactForSponsor);
            Assert.IsType<ContactForSponsorEdit>(savedContactForSponsor);
            Assert.True(savedContactForSponsor.Id > 0);
        }

        [Fact]
        public async Task ContactForSponsorEdit_TestDeleteContactForSponsorEdit()
        {
            const int ID_TO_DELETE = 99;

            var contactList = await ContactForSponsorInfoList.GetContactForSponsorInfoList();

            await ContactForSponsorEdit.DeleteContactForSponsorEdit(ID_TO_DELETE);

            var contactListAfterDelete = await ContactForSponsorInfoList.GetContactForSponsorInfoList();
            
            Assert.NotEqual(contactList.Count, contactListAfterDelete.Count );
        }

        // test invalid state 
         
        [Fact]
        public async Task ContactForSponsorEdit_TestInvalidSaveContactForSponsorEdit()
        {
            var contactForSponsor = await ContactForSponsorEdit.NewContactForSponsorEdit();
            
            ContactForSponsorEdit savedContactForSponsor = null;

            Assert.False(contactForSponsor.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedContactForSponsor = contactForSponsor.Save());
        }

        void BuildContactForSponsorEdit(ContactForSponsorEdit contactForSponsorER)
        {
            contactForSponsorER.Purpose = "purpose";
            contactForSponsorER.PersonId = 1;
            contactForSponsorER.SponsorId = 1;
            contactForSponsorER.DateWhenContacted = DateTime.Today;
            contactForSponsorER.LastUpdatedDate = DateTime.Now;
            contactForSponsorER.LastUpdatedBy = "edm";
            contactForSponsorER.Notes = "This person is on standby";
        }
    }
}
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        private async void ContactForSponsorECL_TestContactForSponsorECL()
        {
            var contactForSponsorEdit = await ContactForSponsorECL.NewContactForSponsorECL();

            Assert.NotNull(contactForSponsorEdit);
            Assert.IsType<ContactForSponsorECL>(contactForSponsorEdit);
        }

        
        [Fact]
        private async void ContactForSponsorECL_TestGetContactForSponsorECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IContactForSponsorDal>();
            var childData = await dal.Fetch();
            
            var listToTest = await ContactForSponsorECL.GetContactForSponsorECL(childData);
            
            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
        
        [Fact]
        private async void ContactForSponsorECL_TestDeleteContactForSponsorEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IContactForSponsorDal>();
            var childData = await dal.Fetch();
            
            var contactForSponsorEditList = await ContactForSponsorECL.GetContactForSponsorECL(childData);

            var contactForSponsor = contactForSponsorEditList.First(a => a.Id == 99);

            // remove is deferred delete
            contactForSponsorEditList.Remove(contactForSponsor); 

            var contactForSponsorListAfterDelete = await contactForSponsorEditList.SaveAsync();
            
            Assert.NotEqual(childData.Count,contactForSponsorListAfterDelete.Count);
        }

        [Fact]
        private async void ContactForSponsorECL_TestUpdateContactForSponsorEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IContactForSponsorDal>();
            var childData = await dal.Fetch();
            
            var contactForSponsorList = await ContactForSponsorECL.GetContactForSponsorECL(childData);
            var countBeforeUpdate = contactForSponsorList.Count;
            var idToUpdate = contactForSponsorList.Min(a => a.Id);
            var contactForSponsorToUpdate = contactForSponsorList.First(a => a.Id == idToUpdate);

            contactForSponsorToUpdate.Notes = "This was updated";
            await contactForSponsorList.SaveAsync();

            var updatedList = await dal.Fetch();
            var updatedContactForSponsorsList = await ContactForSponsorECL.GetContactForSponsorECL(updatedList);
            
            Assert.Equal("This was updated",updatedContactForSponsorsList.First(a => a.Id == idToUpdate).Notes);
            Assert.Equal(countBeforeUpdate, updatedContactForSponsorsList.Count);
        }

        [Fact]
        private async void ContactForSponsorECL_TestAddContactForSponsorEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IContactForSponsorDal>();
            var childData = await dal.Fetch();
            
            var contactForSponsorList = await ContactForSponsorECL.GetContactForSponsorECL(childData);
            var countBeforeAdd = contactForSponsorList.Count;
            
            var domainObj =await BuildContactForSponsor();
            var contactEC = await ContactForSponsorEC.GetContactForSponsorEC(domainObj);
            contactForSponsorList.Add(contactEC);
            
            var contactForSponsorEditList = await contactForSponsorList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, contactForSponsorEditList.Count);
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

        private async Task<Sponsor> BuildSponsor()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ISponsorDal>();
            var childData = await dal.Fetch();

            return childData.First();
        }

        private async Task<Person> BuildPerson()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();
            var childData = await dal.Fetch();

            return childData.First();
        }
        
    }
}

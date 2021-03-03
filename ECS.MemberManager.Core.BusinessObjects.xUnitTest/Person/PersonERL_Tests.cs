using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PersonERL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PersonERL_Tests()
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
        private async void PersonERL_TestNewPersonERL()
        {
            var personErl = await PersonERL.NewPersonERL();

            Assert.NotNull(personErl);
            Assert.IsType<PersonERL>(personErl);
        }
        
        [Fact]
        private async void PersonERL_TestGetPersonERL()
        {
            var personERL = await PersonERL.GetPersonERL();

            Assert.NotNull(personERL);
            Assert.Equal(3, personERL.Count);
        }
        
        [Fact]
        private async void PersonERL_TestUpdatePersonEditChildEntry()
        {
            const int idToUpdate = 1;
            
            var personERL = await PersonERL.GetPersonERL();
            var countBeforeUpdate = personERL.Count;
            var personToUpdate = personERL.First(a => a.Id == idToUpdate);
            personToUpdate.Notes = "This was updated";

            var updatedList = await personERL.SaveAsync();
            
            Assert.Equal("This was updated",updatedList.First(a => a.Id == idToUpdate).Notes);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void PersonERL_TestAddPersonEditChildEntry()
        {
            var personERL = await PersonERL.GetPersonERL();
            var countBeforeAdd = personERL.Count;
            
            var personToAdd = personERL.AddNew();
            await BuildPersonEC(personToAdd);

            var updatedList = await personERL.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedList.Count);
        }

        private async Task BuildPersonEC(PersonEC personToBuild)
        {
            personToBuild.LastName = "lastname";
            personToBuild.MiddleName = "A";
            personToBuild.FirstName = "Joe";
            personToBuild.DateOfFirstContact = DateTime.Now;
            personToBuild.BirthDate = DateTime.Now;
            personToBuild.LastUpdatedDate = DateTime.Now;
            personToBuild.LastUpdatedBy = "edm";
            personToBuild.Code = "code";
            personToBuild.Notes = "Notes";
            personToBuild.EMail = await EMailEC.GetEMailEC(new EMail() {Id = 1});
            personToBuild.Title = await TitleEC.GetTitleEC(new Title() {Id = 1});
        }
 
    }
}
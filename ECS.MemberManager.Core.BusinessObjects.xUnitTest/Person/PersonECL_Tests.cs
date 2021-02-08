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
    public class PersonECL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PersonECL_Tests()
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
        private async void PersonECL_TestPersonECL()
        {
            var personEdit = await PersonECL.NewPersonECL();

            Assert.NotNull(personEdit);
            Assert.IsType<PersonECL>(personEdit);
        }


        [Fact]
        private async void PersonECL_TestGetPersonECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();
            var childData = await dal.Fetch();

            var listToTest = await PersonECL.GetPersonECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }

        [Fact]
        private async void PersonECL_TestDeletePersonEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();
            var childData = await dal.Fetch();

            var personEditList = await PersonECL.GetPersonECL(childData);

            var person = personEditList.First(a => a.Id == 99);

            // remove is deferred delete
            personEditList.Remove(person);

            var personListAfterDelete = await personEditList.SaveAsync();

            Assert.NotEqual(childData.Count, personListAfterDelete.Count);
        }

        [Fact]
        private async void PersonECL_TestUpdatePersonEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();
            var childData = await dal.Fetch();

            var personList = await PersonECL.GetPersonECL(childData);
            var countBeforeUpdate = personList.Count;
            var idToUpdate = personList.Min(a => a.Id);
            var personToUpdate = personList.First(a => a.Id == idToUpdate);

            personToUpdate.Notes = "This was updated";
            await personList.SaveAsync();

            var updatedList = await dal.Fetch();
            var updatedPersonsList = await PersonECL.GetPersonECL(updatedList);

            Assert.Equal("This was updated", updatedPersonsList.First(a => a.Id == idToUpdate).Notes);
            Assert.Equal(countBeforeUpdate, updatedPersonsList.Count);
        }

        [Fact]
        private async void PersonECL_TestAddPersonEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();
            var childData = await dal.Fetch();

            var personList = await PersonECL.GetPersonECL(childData);
            var countBeforeAdd = personList.Count;

            var personToAdd = personList.AddNew();
            await BuildPersonEC(personToAdd);

            var personEditList = await personList.SaveAsync();

            Assert.NotEqual(countBeforeAdd, personEditList.Count);
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
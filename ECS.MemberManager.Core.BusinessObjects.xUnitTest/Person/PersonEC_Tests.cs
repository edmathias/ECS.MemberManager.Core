using System;
using System.IO;
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
    public class PersonEC_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PersonEC_Tests()
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
        public async Task TestPersonEC_Get()
        {
            var person = await PersonEC.GetPersonEC(BuildPerson());

            Assert.NotNull(person);
            Assert.IsType<PersonEC>(person);
            Assert.Equal(999, person.Id);
            Assert.True(person.IsValid);
        }

        [Fact]
        public async Task TestPersonEC_New()
        {
            var person = await PersonEC.NewPersonEC();

            Assert.NotNull(person);
            Assert.False(person.IsValid);
        }


        [Fact]
        public async Task TestPersonEC_LastNameRequired()
        {
            var person = await PersonEC.NewPersonEC();
            await BuildPersonEC(person);
            var isObjectValidInit = person.IsValid;
            person.LastName = string.Empty;
            
            Assert.NotNull(person);
            Assert.True(isObjectValidInit);
            Assert.False(person.IsValid);
            Assert.Equal("LastName",person.BrokenRulesCollection[0].Property);
        }

        [Fact]
        public async Task TestPersonEC_PersonAddressMaxLengthLessThan50()
        {
            var personType = await PersonEC.NewPersonEC();
            await BuildPersonEC(personType);
            var isObjectValidInit = personType.IsValid;
            personType.LastName =  "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                      "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                      "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                      "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(personType);
            Assert.True(isObjectValidInit);
            Assert.False(personType.IsValid);
            Assert.Equal("LastName",personType.BrokenRulesCollection[0].Property);
        }
       
        [Fact]
        public async Task TestPersonEC_LastUpdatedByRequired()
        {
            var personType = await PersonEC.NewPersonEC();
            await BuildPersonEC(personType);
            var isObjectValidInit = personType.IsValid;
            personType.LastUpdatedBy = string.Empty;

            Assert.NotNull(personType);
            Assert.True(isObjectValidInit);
            Assert.False(personType.IsValid);
            Assert.Equal("LastUpdatedBy",personType.BrokenRulesCollection[0].Property);
        }

        [Fact]
        public async Task TestPersonEC_LastUpdatedByMaxLengthLessThan255()
        {
            var personType = await PersonEC.NewPersonEC();
            await BuildPersonEC(personType);
            var isObjectValidInit = personType.IsValid;
            personType.LastUpdatedBy =  "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                      "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                      "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                      "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(personType);
            Assert.True(isObjectValidInit);
            Assert.False(personType.IsValid);
            Assert.Equal("LastUpdatedBy",personType.BrokenRulesCollection[0].Property);
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

        private Person BuildPerson()
        {
            var personToBuild = new Person();
            personToBuild.Id = 999;
            personToBuild.LastName = "lastname";
            personToBuild.MiddleName = "A";
            personToBuild.FirstName = "Joe";
            personToBuild.DateOfFirstContact = DateTime.Now;
            personToBuild.BirthDate = DateTime.Now;
            personToBuild.LastUpdatedDate = DateTime.Now;
            personToBuild.LastUpdatedBy = "edm";
            personToBuild.Code = "code";
            personToBuild.Notes = "Notes";
            personToBuild.EMail = new EMail() {Id = 1};
            personToBuild.Title = new Title() {Id = 1};

            return personToBuild;
        }
    }
}
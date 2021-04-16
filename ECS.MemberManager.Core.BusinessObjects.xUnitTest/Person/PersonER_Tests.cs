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
    public class PersonER_Tests : CslaBaseTest
    {
        [Fact]
        public async Task TestPersonER_Get()
        {
            var person = await PersonER.GetPersonER(1);

            Assert.NotNull(person);
            Assert.IsType<PersonER>(person);
            Assert.Equal(1, person.Id);
            Assert.True(person.IsValid);
        }

        [Fact]
        public async Task TestPersonER_New()
        {
            var person = await PersonER.NewPersonER();

            Assert.NotNull(person);
            Assert.False(person.IsValid);
        }

        [Fact]
        public async void TestPersonER_Update()
        {
            var person = await PersonER.GetPersonER(1);
            var notesUpdate = $"These are updated Notes {DateTime.Now}";
            person.Notes = notesUpdate;

            var result = await person.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal(notesUpdate, result.Notes);
        }

        [Fact]
        public async void TestPersonER_Insert()
        {
            var person = await PersonER.NewPersonER();
            await BuildPerson(person);

            var savedPerson = await person.SaveAsync();

            Assert.NotNull(savedPerson);
            Assert.IsType<PersonER>(savedPerson);
            Assert.True(savedPerson.Id > 0);
            Assert.NotNull(savedPerson.RowVersion);
        }

        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestPersonER_TestInvalidSave()
        {
            var person = await PersonER.NewPersonER();
            await BuildPerson(person);
            person.LastName = string.Empty;

            Assert.False(person.IsValid);
            await Assert.ThrowsAsync<ValidationException>(() => person.SaveAsync());
        }

        [Fact]
        public async Task PersonER_TestSaveOutOfOrder()
        {
            var person1 = await PersonER.GetPersonER(1);
            var person2 = await PersonER.GetPersonER(1);
            person1.Notes = "set up timestamp issue"; // turn on IsDirty
            person2.Notes = "set up timestamp issue";

            var person2_2 = await person2.SaveAsync();

            Assert.NotEqual(person2_2.RowVersion, person1.RowVersion);
            Assert.Equal("set up timestamp issue", person2_2.Notes);
            await Assert.ThrowsAsync<DataPortalException>(() => person1.SaveAsync());
        }

        [Fact]
        public async Task PersonER_TestSubsequentSaves()
        {
            var person = await PersonER.GetPersonER(1);
            person.Notes = "set up timestamp issue"; // turn on IsDirty

            var person2 = await person.SaveAsync();
            var rowVersion1 = person2.RowVersion;
            person2.Notes = "another timestamp trigger";

            var person3 = await person2.SaveAsync();

            Assert.NotEqual(person2.RowVersion, person3.RowVersion);
        }

        [Fact]
        public async Task TestPersonER_InvalidGet()
        {
            await Assert.ThrowsAsync<DataPortalException>(() => PersonER.GetPersonER(999));
        }

        [Fact]
        public async Task TestPersonER_LastNameRequired()
        {
            var person = await PersonER.NewPersonER();
            await BuildPerson(person);
            var isObjectValidInit = person.IsValid;
            person.LastName = string.Empty;

            Assert.NotNull(person);
            Assert.True(isObjectValidInit);
            Assert.False(person.IsValid);
            Assert.Equal("LastName", person.BrokenRulesCollection[0].OriginProperty);
        }

        [Fact]
        public async Task TestPersonER_PersonAddressMaxLengthLessThan50()
        {
            var personType = await PersonER.NewPersonER();
            await BuildPerson(personType);
            var isObjectValidInit = personType.IsValid;
            personType.LastName = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                  "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                  "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                  "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(personType);
            Assert.True(isObjectValidInit);
            Assert.False(personType.IsValid);
            Assert.Equal("LastName", personType.BrokenRulesCollection[0].OriginProperty);
        }

        [Fact]
        public async Task TestPersonER_LastUpdatedByRequired()
        {
            var personType = await PersonER.NewPersonER();
            await BuildPerson(personType);
            var isObjectValidInit = personType.IsValid;
            personType.LastUpdatedBy = string.Empty;

            Assert.NotNull(personType);
            Assert.True(isObjectValidInit);
            Assert.False(personType.IsValid);
            Assert.Equal("LastUpdatedBy", personType.BrokenRulesCollection[0].OriginProperty);
        }

        [Fact]
        public async Task TestPersonER_LastUpdatedByMaxLengthLessThan255()
        {
            var personType = await PersonER.NewPersonER();
            await BuildPerson(personType);
            var isObjectValidInit = personType.IsValid;
            personType.LastUpdatedBy =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(personType);
            Assert.True(isObjectValidInit);
            Assert.False(personType.IsValid);
            Assert.Equal("LastUpdatedBy", personType.BrokenRulesCollection[0].OriginProperty);
        }

        private async Task BuildPerson(PersonER personToBuild)
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
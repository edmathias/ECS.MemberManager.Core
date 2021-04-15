using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PersonalNoteEC_Tests : CslaBaseTest
    {
        [Fact]
        public async Task TestPersonalNoteEC_NewPersonalNoteEC()
        {
            var personalNote = await PersonalNoteEC.NewPersonalNoteEC();

            Assert.NotNull(personalNote);
            Assert.IsType<PersonalNoteEC>(personalNote);
            Assert.False(personalNote.IsValid);
        }

        [Fact]
        public async Task TestPersonalNoteEC_GetPersonalNoteEC()
        {
            var personalNoteToLoad = BuildPersonalNote();
            var personalNote = await PersonalNoteEC.GetPersonalNoteEC(personalNoteToLoad);

            Assert.NotNull(personalNote);
            Assert.IsType<PersonalNoteEC>(personalNote);
            Assert.Equal(personalNoteToLoad.Id, personalNote.Id);
            Assert.Equal(personalNoteToLoad.LastUpdatedBy, personalNote.LastUpdatedBy);
            Assert.Equal(new SmartDate(personalNoteToLoad.LastUpdatedDate), personalNote.LastUpdatedDate);
            Assert.Equal(personalNoteToLoad.RowVersion, personalNote.RowVersion);
            Assert.True(personalNote.IsValid);
        }


        [Fact]
        public async Task TestPersonalNoteEC_LastUpdatedByRequired()
        {
            var personalNoteToTest = BuildPersonalNote();
            var personalNote = await PersonalNoteEC.GetPersonalNoteEC(personalNoteToTest);
            var isObjectValidInit = personalNote.IsValid;
            personalNote.LastUpdatedBy = string.Empty;

            Assert.NotNull(personalNote);
            Assert.True(isObjectValidInit);
            Assert.False(personalNote.IsValid);
            Assert.Equal("LastUpdatedBy", personalNote.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy required", personalNote.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestPersonalNoteEC_DescriptionLessThan50Chars()
        {
            var personalNoteToTest = BuildPersonalNote();
            var personalNote = await PersonalNoteEC.GetPersonalNoteEC(personalNoteToTest);
            var isObjectValidInit = personalNote.IsValid;
            personalNote.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                  "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                  "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                  "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(personalNote);
            Assert.True(isObjectValidInit);
            Assert.False(personalNote.IsValid);
            Assert.Equal("Description", personalNote.BrokenRulesCollection[0].Property);
            Assert.Equal("Description can not exceed 50 characters", personalNote.BrokenRulesCollection[0].Description);
        }

        private PersonalNote BuildPersonalNote()
        {
            var personalNote = new PersonalNote();
            personalNote.Id = 1;
            personalNote.Person = new Person() {Id = 1};
            personalNote.Description = "personal note description";
            personalNote.StartDate = DateTime.Now;
            personalNote.DateEnd = DateTime.Now.AddMonths(12);
            personalNote.LastUpdatedBy = "Hank";
            personalNote.LastUpdatedDate = DateTime.Now;
            personalNote.Note = "notes for personal note";

            return personalNote;
        }
    }
}
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PersonalNoteERL_Tests : CslaBaseTest
    {
        [Fact]
        private async void PersonalNoteERL_TestNewPersonalNoteERL()
        {
            var personalNoteEdit = await PersonalNoteERL.NewPersonalNoteERL();

            Assert.NotNull(personalNoteEdit);
            Assert.IsType<PersonalNoteERL>(personalNoteEdit);
        }

        [Fact]
        private async void PersonalNoteERL_TestGetPersonalNoteERL()
        {
            var personalNoteEdit =
                await PersonalNoteERL.GetPersonalNoteERL();

            Assert.NotNull(personalNoteEdit);
            Assert.Equal(3, personalNoteEdit.Count);
        }

        [Fact]
        private async void PersonalNoteERL_TestDeletePersonalNoteERL()
        {
            const int ID_TO_DELETE = 99;
            var categoryList =
                await PersonalNoteERL.GetPersonalNoteERL();
            var listCount = categoryList.Count;
            var categoryToDelete = categoryList.First(cl => cl.Id == ID_TO_DELETE);
            // remove is deferred delete
            var isDeleted = categoryList.Remove(categoryToDelete);

            var personalNoteListAfterDelete = await categoryList.SaveAsync();

            Assert.NotNull(personalNoteListAfterDelete);
            Assert.IsType<PersonalNoteERL>(personalNoteListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount, personalNoteListAfterDelete.Count);
        }

        [Fact]
        private async void PersonalNoteERL_TestUpdatePersonalNoteERL()
        {
            const int ID_TO_UPDATE = 1;
            const string NOTES_UPDATE = "Updated Notes";

            var categoryList =
                await PersonalNoteERL.GetPersonalNoteERL();
            var personalNoteToUpdate = categoryList.First(cl => cl.Id == ID_TO_UPDATE);
            personalNoteToUpdate.Note = NOTES_UPDATE;

            var updatedList = await categoryList.SaveAsync();
            var updatedPersonalNote = updatedList.First(el => el.Id == ID_TO_UPDATE);

            Assert.NotNull(updatedList);
            Assert.NotNull(updatedPersonalNote);
            Assert.Equal(NOTES_UPDATE, updatedPersonalNote.Note);
        }

        [Fact]
        private async void PersonalNoteERL_TestAddPersonalNoteERL()
        {
            var categoryList =
                await PersonalNoteERL.GetPersonalNoteERL();
            var countBeforeAdd = categoryList.Count;

            var personalNoteToAdd = categoryList.AddNew();
            await BuildPersonalNote(personalNoteToAdd);

            var updatedCategoryList = await categoryList.SaveAsync();

            Assert.NotEqual(countBeforeAdd, updatedCategoryList.Count);
        }

        private async Task BuildPersonalNote(PersonalNoteEC personalNote)
        {
            personalNote.Person = await PersonEC.GetPersonEC(new Person() {Id = 1});
            personalNote.Description = "personal note description";
            personalNote.StartDate = DateTime.Now;
            personalNote.DateEnd = DateTime.Now.AddMonths(12);
            personalNote.LastUpdatedBy = "Hank";
            personalNote.LastUpdatedDate = DateTime.Now;
            personalNote.Note = "notes for personal note";
        }
    }
}
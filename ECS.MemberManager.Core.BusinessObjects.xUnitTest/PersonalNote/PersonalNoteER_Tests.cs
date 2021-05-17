using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PersonalNoteER_Tests : CslaBaseTest
    {
        [Fact]
        public async Task TestPersonalNoteER_NewPersonalNoteER()
        {
            var personalNote = await PersonalNoteER.NewPersonalNoteER();

            Assert.NotNull(personalNote);
            Assert.IsType<PersonalNoteER>(personalNote);
            Assert.False(personalNote.IsValid);
        }

        [Fact]
        public async Task TestPersonalNoteER_GetPersonalNoteER()
        {
            var personalNote = await PersonalNoteER.GetPersonalNoteER(1);

            Assert.NotNull(personalNote);
            Assert.IsType<PersonalNoteER>(personalNote);
            Assert.Equal(1, personalNote.Id);
            Assert.True(personalNote.IsValid);
        }


        [Fact]
        public async Task TestPersonalNoteER_LastUpdatedByRequired()
        {
            var personalNote = await PersonalNoteER.GetPersonalNoteER(1);
            var isObjectValidInit = personalNote.IsValid;
            personalNote.LastUpdatedBy = string.Empty;

            Assert.NotNull(personalNote);
            Assert.True(isObjectValidInit);
            Assert.False(personalNote.IsValid);
            Assert.Equal("LastUpdatedBy", personalNote.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy required", personalNote.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestPersonalNoteER_DescriptionGreaterThan50Chars()
        {
            var personalNote = await PersonalNoteER.GetPersonalNoteER(1);
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
    }
}
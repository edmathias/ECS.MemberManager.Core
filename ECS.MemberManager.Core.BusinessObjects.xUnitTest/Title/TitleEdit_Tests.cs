using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TitleEdit_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public TitleEdit_Tests()
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
        public async Task TitleEdit_Get()
        {
            var title = await TitleEdit.GetTitleEdit(1);

            Assert.NotNull(title);
            Assert.IsType<TitleEdit>(title);
            Assert.Equal(1, title.Id);
            Assert.True(title.IsValid);
        }

        [Fact]
        public async Task TitleEdit_New()
        {
            var title = await TitleEdit.NewTitleEdit();

            Assert.NotNull(title);
            Assert.False(title.IsValid);
        }

        [Fact]
        public async void TitleEdit_Update()
        {
            var title = await TitleEdit.GetTitleEdit(1);
            var notesUpdate = $"These are updated description.";
            title.Description = notesUpdate;

            var result = await title.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal(notesUpdate, result.Description);
        }

        [Fact]
        public async void TitleEdit_Insert()
        {
            var title = await TitleEdit.NewTitleEdit();
            title.Abbreviation = "Lord";
            title.Description = "This person is inserted";

            var savedTitle = await title.SaveAsync();

            Assert.NotNull(savedTitle);
            Assert.IsType<TitleEdit>(savedTitle);
            Assert.True(savedTitle.Id > 0);
            Assert.NotNull(savedTitle.RowVersion);
        }

        [Fact]
        public async Task TitleEdit_Delete()
        {
            await TitleEdit.DeleteTitleEdit(99);

            var emailTypeToCheck = await Assert.ThrowsAsync<Csla.DataPortalException>
                (() => TitleEdit.GetTitleEdit(99));
        }

        [Fact]
        public async Task TitleEdit_AbbreviationRequired()
        {
            var title = await TitleEdit.NewTitleEdit();
            title.Abbreviation = "Mrs";
            var isObjectValidInit = title.IsValid;
            title.Abbreviation = string.Empty;

            Assert.NotNull(title);
            Assert.True(isObjectValidInit);
            Assert.False(title.IsValid);
        }

        [Fact]
        public async Task TitleEdit_DescriptionExceedsMaxLengthOf50()
        {
            var title = await TitleEdit.NewTitleEdit();
            title.Abbreviation = "Mrs";
            title.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                    "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                    "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                    "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(title);
            Assert.False(title.IsValid);
            Assert.Equal("The field Description must be a string or array type with a maximum length of '50'.",
                title.BrokenRulesCollection[0].Description);
        }
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TitleEdit_TestInvalidSave()
        {
            var title = await TitleEdit.NewTitleEdit();
            title.Description = String.Empty;

            Assert.False(title.IsValid);
            await Assert.ThrowsAsync<ValidationException>(() => title.SaveAsync());
        }
    
        [Fact]
        public async Task TitleEdit_TestSaveOutOfOrder()
        {
            var emailType1 = await TitleEdit.GetTitleEdit(1);
            var emailType2 = await TitleEdit.GetTitleEdit(1);
            emailType1.Description = "set up timestamp issue";  // turn on IsDirty
            emailType2.Description = "set up timestamp issue";

            var emailType2_2 = await emailType2.SaveAsync();
            
            Assert.NotEqual(emailType2_2.RowVersion, emailType1.RowVersion);
            Assert.Equal("set up timestamp issue",emailType2_2.Description);
            await Assert.ThrowsAsync<DataPortalException>(() => emailType1.SaveAsync());
        }

        [Fact]
        public async Task TitleEdit_TestSubsequentSaves()
        {
            var emailType = await TitleEdit.GetTitleEdit(1);
            emailType.Description = "set up timestamp issue";  // turn on IsDirty

            var emailType2 = await emailType.SaveAsync();
            var rowVersion1 = emailType2.RowVersion;
            emailType2.Description = "another timestamp trigger";

            var emailType3 = await emailType2.SaveAsync();
            
            Assert.NotEqual(emailType2.RowVersion, emailType3.RowVersion);
        }
        
        [Fact]
        public async Task TitleEdit_InvalidGet()
        {
            await Assert.ThrowsAsync<DataPortalException>(() => TitleEdit.GetTitleEdit(999));
        }
    }
}
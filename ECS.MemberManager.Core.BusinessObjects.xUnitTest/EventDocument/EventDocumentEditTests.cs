using System;
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
    public class EventDocumentEditTests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EventDocumentEditTests()
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
        public async Task TestEventDocumentEdit_TestGetEventDocumentEdit()
        {
            var eventDocument = await EventDocumentEdit.GetEventDocumentEdit(1);

            Assert.NotNull(eventDocument);
            Assert.IsType<EventDocumentEdit>(eventDocument);
            Assert.Equal(1, eventDocument.Id);
            Assert.True(eventDocument.IsValid);
        }

        [Fact]
        public async Task TestEventDocumentEdit_New()
        {
            var eventDocument = await EventDocumentEdit.NewEventDocumentEdit();

            Assert.NotNull(eventDocument);
            Assert.False(eventDocument.IsValid);
        }

        [Fact]
        public async void TestEventDocumentEdit_Update()
        {
            var eventDocument = await EventDocumentEdit.GetEventDocumentEdit(1);
            var notesUpdate = $"These are updated Notes {DateTime.Now}";
            eventDocument.Notes = notesUpdate;

            var result = await eventDocument.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal(notesUpdate, result.Notes);
        }

        [Fact]
        public async void TestEventDocumentEdit_Insert()
        {
            var eventDocument = await EventDocumentEdit.NewEventDocumentEdit();
            BuildValidEventDocument(eventDocument);
            eventDocument.Notes = "This person is inserted";

            var savedEventDocument = await eventDocument.SaveAsync();

            Assert.NotNull(savedEventDocument);
            Assert.IsType<EventDocumentEdit>(savedEventDocument);
            Assert.True(savedEventDocument.Id > 0);
            Assert.NotNull(savedEventDocument.RowVersion);
        }

        [Fact]
        public async Task TestEventDocumentEdit_Delete()
        {
            await EventDocumentEdit.DeleteEventDocumentEdit(99);

            var emailTypeToCheck = await Assert.ThrowsAsync<Csla.DataPortalException>
                (() => EventDocumentEdit.GetEventDocumentEdit(99));
        }

        [Fact]
        public async Task TestEventDocumentNameRequired()
        {
            var eventDocument = await EventDocumentEdit.NewEventDocumentEdit();
            BuildValidEventDocument(eventDocument);
            var isObjectValidInit = eventDocument.IsValid;
            
            var newEventDocument = await EventDocumentEdit.NewEventDocumentEdit();
            newEventDocument.DocumentName = string.Empty;

            Assert.NotNull(newEventDocument);
            Assert.True(isObjectValidInit);
            Assert.False(newEventDocument.IsValid);
        }

        [Fact]
        public async Task TestEventDocumentEdit_DocumentNameExceedsMaxLengthOf50()
        {
            var eventDocument = await EventDocumentEdit.NewEventDocumentEdit();
            BuildValidEventDocument(eventDocument);
            eventDocument.DocumentName = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                    "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                    "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                    "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(eventDocument);
            Assert.False(eventDocument.IsValid);
            Assert.Equal("The field DocumentName must be a string or array type with a maximum length of '50'.",
                eventDocument.BrokenRulesCollection[0].Description);
        }
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestEventDocumentEdit_TestInvalidSave()
        {
            var eventDocument = await EventDocumentEdit.NewEventDocumentEdit();
            eventDocument.DocumentName = String.Empty;

            Assert.False(eventDocument.IsValid);
            await Assert.ThrowsAsync<ValidationException>(() => eventDocument.SaveAsync());
        }
    
        [Fact]
        public async Task EventDocumentEdit_TestSaveOutOfOrder()
        {
            var emailType1 = await EventDocumentEdit.GetEventDocumentEdit(1);
            var emailType2 = await EventDocumentEdit.GetEventDocumentEdit(1);
            emailType1.Notes = "set up timestamp issue";  // turn on IsDirty
            emailType2.Notes = "set up timestamp issue";

            var emailType2_2 = await emailType2.SaveAsync();
            
            Assert.NotEqual(emailType2_2.RowVersion, emailType1.RowVersion);
            Assert.Equal("set up timestamp issue",emailType2_2.Notes);
            await Assert.ThrowsAsync<DataPortalException>(() => emailType1.SaveAsync());
        }

        [Fact]
        public async Task EventDocumentEdit_TestSubsequentSaves()
        {
            var emailType = await EventDocumentEdit.GetEventDocumentEdit(1);
            emailType.Notes = "set up timestamp issue";  // turn on IsDirty

            var emailType2 = await emailType.SaveAsync();
            var rowVersion1 = emailType2.RowVersion;
            emailType2.Notes = "another timestamp trigger";

            var emailType3 = await emailType2.SaveAsync();
            
            Assert.NotEqual(emailType2.RowVersion, emailType3.RowVersion);
        }
        
        [Fact]
        public async Task TestEventDocumentEdit_InvalidGet()
        {
            await Assert.ThrowsAsync<DataPortalException>(() => EventDocumentEdit.GetEventDocumentEdit(999));
        }

        private void BuildValidEventDocument(EventDocumentEdit eventDocument)
        {
            eventDocument.Notes = "notes 1";
            eventDocument.DocumentName = "name of document";
            eventDocument.EventId = 1;
            eventDocument.DocumentTypeId = 1;
            eventDocument.PathAndFileName = "path and file name";
            eventDocument.LastUpdatedBy = "edm";
            eventDocument.LastUpdatedDate = DateTime.Now;
        }
    }
}
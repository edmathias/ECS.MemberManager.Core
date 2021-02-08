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
    public class EventDocumentER_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EventDocumentER_Tests()
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
        public async Task TestEventDocumentER_TestGetEventDocumentER()
        {
            var eventDocument = await EventDocumentER.GetEventDocumentER(1);

            Assert.NotNull(eventDocument);
            Assert.IsType<EventDocumentER>(eventDocument);
            Assert.Equal(1, eventDocument.Id);
            Assert.True(eventDocument.IsValid);
        }

        [Fact]
        public async Task TestEventDocumentER_New()
        {
            var eventDocument = await EventDocumentER.NewEventDocumentER();

            Assert.NotNull(eventDocument);
            Assert.False(eventDocument.IsValid);
        }

        [Fact]
        public async void TestEventDocumentER_Update()
        {
            var eventDocument = await EventDocumentER.GetEventDocumentER(1);
            var notesUpdate = $"These are updated Notes {DateTime.Now}";
            eventDocument.Notes = notesUpdate;

            var result = await eventDocument.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal(notesUpdate, result.Notes);
        }

        [Fact]
        public async void TestEventDocumentER_Insert()
        {
            var eventDocument = await EventDocumentER.NewEventDocumentER();
            BuildValidEventDocument(eventDocument);
            eventDocument.Notes = "This person is inserted";

            var savedEventDocument = await eventDocument.SaveAsync();

            Assert.NotNull(savedEventDocument);
            Assert.IsType<EventDocumentER>(savedEventDocument);
            Assert.True(savedEventDocument.Id > 0);
            Assert.NotNull(savedEventDocument.RowVersion);
        }

        [Fact]
        public async Task TestEventDocumentER_Delete()
        {
            await EventDocumentER.DeleteEventDocumentER(99);

            var emailTypeToCheck = await Assert.ThrowsAsync<Csla.DataPortalException>
                (() => EventDocumentER.GetEventDocumentER(99));
        }

        [Fact]
        public async Task TestEventDocumentNameRequired()
        {
            var eventDocument = await EventDocumentER.NewEventDocumentER();
            BuildValidEventDocument(eventDocument);
            var isObjectValidInit = eventDocument.IsValid;
            
            var newEventDocument = await EventDocumentER.NewEventDocumentER();
            newEventDocument.DocumentName = string.Empty;

            Assert.NotNull(newEventDocument);
            Assert.True(isObjectValidInit);
            Assert.False(newEventDocument.IsValid);
        }

        [Fact]
        public async Task TestEventDocumentER_DocumentNameExceedsMaxLengthOf50()
        {
            var eventDocument = await EventDocumentER.NewEventDocumentER();
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
        public async Task TestEventDocumentER_TestInvalidSave()
        {
            var eventDocument = await EventDocumentER.NewEventDocumentER();
            eventDocument.DocumentName = String.Empty;

            Assert.False(eventDocument.IsValid);
            await Assert.ThrowsAsync<ValidationException>(() => eventDocument.SaveAsync());
        }
    
        [Fact]
        public async Task EventDocumentER_TestSaveOutOfOrder()
        {
            var emailType1 = await EventDocumentER.GetEventDocumentER(1);
            var emailType2 = await EventDocumentER.GetEventDocumentER(1);
            emailType1.Notes = "set up timestamp issue";  // turn on IsDirty
            emailType2.Notes = "set up timestamp issue";

            var emailType2_2 = await emailType2.SaveAsync();
            
            Assert.NotEqual(emailType2_2.RowVersion, emailType1.RowVersion);
            Assert.Equal("set up timestamp issue",emailType2_2.Notes);
            await Assert.ThrowsAsync<DataPortalException>(() => emailType1.SaveAsync());
        }

        [Fact]
        public async Task EventDocumentER_TestSubsequentSaves()
        {
            var emailType = await EventDocumentER.GetEventDocumentER(1);
            emailType.Notes = "set up timestamp issue";  // turn on IsDirty

            var emailType2 = await emailType.SaveAsync();
            var rowVersion1 = emailType2.RowVersion;
            emailType2.Notes = "another timestamp trigger";

            var emailType3 = await emailType2.SaveAsync();
            
            Assert.NotEqual(emailType2.RowVersion, emailType3.RowVersion);
        }
        
        [Fact]
        public async Task TestEventDocumentER_InvalidGet()
        {
            await Assert.ThrowsAsync<DataPortalException>(() => EventDocumentER.GetEventDocumentER(999));
        }

        private void BuildValidEventDocument(EventDocumentER eventDocument)
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
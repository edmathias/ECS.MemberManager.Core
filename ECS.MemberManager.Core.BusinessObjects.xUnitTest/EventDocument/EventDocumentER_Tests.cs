﻿using System;
using System.IO;
using System.Linq;
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
        public async Task EventDocumentER_TestGetEventDocument()
        {
            var eventDocumentObj = await EventDocumentER.GetEventDocumentER(1);

            Assert.NotNull(eventDocumentObj);
            Assert.IsType<EventDocumentER>(eventDocumentObj);
        }

        [Fact]
        public async Task EventDocumentER_TestGetNewEventDocumentER()
        {
            var eventDocumentObj = await EventDocumentER.NewEventDocumentER();

            Assert.NotNull(eventDocumentObj);
            Assert.False(eventDocumentObj.IsValid);
        }

        [Fact]
        public async Task EventDocumentER_TestUpdateExistingEventDocumentER()
        {
            var eventDocumentObj = await EventDocumentER.GetEventDocumentER(1);
            eventDocumentObj.Notes = "These are updated Notes";
            
            var result =  await eventDocumentObj.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes",result.Notes );
        }

        [Fact]
        public async Task EventDocumentER_TestInsertNewEventDocumentER()
        {
            var eventDocumentObj = await EventDocumentER.NewEventDocumentER();
            eventDocumentObj.DocumentName = "eventDocument name";
            eventDocumentObj.PathAndFileName = "c:\\pathandfilename";
            eventDocumentObj.LastUpdatedBy = "edm";
            eventDocumentObj.LastUpdatedDate = DateTime.Now;
            eventDocumentObj.Event = await EventEC.GetEventEC(new Event() { Id = 1 });
            eventDocumentObj.DocumentType = await DocumentTypeEC.GetDocumentTypeEC(new DocumentType() { Id = 1});
            var savedEventDocument = await eventDocumentObj.SaveAsync();
           
            Assert.NotNull(savedEventDocument);
            Assert.IsType<EventDocumentER>(savedEventDocument);
            Assert.True( savedEventDocument.Id > 0 );
        }

        [Fact]
        public async Task EventDocumentER_TestDeleteObjectFromDatabase()
        {
            const int ID_TO_DELETE = 99;
            
            await EventDocumentER.DeleteEventDocumentER(ID_TO_DELETE);

            await Assert.ThrowsAsync<DataPortalException>(() => EventDocumentER.GetEventDocumentER(ID_TO_DELETE));
        }
        
        // test invalid state 
        [Fact]
        public async Task EventDocumentER_TestDescriptionRequired()
        {
            var eventDocumentObj = await EventDocumentER.NewEventDocumentER();
            eventDocumentObj.DocumentName = "eventDocument name";
            eventDocumentObj.PathAndFileName = "c:\\pathandfilename";
            eventDocumentObj.LastUpdatedBy = "edm";
            eventDocumentObj.LastUpdatedDate = DateTime.Now;
            var isObjectValidInit = eventDocumentObj.IsValid;
            eventDocumentObj.DocumentName = string.Empty;

            Assert.NotNull(eventDocumentObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventDocumentObj.IsValid);
        }
       
        [Fact]
        public async Task EventDocumentER_TestDocumentNameExceedsMaxLengthOf255()
        {
            var eventDocumentObj = await EventDocumentER.NewEventDocumentER();
            eventDocumentObj.DocumentName = "eventDocument name";
            eventDocumentObj.PathAndFileName = "c:\\pathandfilename";
            eventDocumentObj.LastUpdatedBy = "edm";
            eventDocumentObj.LastUpdatedDate = DateTime.Now;
            var isObjectValid = eventDocumentObj.IsValid;
            
            eventDocumentObj.DocumentName = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                     "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                     "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                     "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(eventDocumentObj);
            Assert.True(isObjectValid);
            Assert.False(eventDocumentObj.IsValid);
            Assert.Equal("The field DocumentName must be a string or array type with a maximum length of '50'.",
                eventDocumentObj.BrokenRulesCollection[0].Description);
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task EventDocumentER_TestInvalidSaveEventDocumentER()
        {
            var eventDocumentObj = await EventDocumentER.NewEventDocumentER();
            eventDocumentObj.DocumentName = String.Empty;
            EventDocumentER savedEventDocument = null;
                
            Assert.False(eventDocumentObj.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedEventDocument =  eventDocumentObj.Save() );
        }
        
        private EventDocument BuildEventDocument()
        {
            var eventDocumentObj = new EventDocument();
            eventDocumentObj.Id = 1;
            eventDocumentObj.Event = new Event() {Id = 1};
            eventDocumentObj.DocumentType = new DocumentType() {Id = 1};
            eventDocumentObj.DocumentName = "test doc name";
            eventDocumentObj.PathAndFileName = "c:\\pathandfilename";
            eventDocumentObj.LastUpdatedBy = "edm";
            eventDocumentObj.LastUpdatedDate = DateTime.Now;
            eventDocumentObj.Notes = "notes for doctype";

            return eventDocumentObj;
        }        

    }
}

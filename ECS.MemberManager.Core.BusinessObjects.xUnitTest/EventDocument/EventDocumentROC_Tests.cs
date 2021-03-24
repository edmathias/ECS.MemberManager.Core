using System;
using System.IO;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventDocumentROC_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EventDocumentROC_Tests()
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
        public async Task TestEventDocumentROC_GetEventDocumentROC()
        {
            var eventDocumentObjToLoad = BuildEventDocument();
            var eventDocumentObj = await EventDocumentROC.GetEventDocumentROC(eventDocumentObjToLoad);

            Assert.NotNull(eventDocumentObj);
            Assert.IsType<EventDocumentROC>(eventDocumentObj);
            Assert.Equal(eventDocumentObjToLoad.Id, eventDocumentObj.Id);
            Assert.Equal(eventDocumentObjToLoad.Event.Id, eventDocumentObj.Event.Id);
            Assert.Equal(eventDocumentObjToLoad.DocumentType.Id, eventDocumentObj.DocumentType.Id);
            Assert.Equal(eventDocumentObjToLoad.DocumentName, eventDocumentObj.DocumentName);
            Assert.Equal(eventDocumentObjToLoad.PathAndFileName, eventDocumentObj.PathAndFileName);
            Assert.Equal(eventDocumentObjToLoad.LastUpdatedBy, eventDocumentObj.LastUpdatedBy);
            Assert.Equal(new SmartDate(eventDocumentObjToLoad.LastUpdatedDate), eventDocumentObj.LastUpdatedDate);
            Assert.Equal(eventDocumentObjToLoad.Notes, eventDocumentObj.Notes);
            Assert.Equal(eventDocumentObjToLoad.RowVersion, eventDocumentObj.RowVersion);
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
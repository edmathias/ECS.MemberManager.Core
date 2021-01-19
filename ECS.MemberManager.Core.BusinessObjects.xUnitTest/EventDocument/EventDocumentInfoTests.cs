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
    public class EventDocumentInfoTests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EventDocumentInfoTests()
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
        public async Task TestEventDocumentInfo_TestGetEventDocumentInfo()
        {
            var eventDocument = await EventDocumentInfo.GetEventDocumentInfo(1);

            Assert.NotNull(eventDocument);
            Assert.IsType<EventDocumentInfo>(eventDocument);
            Assert.Equal(1, eventDocument.Id);
        }

        private void BuildValidEventDocument(EventDocumentInfo eventDocument)
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
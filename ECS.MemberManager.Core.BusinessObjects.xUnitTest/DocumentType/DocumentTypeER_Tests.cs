using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class DocumentTypeER_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public DocumentTypeER_Tests()
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
        public async Task DocumentTypeER_TestGetDocumentType()
        {
            var documentType = await DocumentTypeER.GetDocumentTypeER(1);

            Assert.NotNull(documentType);
            Assert.IsType<DocumentTypeER>(documentType);
        }

        [Fact]
        public async Task DocumentTypeER_TestGetNewDocumentTypeER()
        {
            var documentType = await DocumentTypeER.NewDocumentTypeER();

            Assert.NotNull(documentType);
            Assert.False(documentType.IsValid);
        }

        [Fact]
        public async Task DocumentTypeER_TestUpdateExistingDocumentTypeER()
        {
            var documentType = await DocumentTypeER.GetDocumentTypeER(1);
            documentType.Notes = "These are updated Notes";
            
            var result =  await documentType.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes",result.Notes );
        }

        [Fact]
        public async Task DocumentTypeER_TestInsertNewDocumentTypeER()
        {
            var documentType = await DocumentTypeER.NewDocumentTypeER();
            documentType.Description = "Standby";
            documentType.Notes = "This person is on standby";
            documentType.LastUpdatedBy = "edm";
            documentType.LastUpdatedDate = DateTime.Now;

            var savedDocumentType = await documentType.SaveAsync();
           
            Assert.NotNull(savedDocumentType);
            Assert.IsType<DocumentTypeER>(savedDocumentType);
            Assert.True( savedDocumentType.Id > 0 );
        }

        [Fact]
        public async Task DocumentTypeER_TestDeleteObjectFromDatabase()
        {
            const int ID_TO_DELETE = 99;
            
            await DocumentTypeER.DeleteDocumentTypeER(ID_TO_DELETE);

            await Assert.ThrowsAsync<DataPortalException>(() => DocumentTypeER.GetDocumentTypeER(ID_TO_DELETE));
        }
        
        // test invalid state 
        [Fact]
        public async Task DocumentTypeER_TestDescriptionRequired() 
        {
            var documentType = await DocumentTypeER.NewDocumentTypeER();
            documentType.Description = "make valid";
            documentType.LastUpdatedBy = "edm";
            documentType.LastUpdatedDate = DateTime.Now;
            var isObjectValidInit = documentType.IsValid;
            documentType.Description = string.Empty;

            Assert.NotNull(documentType);
            Assert.True(isObjectValidInit);
            Assert.False(documentType.IsValid);
        }
       
        [Fact]
        public async Task DocumentTypeER_TestDescriptionExceedsMaxLengthOf50()
        {
            var documentType = await DocumentTypeER.NewDocumentTypeER();
            documentType.LastUpdatedBy = "edm";
            documentType.LastUpdatedDate = DateTime.Now;
            documentType.Description = "valid length";
            Assert.True(documentType.IsValid);
            
            documentType.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(documentType);
            Assert.False(documentType.IsValid);
            Assert.Equal("Description",documentType.BrokenRulesCollection[0].Property);
            Assert.Equal("Description can not exceed 50 characters",documentType.BrokenRulesCollection[0].Description);
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestDocumentTypeEC_LastUpdatedByRequired()
        {
            var documentType = await DocumentTypeER.NewDocumentTypeER();
            documentType.Description = "test description";
            documentType.LastUpdatedBy = "edm";
            documentType.LastUpdatedDate = DateTime.Now;
            var isObjectValidInit = documentType.IsValid;
            documentType.LastUpdatedBy = string.Empty;

            Assert.NotNull(documentType);
            Assert.True(isObjectValidInit);
            Assert.False(documentType.IsValid);
            Assert.Equal("LastUpdatedBy",documentType.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy required",documentType.BrokenRulesCollection[0].Description);
        }
 
        [Fact]
        public async Task TestDocumentTypeER_LastUpdatedByLessThan255Chars()
        {
            var documentType = await DocumentTypeER.NewDocumentTypeER();
            documentType.Description = "test description";
            documentType.LastUpdatedBy = "edm";
            documentType.LastUpdatedDate = DateTime.Now;
            var isObjectValidInit = documentType.IsValid;
            documentType.LastUpdatedBy = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                         "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                         "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                         "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(documentType);
            Assert.True(isObjectValidInit);
            Assert.False(documentType.IsValid);
            Assert.Equal("LastUpdatedBy",documentType.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy can not exceed 255 characters",documentType.BrokenRulesCollection[0].Description);
        }
        
        [Fact]
        public async Task DocumentTypeER_TestInvalidSaveDocumentTypeER()
        {
            var documentType = await DocumentTypeER.NewDocumentTypeER();
            documentType.Description = String.Empty;
            DocumentTypeER savedDocumentType = null;
                
            Assert.False(documentType.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedDocumentType =  documentType.Save() );
        }
        
        
    }
}

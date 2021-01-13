using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class DocumentTypeEdit_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public DocumentTypeEdit_Tests()
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
        public async Task DocumentTypeEdit_TestGetDocumentType()
        {
            var documentType = await DocumentTypeEdit.GetDocumentTypeEdit(1);

            Assert.NotNull(documentType);
            Assert.IsType<DocumentTypeEdit>(documentType);
        }

        [Fact]
        public async Task DocumentTypeEdit_TestGetNewDocumentTypeEdit()
        {
            var documentType = await DocumentTypeEdit.NewDocumentTypeEdit();

            Assert.NotNull(documentType);
            Assert.False(documentType.IsValid);
        }

        [Fact]
        public async Task DocumentTypeEdit_TestUpdateExistingDocumentTypeEdit()
        {
            var documentType = await DocumentTypeEdit.GetDocumentTypeEdit(1);
            documentType.Notes = "These are updated Notes";
            
            var result =  await documentType.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes",result.Notes );
        }

        [Fact]
        public async Task DocumentTypeEdit_TestInsertNewDocumentTypeEdit()
        {
            var documentType = await DocumentTypeEdit.NewDocumentTypeEdit();
            documentType.Description = "Standby";
            documentType.Notes = "This person is on standby";
            documentType.LastUpdatedBy = "edm";
            documentType.LastUpdatedDate = DateTime.Now;

            var savedDocumentType = await documentType.SaveAsync();
           
            Assert.NotNull(savedDocumentType);
            Assert.IsType<DocumentTypeEdit>(savedDocumentType);
            Assert.True( savedDocumentType.Id > 0 );
        }

        [Fact]
        public async Task DocumentTypeEdit_TestDeleteObjectFromDatabase()
        {
            var deleteId = MockDb.DocumentTypes.Max(dt => dt.Id);
            int beforeCount = MockDb.DocumentTypes.Count();

            await DocumentTypeEdit.DeleteDocumentTypeEdit(deleteId);

            var listAfterDelete = await DocumentTypeEditList.GetDocumentTypeEditList();
            
            Assert.NotEqual(beforeCount,listAfterDelete.Count());
        }
        
        // test invalid state 
        [Fact]
        public async Task DocumentTypeEdit_TestDescriptionRequired() 
        {
            var documentType = await DocumentTypeEdit.NewDocumentTypeEdit();
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
        public async Task DocumentTypeEdit_TestDescriptionExceedsMaxLengthOf50()
        {
            var documentType = await DocumentTypeEdit.NewDocumentTypeEdit();
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
            Assert.Equal("The field Description must be a string or array type with a maximum length of '50'.",
                documentType.BrokenRulesCollection[0].Description);
 
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task DocumentTypeEdit_TestInvalidSaveDocumentTypeEdit()
        {
            var documentType = await DocumentTypeEdit.NewDocumentTypeEdit();
            documentType.Description = String.Empty;
            DocumentTypeEdit savedDocumentType = null;
                
            Assert.False(documentType.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedDocumentType =  documentType.Save() );
        }
        
        
    }
}

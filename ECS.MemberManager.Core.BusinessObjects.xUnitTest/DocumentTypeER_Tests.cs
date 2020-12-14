using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    /// <summary>
    /// Summary description for JustMockTest
    /// </summary>
    public class DocumentTypeER_Tests 
    {

        [Fact]
        public async Task TestDocumentTypeER_Get()
        {
            var documentType = await DocumentTypeER.GetDocumentType(1);

            Assert.Equal(1, documentType.Id);
            Assert.True(documentType.IsValid);
        }

        [Fact]
        public async Task TestDocumentTypeER_GetNewObject()
        {
            var documentType = await DocumentTypeER.NewDocumentType();

            Assert.NotNull(documentType);
            Assert.False(documentType.IsValid);
        }

        [Fact]
        public async Task TestDocumentTypeER_UpdateExistingObjectInDatabase()
        {
            var documentType = await DocumentTypeER.GetDocumentType(1);
            documentType.Notes = "These are updated Notes";
            
            var result = documentType.Save();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes",result.Notes );
        }

        [Fact]
        public async Task TestDocumentTypeER_InsertNewObjectIntoDatabase()
        {
            var documentType = await DocumentTypeER.NewDocumentType();
            documentType.Description = "Standby";
            documentType.Notes = "This person is on standby";
            documentType.LastUpdatedBy = "edm";
            documentType.LastUpdatedDate = DateTime.Now;

            var savedDocumentType = documentType.Save();
           
            Assert.NotNull(savedDocumentType);
            Assert.IsType<DocumentTypeER>(savedDocumentType);
            Assert.True( savedDocumentType.Id > 0 );
        }

        [Fact]
        public async Task TestDocumentTypeER_DeleteObjectFromDatabase()
        {
            int beforeCount = MockDb.DocumentTypes.Count();

            await DocumentTypeER.DeleteDocumentType(99);
            
            Assert.NotEqual(beforeCount,MockDb.DocumentTypes.Count());
        }
        
        // test invalid state 
        [Fact]
        public async Task TestDocumentTypeER_DescriptionRequired() 
        {
            var documentType = await DocumentTypeER.NewDocumentType();
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
        public async Task TestDocumentTypeER_DescriptionExceedsMaxLengthOf50()
        {
            var documentType = await DocumentTypeER.NewDocumentType();
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
        public async Task TestDocumentTypeER_TestInvalidSave()
        {
            var documentType = await DocumentTypeER.NewDocumentType();
            documentType.Description = String.Empty;
            DocumentTypeER savedDocumentType = null;
                
            Assert.False(documentType.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedDocumentType =  documentType.Save() );
        }
    }
}

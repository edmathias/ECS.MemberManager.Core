using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class DocumentTypeERL_Tests 
    {
        public DocumentTypeERL_Tests()
        {
            MockDb.ResetMockDb();
        }
        
        [Fact]
        private async void DocumentTypeERL_TestGetDocumentTypeList()
        {
            var listToTest = MockDb.DocumentTypes;
            
            var documentTypeErl = await DocumentTypeERL.GetDocumentTypeList(listToTest);

            Assert.NotNull(documentTypeErl);
            Assert.Equal(MockDb.DocumentTypes.Count, documentTypeErl.Count);
        }
        
        [Fact]
        private async void DocumentTypeERL_TestDeleteDocumentTypesEntry()
        {
            var listToTest = MockDb.DocumentTypes;
            var listCount = listToTest.Count;
            
            var idToDelete = MockDb.DocumentTypes.Max(a => a.Id);
            var documentTypeErl = await DocumentTypeERL.GetDocumentTypeList(listToTest);

            var documentType = documentTypeErl.First(a => a.Id == idToDelete);

            // remove is deferred delete
            documentTypeErl.Remove(documentType); 

            var documentTypeListAfterDelete = await documentTypeErl.SaveAsync();
            
            Assert.NotEqual(listCount,documentTypeListAfterDelete.Count);
        }

        [Fact]
        private async void DocumentTypeERL_TestUpdateDocumentTypesEntry()
        {
            var documentTypeList = await DocumentTypeERL.GetDocumentTypeList(MockDb.DocumentTypes);
            var countBeforeUpdate = documentTypeList.Count;
            var idToUpdate = MockDb.DocumentTypes.Min(a => a.Id);
            var documentTypeToUpdate = documentTypeList.First(a => a.Id == idToUpdate);

            documentTypeToUpdate.Description = "This was updated";
            await documentTypeList.SaveAsync();
            
            var updatedDocumentTypesList = await DocumentTypeERL.GetDocumentTypeList(MockDb.DocumentTypes);
            
            Assert.Equal("This was updated",updatedDocumentTypesList.First(a => a.Id == idToUpdate).Description);
            Assert.Equal(countBeforeUpdate, updatedDocumentTypesList.Count);
        }

        [Fact]
        private async void DocumentTypeERL_TestAddDocumentTypesEntry()
        {
            var documentTypeList = await DocumentTypeERL.GetDocumentTypeList(MockDb.DocumentTypes);
            var countBeforeAdd = documentTypeList.Count;
            
            var documentTypeToAdd = documentTypeList.AddNew();
            BuildDocumentType(documentTypeToAdd); 

            await documentTypeList.SaveAsync();
            
            var updatedDocumentTypesList = await DocumentTypeERL.GetDocumentTypeList(MockDb.DocumentTypes);
            
            Assert.NotEqual(countBeforeAdd, updatedDocumentTypesList.Count);
        }

        private void BuildDocumentType(DocumentTypeEC documentType)
        {
            documentType.Description = "doc type description";
            documentType.Notes = "document type notes";
            documentType.LastUpdatedBy = "edm";
            documentType.LastUpdatedDate = DateTime.Now;
        }
        
    }
}

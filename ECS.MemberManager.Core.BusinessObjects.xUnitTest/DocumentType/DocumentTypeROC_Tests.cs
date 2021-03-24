using System;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class DocumentTypeROC_Tests : CslaBaseTest
    {
        [Fact]
        public async void DocumentTypeROC_TestGetChild()
        {
            const int ID_VALUE = 999;

            var docType = BuildDocumentType();
            docType.Id = ID_VALUE;

            var documentType = await DocumentTypeROC.GetDocumentTypeROC(docType);

            Assert.NotNull(documentType);
            Assert.IsType<DocumentTypeROC>(documentType);
            Assert.Equal(documentType.Id, documentType.Id);
            Assert.Equal(documentType.Description, documentType.Description);
            Assert.Equal(documentType.Notes, documentType.Notes);
            Assert.Equal(documentType.LastUpdatedBy, documentType.LastUpdatedBy);
            Assert.Equal(documentType.LastUpdatedDate, documentType.LastUpdatedDate);
            Assert.Equal(documentType.RowVersion, documentType.RowVersion);
        }

        private DocumentType BuildDocumentType()
        {
            var documentType = new DocumentType();
            documentType.Id = 1;
            documentType.Description = "test description";
            documentType.LastUpdatedBy = "edm";
            documentType.LastUpdatedDate = DateTime.Now;
            documentType.Notes = "notes for doctype";

            return documentType;
        }
    }
}
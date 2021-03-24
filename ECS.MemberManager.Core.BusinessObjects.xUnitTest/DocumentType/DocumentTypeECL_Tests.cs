using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class DocumentTypeECL_Tests : CslaBaseTest
    {
        [Fact]
        private async void DocumentTypeECL_TestDocumentTypeECL()
        {
            var documentTypeEdit = await DocumentTypeECL.NewDocumentTypeECL();

            Assert.NotNull(documentTypeEdit);
            Assert.IsType<DocumentTypeECL>(documentTypeEdit);
        }

        [Fact]
        private async void DocumentTypeECL_TestGetDocumentTypeECL()
        {
            var listToTest = await DocumentTypeECL.GetDocumentTypeECL(GetDocumentTypes());

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }

        private void BuildDocumentType(DocumentTypeEC documentType)
        {
            documentType.Description = "doc type description";
            documentType.Notes = "document type notes";
            documentType.LastUpdatedBy = "edm";
            documentType.LastUpdatedDate = DateTime.Now;
        }

        private static IList<DocumentType> GetDocumentTypes()
        {
            return new List<DocumentType>
            {
                new DocumentType()
                {
                    Id = 1, Description = "Document Type A", Notes = String.Empty,
                    LastUpdatedBy = "edm", LastUpdatedDate = DateTime.Now,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new DocumentType()
                {
                    Id = 2, Description = "Document Type B", Notes = "some notes",
                    LastUpdatedBy = "edm", LastUpdatedDate = DateTime.Now,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new DocumentType()
                {
                    Id = 99, Description = "Document Type to Delete", Notes = String.Empty,
                    LastUpdatedBy = "edm", LastUpdatedDate = DateTime.Now,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
            };
        }
    }
}
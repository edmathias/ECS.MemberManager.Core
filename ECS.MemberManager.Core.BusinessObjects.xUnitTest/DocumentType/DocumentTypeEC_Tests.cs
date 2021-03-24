using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class DocumentTypeEC_Tests : CslaBaseTest
    {
        [Fact]
        public async Task TestDocumentTypeEC_NewDocumentTypeEC()
        {
            var category = await DocumentTypeEC.NewDocumentTypeEC();

            Assert.NotNull(category);
            Assert.IsType<DocumentTypeEC>(category);
            Assert.False(category.IsValid);
        }

        [Fact]
        public async Task TestDocumentTypeEC_GetDocumentTypeEC()
        {
            var documentTypeToLoad = BuildDocumentType();
            var documentType = await DocumentTypeEC.GetDocumentTypeEC(documentTypeToLoad);

            Assert.NotNull(documentType);
            Assert.IsType<DocumentTypeEC>(documentType);
            Assert.Equal(documentTypeToLoad.Id, documentType.Id);
            Assert.Equal(documentTypeToLoad.Description, documentType.Description);
            Assert.Equal(documentTypeToLoad.LastUpdatedBy, documentType.LastUpdatedBy);
            Assert.Equal(new SmartDate(documentTypeToLoad.LastUpdatedDate), documentType.LastUpdatedDate);
            Assert.Equal(documentTypeToLoad.Notes, documentType.Notes);
            Assert.Equal(documentTypeToLoad.RowVersion, documentType.RowVersion);
            Assert.True(documentType.IsValid);
        }

        [Fact]
        public async Task TestDocumentTypeEC_DescriptionRequired()
        {
            var categoryToTest = BuildDocumentType();
            var category = await DocumentTypeEC.GetDocumentTypeEC(categoryToTest);
            var isObjectValidInit = category.IsValid;
            category.Description = string.Empty;

            Assert.NotNull(category);
            Assert.True(isObjectValidInit);
            Assert.False(category.IsValid);
            Assert.Equal("Description", category.BrokenRulesCollection[0].Property);
            Assert.Equal("Description required", category.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestDocumentTypeEC_DescriptionLessThan50Chars()
        {
            var categoryToTest = BuildDocumentType();
            var category = await DocumentTypeEC.GetDocumentTypeEC(categoryToTest);
            var isObjectValidInit = category.IsValid;
            category.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                   "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                   "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                   "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(category);
            Assert.True(isObjectValidInit);
            Assert.False(category.IsValid);
            Assert.Equal("Description", category.BrokenRulesCollection[0].Property);
            Assert.Equal("Description can not exceed 50 characters", category.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestDocumentTypeEC_LastUpdatedByRequired()
        {
            var categoryToTest = BuildDocumentType();
            var category = await DocumentTypeEC.GetDocumentTypeEC(categoryToTest);
            var isObjectValidInit = category.IsValid;
            category.LastUpdatedBy = string.Empty;

            Assert.NotNull(category);
            Assert.True(isObjectValidInit);
            Assert.False(category.IsValid);
            Assert.Equal("LastUpdatedBy", category.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy required", category.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestDocumentTypeEC_LastUpdatedByLessThan255Chars()
        {
            var categoryToTest = BuildDocumentType();
            var category = await DocumentTypeEC.GetDocumentTypeEC(categoryToTest);
            var isObjectValidInit = category.IsValid;
            category.LastUpdatedBy = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                     "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                     "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                     "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(category);
            Assert.True(isObjectValidInit);
            Assert.False(category.IsValid);
            Assert.Equal("LastUpdatedBy", category.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy can not exceed 255 characters", category.BrokenRulesCollection[0].Description);
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
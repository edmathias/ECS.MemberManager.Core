using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class DocumentTypeInfo_Tests
    {

        [Fact]
        public async void DocumentTypeInfo_TestGetById()
        {
            var documentType = await DocumentTypeInfo.GetDocumentTypeInfo(1);
            
            Assert.NotNull(documentType);
            Assert.IsType<DocumentTypeInfo>(documentType);
            Assert.Equal(1, documentType.Id);
        }

        [Fact]
        public async void DocumentTypeInfo_TestGetChild()
        {
            const int ID_VALUE = 999;
            
            var emailType = new DocumentType()
            {
                Id = ID_VALUE,
                Description = "Test email type",
                Notes = "email type notes"
            };

            var emailTypeInfo = await DocumentTypeInfo.GetDocumentTypeInfo(emailType);
            
            Assert.NotNull(emailTypeInfo);
            Assert.IsType<DocumentTypeInfo>(emailTypeInfo);
            Assert.Equal(ID_VALUE, emailTypeInfo.Id);

        }
    }
}
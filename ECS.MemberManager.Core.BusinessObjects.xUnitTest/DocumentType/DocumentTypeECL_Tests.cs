using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class DocumentTypeECL_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public DocumentTypeECL_Tests()
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
        private async void DocumentTypeECL_TestDocumentTypeECL()
        {
            var documentTypeEdit = await DocumentTypeECL.NewDocumentTypeECL();

            Assert.NotNull(documentTypeEdit);
            Assert.IsType<DocumentTypeECL>(documentTypeEdit);
        }

        
        [Fact]
        private async void DocumentTypeECL_TestGetDocumentTypeECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IDocumentTypeDal>();
            var childData = await dal.Fetch();
            
            var listToTest = await DocumentTypeECL.GetDocumentTypeECL(childData);
            
            Assert.NotNull(listToTest);
            Assert.Equal(4, listToTest.Count);
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

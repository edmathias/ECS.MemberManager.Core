using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;

namespace ECS.MemberManager.Core.BusinessObjects.Test
{
    /// <summary>
    /// Summary description for JustMockTest
    /// </summary>
    [TestClass]
    public class DocumentTypeER_Tests 
    {

        [TestMethod]
        public async Task TestDocumentTypeER_Get()
        {
            var documentType = await DocumentTypeER.GetDocumentType(1);

            Assert.AreEqual(documentType.Id, 1);
            Assert.IsTrue(documentType.IsValid);
        }

        [TestMethod]
        public async Task TestDocumentTypeER_GetNewObject()
        {
            var documentType = await DocumentTypeER.NewDocumentType();

            Assert.IsNotNull(documentType);
            Assert.IsFalse(documentType.IsValid);
        }

        [TestMethod]
        public async Task TestDocumentTypeER_UpdateExistingObjectInDatabase()
        {
            var documentType = await DocumentTypeER.GetDocumentType(1);
            documentType.Notes = "These are updated Notes";
            
            var result = documentType.Save();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Notes, "These are updated Notes");
        }

        [TestMethod]
        public async Task TestDocumentTypeER_InsertNewObjectIntoDatabase()
        {
            var documentType = await DocumentTypeER.NewDocumentType();
            documentType.Description = "Standby";
            documentType.Notes = "This person is on standby";
            documentType.LastUpdatedBy = "edm";
            documentType.LastUpdatedDate = DateTime.Now;

            var savedDocumentType = documentType.Save();
           
            Assert.IsNotNull(savedDocumentType);
            Assert.IsInstanceOfType(savedDocumentType, typeof(DocumentTypeER));
            Assert.IsTrue( savedDocumentType.Id > 0 );
        }

        [TestMethod]
        public async Task TestDocumentTypeER_DeleteObjectFromDatabase()
        {
            int beforeCount = MockDb.DocumentTypes.Count();

            await DocumentTypeER.DeleteDocumentType(1);
            
            Assert.AreNotEqual(beforeCount,MockDb.DocumentTypes.Count());
        }
        
        // test invalid state 
        [TestMethod]
        public async Task TestDocumentTypeER_DescriptionRequired() 
        {
            var documentType = await DocumentTypeER.NewDocumentType();
            documentType.Description = "make valid";
            documentType.LastUpdatedBy = "edm";
            documentType.LastUpdatedDate = DateTime.Now;
            var isObjectValidInit = documentType.IsValid;
            documentType.Description = string.Empty;

            Assert.IsNotNull(documentType);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(documentType.IsValid);
        }
       
        [TestMethod]
        public async Task TestDocumentTypeER_DescriptionExceedsMaxLengthOf50()
        {
            var documentType = await DocumentTypeER.NewDocumentType();
            documentType.LastUpdatedBy = "edm";
            documentType.LastUpdatedDate = DateTime.Now;
            documentType.Description = "valid length";
            Assert.IsTrue(documentType.IsValid);
            
            documentType.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.IsNotNull(documentType);
            Assert.IsFalse(documentType.IsValid);
            Assert.AreEqual(documentType.BrokenRulesCollection[0].Description,
                "The field Description must be a string or array type with a maximum length of '50'.");
 
        }        
        // test exception if attempt to save in invalid state

        [TestMethod]
        public async Task TestDocumentTypeER_TestInvalidSave()
        {
            var documentType = await DocumentTypeER.NewDocumentType();
            documentType.Description = String.Empty;
            DocumentTypeER savedDocumentType = null;
                
            Assert.IsFalse(documentType.IsValid);
            Assert.ThrowsException<ValidationException>(() => savedDocumentType =  documentType.Save() );
        }
    }
}

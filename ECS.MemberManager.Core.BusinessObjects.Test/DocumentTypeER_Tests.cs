using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
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
        public void TestDocumentTypeER_Get()
        {
            var documentType = DocumentTypeER.GetDocumentTypeER(1);

            Assert.AreEqual(documentType.Id, 1);
            Assert.IsTrue(documentType.IsValid);
        }

        [TestMethod]
        public void TestDocumentTypeER_GetNewObject()
        {
            var documentType = DocumentTypeER.NewDocumentTypeER();

            Assert.IsNotNull(documentType);
            Assert.IsFalse(documentType.IsValid);
        }

        [TestMethod]
        public void TestDocumentTypeER_UpdateExistingObjectInDatabase()
        {
            var documentType = DocumentTypeER.GetDocumentTypeER(1);
            documentType.Notes = "These are updated Notes";
            
            var result = documentType.Save();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Notes, "These are updated Notes");
        }

        [TestMethod]
        public void TestDocumentTypeER_InsertNewObjectIntoDatabase()
        {
            var documentType = DocumentTypeER.NewDocumentTypeER();
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
        public void TestDocumentTypeER_DeleteObjectFromDatabase()
        {
            int beforeCount = MockDb.DocumentTypes.Count();
            
            DocumentTypeER.DeleteDocumentTypeER(1);
            
            Assert.AreNotEqual(beforeCount,MockDb.DocumentTypes.Count());
        }
        
        // test invalid state 
        [TestMethod]
        public void TestDocumentTypeER_DescriptionRequired()
        {
            var documentType = DocumentTypeER.NewDocumentTypeER();
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
        public void TestDocumentTypeER_DescriptionExceedsMaxLengthOf255()
        {
            var documentType = DocumentTypeER.NewDocumentTypeER();
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
        public void TestDocumentTypeER_TestInvalidSave()
        {
            var documentType = DocumentTypeER.NewDocumentTypeER();
            documentType.Description = String.Empty;
            DocumentTypeER savedDocumentType = null;
                
            
            Assert.IsFalse(documentType.IsValid);
            Assert.ThrowsException<ValidationException>(() => savedDocumentType =  documentType.Save() );
        }
    }
}

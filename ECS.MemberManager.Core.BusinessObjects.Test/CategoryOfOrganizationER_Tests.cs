using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ECS.MemberManager.Core.BusinessObjects.Test
{
    public class CategoryOfOrganizationER_Tests
    {
       [TestMethod]
        public async Task TestCategoryOfOrganizationER_Get()
        {
            var documentType = await CategoryOfOrganizationER.GetCategoryOfOrganization(1);

            Assert.AreEqual(documentType.Id, 1);
            Assert.IsTrue(documentType.IsValid);
        }

        [TestMethod]
        public async Task TestCategoryOfOrganizationER_GetNewObject()
        {
            var documentType = await CategoryOfOrganizationER.NewCategoryOfOrganization();

            Assert.IsNotNull(documentType);
            Assert.IsFalse(documentType.IsValid);
        }

        [TestMethod]
        public async Task TestCategoryOfOrganizationER_UpdateExistingObjectInDatabase()
        {
            var documentType = await CategoryOfOrganizationER.GetCategoryOfOrganization(1);
            documentType.DisplayOrder = 2;
            
            
            var result = documentType.Save();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.DisplayOrder,2);
        }

        [TestMethod]
        public async Task TestCategoryOfOrganizationER_InsertNewObjectIntoDatabase()
        {
            var documentType = await CategoryOfOrganizationER.NewCategoryOfOrganization();
            documentType.Name = "Category 1";

            var savedCategoryOfOrganization = documentType.Save();
           
            Assert.IsNotNull(savedCategoryOfOrganization);
            Assert.IsInstanceOfType(savedCategoryOfOrganization, typeof(CategoryOfOrganizationER));
            Assert.IsTrue( savedCategoryOfOrganization.Id > 0 );
        }

        [TestMethod]
        public async Task TestCategoryOfOrganizationER_DeleteObjectFromDatabase()
        {
            int beforeCount = MockDb.CategoryOfOrganizations.Count();

            await CategoryOfOrganizationER.DeleteCategoryOfOrganization(1);
            
            Assert.AreNotEqual(beforeCount,MockDb.CategoryOfOrganizations.Count());
        }
        
        // test invalid state 
        [TestMethod]
        public async Task TestCategoryOfOrganizationER_NameRequired() 
        {
            var documentType = await CategoryOfOrganizationER.NewCategoryOfOrganization();
            documentType.Name = String.Empty;
            var isObjectValidInit = documentType.IsValid;

            Assert.IsNotNull(documentType);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(documentType.IsValid);
        }
       
        [TestMethod]
        public async Task TestCategoryOfOrganizationER_DescriptionExceedsMaxLengthOf35()
        {
            var documentType = await CategoryOfOrganizationER.NewCategoryOfOrganization();
            Assert.IsTrue(documentType.IsValid);

            documentType.Name =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.IsNotNull(documentType);
            Assert.IsFalse(documentType.IsValid);
            Assert.AreEqual(documentType.BrokenRulesCollection[0].Description,
                "The field Name must be a string or array type with a maximum length of '35'.");
 
        }        
        // test exception if attempt to save in invalid state

        [TestMethod]
        public async Task TestCategoryOfOrganizationER_TestInvalidSave()
        {
            var documentType = await CategoryOfOrganizationER.NewCategoryOfOrganization();
            documentType.Name = String.Empty;
            CategoryOfOrganizationER savedCategoryOfOrganization = null;
                
            Assert.IsFalse(documentType.IsValid);
            Assert.ThrowsException<ValidationException>(() => savedCategoryOfOrganization =  documentType.Save() );
        }
    }
}
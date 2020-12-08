using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ECS.MemberManager.Core.BusinessObjects.Test
{
    [TestClass]
    public class OrganizationTypeER_Tests
    {
       [TestMethod]
        public async Task TestOrganizationTypeER_Get()
        {
            var organizationType = await OrganizationTypeER.GetOrganizationType(1);

            Assert.AreEqual(organizationType.Id, 1);
            Assert.IsTrue(organizationType.IsValid);
        }

        [TestMethod]
        public async Task TestOrganizationTypeER_New()
        {
            var organizationType = await OrganizationTypeER.NewOrganizationType();

            Assert.IsNotNull(organizationType);
            Assert.IsFalse(organizationType.IsValid);
        }

        [TestMethod]
        public async Task TestOrganizationTypeER_Update()
        {
            var organizationType = await OrganizationTypeER.GetOrganizationType(1);
            organizationType.Notes = "These are updated Notes";
            
            var result = organizationType.Save();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Notes, "These are updated Notes");
        }

        [TestMethod]
        public async Task TestOrganizationTypeER_Insert()
        {
            var organizationType = await OrganizationTypeER.NewOrganizationType();
            organizationType.Name = "Organization name";
            organizationType.Notes = "this is a great organization";

            var savedOrganizationType = organizationType.Save();
           
            Assert.IsNotNull(savedOrganizationType);
            Assert.IsInstanceOfType(savedOrganizationType, typeof(OrganizationTypeER));
            Assert.IsTrue( savedOrganizationType.Id > 0 );
        }

        [TestMethod]
        public async Task TestOrganizationTypeER_Delete()
        {
            int beforeCount = MockDb.OrganizationTypes.Count();
            
            await OrganizationTypeER.DeleteOrganizationType(1);
            
            Assert.AreNotEqual(beforeCount,MockDb.OrganizationTypes.Count());
        }
        
        // test invalid state 
        [TestMethod]
        public async Task TestOrganizationTypeER_NameRequired()
        {
            var organizationType = await OrganizationTypeER.NewOrganizationType();
            organizationType.Name = "make valid";
            var isObjectValidInit = organizationType.IsValid;
            organizationType.Name = string.Empty;

            Assert.IsNotNull(organizationType);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(organizationType.IsValid);
 
        }
       
        [TestMethod]
        public async Task TestOrganizationTypeER_DescriptionExceedsMaxLengthOf50()
        {
            var organizationType = await OrganizationTypeER.NewOrganizationType();
            organizationType.Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.IsFalse(organizationType.IsValid);
            Assert.AreEqual(organizationType.BrokenRulesCollection[0].Description,
                "The field Name must be a string or array type with a maximum length of '50'.");
 
        }        
        // test exception if attempt to save in invalid state

        [TestMethod]
        public async Task TestOrganizationTypeER_TestInvalidSave()
        {
            var organizationType = await OrganizationTypeER.NewOrganizationType();
            OrganizationTypeER savedOrganizationType = null;
            
            Assert.IsFalse(organizationType.IsValid);
            Assert.ThrowsException<ValidationException>(() => savedOrganizationType =  organizationType.Save() );
        }
        

    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ECS.MemberManager.Core.BusinessObjects.Test
{
    [TestClass]
    public class OrganizationER_Tests
    {
        [TestMethod]
        public async Task TestOrganizationER_Get()
        {
            var organization = await OrganizationER.GetOrganization(1);

            Assert.AreEqual(organization.Id, 1);
            Assert.IsTrue(organization.IsValid);
        }
        
        [TestMethod]
        public async Task TestOrganizationER_New()
        {
            var organizationType = await OrganizationER.NewOrganization();

            Assert.IsNotNull(organizationType);
            Assert.IsFalse(organizationType.IsValid);
        }

        [TestMethod]
        public async Task TestOrganizationER_Update()
        {
            var organization = await OrganizationER.GetOrganization(1);
            organization.Notes = "These are updated Notes";
            
            var result = organization.Save();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Notes, "These are updated Notes");
        }

        [TestMethod]
        public async Task TestOrganizationER_Insert()
        {
            var organization = await OrganizationER.NewOrganization();
            organization.Name = "Organization name";
            organization.Notes = "notes for org";

            var savedOrganization = organization.Save();
           
            Assert.IsNotNull(savedOrganization);
            Assert.IsInstanceOfType(savedOrganization, typeof(OrganizationER));
            Assert.IsTrue( savedOrganization.Id > 0 );
        }

        [TestMethod]
        public async Task TestOrganizationER_Delete()
        {
            int beforeCount = MockDb.Organizations.Count();
            
            await OrganizationER.DeleteOrganization(1);
            
            Assert.AreNotEqual(beforeCount,MockDb.Organizations.Count());
        }
        
        // test invalid state 
        [TestMethod]
        public async Task TestOrganizationER_DescriptionRequired()
        {
            var organization = await OrganizationER.NewOrganization();
            organization.Name = "make valid";
            var isObjectValidInit = organization.IsValid;
            organization.Name = string.Empty;

            Assert.IsNotNull(organization);
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(organization.IsValid);
 
        }
       
        [TestMethod]
        public async Task TestOrganizationER_DescriptionExceedsMaxLengthOf50()
        {
            var organization = await OrganizationER.NewOrganization();
            organization.Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.IsNotNull(organization);
            Assert.IsFalse(organization.IsValid);
            Assert.AreEqual(organization.BrokenRulesCollection[0].Description,
                "The field Name must be a string or array type with a maximum length of '50'.");
 
        }        
        // test exception if attempt to save in invalid state

        [TestMethod]
        public async Task TestOrganizationER_TestInvalidSave()
        {
            var organization = await OrganizationER.NewOrganization();
            OrganizationER savedOrganization = null;
            
            Assert.IsFalse(organization.IsValid);
            Assert.ThrowsException<ValidationException>(() => savedOrganization =  organization.Save() );
        }

    }
}
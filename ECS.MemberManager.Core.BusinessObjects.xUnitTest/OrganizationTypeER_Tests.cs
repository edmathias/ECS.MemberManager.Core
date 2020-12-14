using System;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OrganizationTypeER_Tests
    {
       [Fact]
        public async Task TestOrganizationTypeER_Get()
        {
            var organizationType = await OrganizationTypeER.GetOrganizationType(1);

            Assert.Equal(1, organizationType.Id);
            Assert.True(organizationType.IsValid);
        }

        [Fact]
        public async Task TestOrganizationTypeER_New()
        {
            var organizationType = await OrganizationTypeER.NewOrganizationType();

            Assert.NotNull(organizationType);
            Assert.False(organizationType.IsValid);
        }

        [Fact]
        public async Task TestOrganizationTypeER_Update()
        {
            var organizationType = await OrganizationTypeER.GetOrganizationType(1);
            organizationType.Notes = "These are updated Notes";
            
            var result = organizationType.Save();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes",result.Notes);
        }

        [Fact]
        public async Task TestOrganizationTypeER_Insert()
        {
            var organizationType = await OrganizationTypeER.NewOrganizationType();
            organizationType.Name = "Organization name";
            organizationType.Notes = "this is a great organization";

            var savedOrganizationType = organizationType.Save();
           
            Assert.NotNull(savedOrganizationType);
            Assert.IsType<OrganizationTypeER>(savedOrganizationType);
            Assert.True( savedOrganizationType.Id > 0 );
        }

        [Fact]
        public async Task TestOrganizationTypeER_Delete()
        {
            int beforeCount = MockDb.OrganizationTypes.Count();
            
            await OrganizationTypeER.DeleteOrganizationType(99);
            
            Assert.NotEqual(beforeCount,MockDb.OrganizationTypes.Count());
        }
        
        // test invalid state 
        [Fact]
        public async Task TestOrganizationTypeER_NameRequired()
        {
            var organizationType = await OrganizationTypeER.NewOrganizationType();
            organizationType.Name = "make valid";
            var isObjectValidInit = organizationType.IsValid;
            organizationType.Name = string.Empty;

            Assert.NotNull(organizationType);
            Assert.True(isObjectValidInit);
            Assert.False(organizationType.IsValid);
 
        }
       
        [Fact]
        public async Task TestOrganizationTypeER_DescriptionExceedsMaxLengthOf50()
        {
            var organizationType = await OrganizationTypeER.NewOrganizationType();
            organizationType.Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.False(organizationType.IsValid);
            Assert.Equal("The field Name must be a string or array type with a maximum length of '50'.", 
                organizationType.BrokenRulesCollection[0].Description);
 
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestOrganizationTypeER_TestInvalidSave()
        {
            var organizationType = await OrganizationTypeER.NewOrganizationType();
            OrganizationTypeER savedOrganizationType = null;
            
            Assert.False(organizationType.IsValid);
            Assert.Throws<ValidationException>(() => savedOrganizationType =  organizationType.Save() );
        }
        

    }
}
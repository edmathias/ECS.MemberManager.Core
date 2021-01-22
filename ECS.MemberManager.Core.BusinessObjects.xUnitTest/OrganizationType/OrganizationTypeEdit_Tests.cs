using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OrganizationTypeEdit_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public OrganizationTypeEdit_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");

            if (testLibrary == "Mock")
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
        public async void OrganizationTypeEdit_Get()
        {
            var organizationType = await OrganizationTypeEdit.GetOrganizationTypeEdit(1);
            
            Assert.NotNull(organizationType.CategoryOfOrganization);
            Assert.Equal(1, organizationType.Id);
            Assert.True(organizationType.IsValid);
        }

        [Fact]
        public async void OrganizationTypeEdit_GetNewObject()
        {
            var organizationType = await OrganizationTypeEdit.NewOrganizationTypeEdit();

            Assert.NotNull(organizationType);
            Assert.False(organizationType.IsValid);
        }

        [Fact]
        public async void OrganizationTypeEdit_UpdateExistingObjectInDatabase()
        {
            var updatedName = "new name";
            var organizationType = await OrganizationTypeEdit.GetOrganizationTypeEdit(1);
            organizationType.Name = updatedName;
            
            var result = await organizationType.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal(updatedName,result.Name);
        }

        [Fact]
        public async Task OrganizationTypeEdit_InsertNewObjectIntoDatabase()
        {
            var organizationType = await OrganizationTypeEdit.NewOrganizationTypeEdit();
            organizationType.Name = "inserted name";
            organizationType.CategoryOfOrganization = await CategoryOfOrganizationEdit.GetCategoryOfOrganizationEdit(1);

            var savedOrganizationType = await organizationType.SaveAsync();
           
            Assert.NotNull(savedOrganizationType);
            Assert.IsType<OrganizationTypeEdit>(savedOrganizationType);
            Assert.True( savedOrganizationType.Id > 0 );
        }

        [Fact]
        public async Task OrganizationTypeEdit_DeleteObjectFromDatabase()
        {
            const int ID_TO_DELETE = 99;
            
            await OrganizationTypeEdit.DeleteOrganizationTypeEdit(ID_TO_DELETE);
            
            var categoryToCheck = await Assert.ThrowsAsync<Csla.DataPortalException>
                (() => OrganizationTypeEdit.GetOrganizationTypeEdit(ID_TO_DELETE));        
        }
        
        // test invalid state 
        [Fact]
        public async Task OrganizationTypeEdit_NameRequired() 
        {
            var organizationType = await OrganizationTypeEdit.NewOrganizationTypeEdit();
            organizationType.Name = "Make it valid";
            var isObjectValidInit = organizationType.IsValid;
            organizationType.Name = String.Empty;

            Assert.NotNull(organizationType);
            Assert.True(isObjectValidInit);
            Assert.False(organizationType.IsValid);
        }
       
        [Fact]
        public async Task OrganizationTypeEdit_NameExceedsMaxLengthOf50()
        {
            var organizationType = await OrganizationTypeEdit.NewOrganizationTypeEdit();
            organizationType.Name = "valid name";
            Assert.True(organizationType.IsValid);

            organizationType.Name =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(organizationType);
            Assert.False(organizationType.IsValid);
            Assert.Equal("The field Name must be a string or array type with a maximum length of '50'.",
                organizationType.BrokenRulesCollection[0].Description);
 
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task OrganizationTypeEdit_TestInvalidSave()
        {
            var organizationType = await OrganizationTypeEdit.NewOrganizationTypeEdit();
            organizationType.Name = String.Empty;
                
            Assert.False(organizationType.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => organizationType.Save());
        }

 
    }
}
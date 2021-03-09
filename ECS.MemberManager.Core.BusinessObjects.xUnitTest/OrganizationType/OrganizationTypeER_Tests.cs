using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OrganizationTypeER_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public OrganizationTypeER_Tests()
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
        public async void OrganizationTypeER_Get()
        {
            var organizationType = await OrganizationTypeER.GetOrganizationTypeER(1);
            
            Assert.NotNull(organizationType);
            Assert.Equal(1, organizationType.Id);
            Assert.True(organizationType.IsValid);
        }

        [Fact]
        public async void OrganizationTypeER_GetNewObject()
        {
            var organizationType = await OrganizationTypeER.NewOrganizationTypeER();

            Assert.NotNull(organizationType);
            Assert.False(organizationType.IsValid);
        }

        [Fact]
        public async void OrganizationTypeER_UpdateExistingObjectInDatabase()
        {
            var updatedName = "new name";
            var organizationType = await OrganizationTypeER.GetOrganizationTypeER(1);
            organizationType.Name = updatedName;
            
            var result = await organizationType.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal(updatedName,result.Name);
        }

        [Fact]
        public async Task OrganizationTypeER_InsertNewObjectIntoDatabase()
        {
            var organizationType = await OrganizationTypeER.NewOrganizationTypeER();
            organizationType.Name = "inserted name";
            organizationType.CategoryOfOrganization = await CategoryOfOrganizationEC.GetCategoryOfOrganizationEC(new CategoryOfOrganization
            {
                Id = 1,
                Category = "Category name",
                DisplayOrder = 1
            });

            var savedOrganizationType = await organizationType.SaveAsync();
           
            Assert.NotNull(savedOrganizationType);
            Assert.IsType<OrganizationTypeER>(savedOrganizationType);
            Assert.True( savedOrganizationType.Id > 0 );
        }

        [Fact]
        public async Task OrganizationTypeER_DeleteObjectFromDatabase()
        {
            const int ID_TO_DELETE = 99;
            
            await OrganizationTypeER.DeleteOrganizationTypeER(ID_TO_DELETE);
            
            var categoryToCheck = await Assert.ThrowsAsync<Csla.DataPortalException>
                (() => OrganizationTypeER.GetOrganizationTypeER(ID_TO_DELETE));        
        }
        
        // test invalid state 
        [Fact]
        public async Task OrganizationTypeER_NameRequired() 
        {
            var organizationType = await OrganizationTypeER.NewOrganizationTypeER();
            organizationType.Name = "valid name";
            organizationType.CategoryOfOrganization = await CategoryOfOrganizationEC.NewCategoryOfOrganizationEC();
            organizationType.CategoryOfOrganization.Category = "category name";
            var isObjectValidInit = organizationType.IsValid;
            organizationType.Name = String.Empty;

            Assert.NotNull(organizationType);
            Assert.True(isObjectValidInit);
            Assert.False(organizationType.IsValid);
        }
       
        [Fact]
        public async Task OrganizationTypeER_NameExceedsMaxLengthOf50()
        {
            var organizationType = await OrganizationTypeER.NewOrganizationTypeER();
            organizationType.Name = "valid name";
            organizationType.CategoryOfOrganization = await CategoryOfOrganizationEC.NewCategoryOfOrganizationEC();
            organizationType.CategoryOfOrganization.Category = "category name";
            var isValidOrig = organizationType.IsValid;
            
            organizationType.Name =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(organizationType);
            Assert.True(isValidOrig);
            Assert.False(organizationType.IsValid);
            Assert.Equal("Name", organizationType.BrokenRulesCollection[0].Property);

            Assert.Equal("Name can not exceed 50 characters",
                organizationType.BrokenRulesCollection[0].Description);
 
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task OrganizationTypeER_TestInvalidSave()
        {
            var organizationType = await OrganizationTypeER.NewOrganizationTypeER();
            organizationType.Name = String.Empty;
                
            Assert.False(organizationType.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => organizationType.Save());
        }
    }
}

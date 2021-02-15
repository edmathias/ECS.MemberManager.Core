﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OrganizationTypeEC_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public OrganizationTypeEC_Tests()
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
        public async Task TestOrganizationTypeEC_GetNewOrganizationTypeEC()
        {
            var categoryOfOrganizationToLoad = BuildOrganizationType();
            var categoryOfOrganization = await OrganizationTypeEC.GetOrganizationTypeEC(categoryOfOrganizationToLoad);

            Assert.NotNull(categoryOfOrganization);
            Assert.IsType<OrganizationTypeEC>(categoryOfOrganization);
            Assert.Equal(categoryOfOrganizationToLoad.Id,categoryOfOrganization.Id);
            Assert.Equal(categoryOfOrganizationToLoad.Name,categoryOfOrganization.Name);
            Assert.Equal(categoryOfOrganizationToLoad.Notes,categoryOfOrganization.Notes);
            Assert.True(categoryOfOrganization.IsValid);
        }

        [Fact]
        public async Task TestOrganizationTypeEC_NewOrganizationTypeEC()
        {
            var category = await OrganizationTypeEC.NewOrganizationTypeEC();

            Assert.NotNull(category);
            Assert.IsType<OrganizationTypeEC>(category);
            Assert.False(category.IsValid);
        }

        [Fact]
        public async Task TestOrganizationTypeEC_CategoryRequired()
        {
            var categoryToTest = BuildOrganizationType();
            var category = await OrganizationTypeEC.GetOrganizationTypeEC(categoryToTest);
            var isObjectValidInit = category.IsValid;
            category.Name = string.Empty;

            Assert.NotNull(category);
            Assert.True(isObjectValidInit);
            Assert.False(category.IsValid);
            Assert.Equal("Name",category.BrokenRulesCollection[0].Property);
        }

        
        private OrganizationType BuildOrganizationType()
        {
            var category = new OrganizationType()
            {
                Name = "organization type name 1",
                Notes = "org type notes",
                CategoryOfOrganization = new CategoryOfOrganization() { Id =1,DisplayOrder = 1,Category = "cat name"}
            };

            return category;
        }
    }
}
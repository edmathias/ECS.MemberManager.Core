﻿using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OrganizationTypeRORL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public OrganizationTypeRORL_Tests()
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
        private async void OrganizationTypeRORL_TestGetOrganizationTypeRORL()
        {
            var categoryOfOrganizationTypeInfoList = await OrganizationTypeRORL.GetOrganizationTypeRORL();

            Assert.NotNull(categoryOfOrganizationTypeInfoList);
            Assert.True(categoryOfOrganizationTypeInfoList.IsReadOnly);
            Assert.Equal(3, categoryOfOrganizationTypeInfoList.Count);
        }
    }
}
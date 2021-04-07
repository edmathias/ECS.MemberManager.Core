﻿using System;
using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OfficeECL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public OfficeECL_Tests()
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
        private async void OfficeECL_TestOfficeECL()
        {
            var officeEdit = await OfficeECL.NewOfficeECL();

            Assert.NotNull(officeEdit);
            Assert.IsType<OfficeECL>(officeEdit);
        }


        [Fact]
        private async void OfficeECL_TestGetOfficeECL()
        {
            var childData = MockDb.Offices;

            var listToTest = await OfficeECL.GetOfficeECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }

        private void BuildOffice(OfficeEC officeObj)
        {
            officeObj.Term = 1;
            officeObj.CalendarPeriod = "annual";
            officeObj.Name = "office name";
            officeObj.ChosenHow = 2;
            officeObj.Appointer = "members";
            officeObj.LastUpdatedBy = "edm";
            officeObj.LastUpdatedDate = DateTime.Now;
            officeObj.Notes = "notes";
        }
    }
}
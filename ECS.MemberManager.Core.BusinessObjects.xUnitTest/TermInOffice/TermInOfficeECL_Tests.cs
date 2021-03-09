﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TermInOfficeECL_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public TermInOfficeECL_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");
            
            if(testLibrary == "Mock")
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
        private async void TermInOfficeECL_TestTermInOfficeECL()
        {
            var eventObjEdit = await TermInOfficeECL.NewTermInOfficeECL();

            Assert.NotNull(eventObjEdit);
            Assert.IsType<TermInOfficeECL>(eventObjEdit);
        }

        
        [Fact]
        private async void TermInOfficeECL_TestGetTermInOfficeECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITermInOfficeDal>();
            var childData = await dal.Fetch();
            
            var listToTest = await TermInOfficeECL.GetTermInOfficeECL(childData);
            
            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }

  
    }
}
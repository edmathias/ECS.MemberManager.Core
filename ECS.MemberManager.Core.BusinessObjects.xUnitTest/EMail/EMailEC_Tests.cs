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
    public class EMailEC_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EMailEC_Tests()
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
        public async Task TestEMailEC_NewEMailEC()
        {
            var eMail = await EMailEC.NewEMailEC();

            Assert.NotNull(eMail);
            Assert.IsType<EMailEC>(eMail);
            Assert.False(eMail.IsValid);
        }
        
        [Fact]
        public async Task TestEMailEC_GetEMailEC()
        {
            var eMailToLoad = BuildEMail();
            var eMail = await EMailEC.GetEMailEC(eMailToLoad);

            Assert.NotNull(eMail);
            Assert.IsType<EMailEC>(eMail);
            Assert.Equal(eMailToLoad.Id,eMail.Id);
            Assert.Equal(eMailToLoad.LastUpdatedBy, eMail.LastUpdatedBy);
            Assert.Equal(new SmartDate(eMailToLoad.LastUpdatedDate), eMail.LastUpdatedDate);
            Assert.Equal(eMailToLoad.Notes, eMail.Notes);
            Assert.Equal(eMailToLoad.RowVersion, eMail.RowVersion);
            Assert.True(eMail.IsValid);
        }

        [Fact]
        public async Task TestEMailEC_EMailAddressRequired()
        {
            var eMailToTest = BuildEMail();
            var eMail = await EMailEC.GetEMailEC(eMailToTest);
            var isObjectValidInit = eMail.IsValid;
            eMail.EMailAddress = string.Empty;

            Assert.NotNull(eMail);
            Assert.True(isObjectValidInit);
            Assert.False(eMail.IsValid);
            Assert.Equal("EMailAddress",eMail.BrokenRulesCollection[0].Property);
            Assert.Equal("EMailAddress required",eMail.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestEMailEC_EMailAddressGreaterThan255Chars()
        {
            var eMailToTest = BuildEMail();
            var eMail = await EMailEC.GetEMailEC(eMailToTest);
            var isObjectValidInit = eMail.IsValid;
            eMail.EMailAddress = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempo" +
                                 "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud" +
                                 "exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure" +
                                 "dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.";
            Assert.NotNull(eMail);
            Assert.True(isObjectValidInit);
            Assert.False(eMail.IsValid);
            Assert.Equal("EMailAddress",eMail.BrokenRulesCollection[0].Property);
            Assert.Equal("EMailAddress can not exceed 255 characters",eMail.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestEMailEC_LastUpdatedByRequired()
        {
            var eMailToTest = BuildEMail();
            var eMail = await EMailEC.GetEMailEC(eMailToTest);
            var isObjectValidInit = eMail.IsValid;
            eMail.LastUpdatedBy = string.Empty;

            Assert.NotNull(eMail);
            Assert.True(isObjectValidInit);
            Assert.False(eMail.IsValid);
            Assert.Equal("LastUpdatedBy",eMail.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy required",eMail.BrokenRulesCollection[0].Description);
        }
     
        [Fact]
        public async Task TestEMailEC_LastUpdatedByExceeds255Characters()
        {
            var eMailToTest = BuildEMail();
            var eMail = await EMailEC.GetEMailEC(eMailToTest);
            var isObjectValidInit = eMail.IsValid;
            eMail.LastUpdatedBy = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempo" +
                                  "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud" +
                                  "exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure" +
                                  "dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.";

            Assert.NotNull(eMail);
            Assert.True(isObjectValidInit);
            Assert.False(eMail.IsValid);
            Assert.Equal("LastUpdatedBy",eMail.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy can not exceed 255 characters",eMail.BrokenRulesCollection[0].Description);
        }
        
        private EMail BuildEMail()
        {
            var eMail = new EMail();
            eMail.Id = 1;
            eMail.EMailType = new EMailType();
            eMail.EMailAddress = "email@email.com";
            eMail.LastUpdatedBy = "edm";
            eMail.LastUpdatedDate = DateTime.Now;
            eMail.Notes = "notes for doctype";

            return eMail;
        }        
    }
}
using System;
using System.IO;
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
    public class EMailER_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EMailER_Tests()
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
        public async Task TestEMailER_Get()
        {
            var eMail = await EMailER.GetEMailER(1);

            Assert.NotNull(eMail);
            Assert.IsType<EMailER>(eMail);
            Assert.NotNull(eMail.EMailType);
            Assert.Equal(1, eMail.Id);
            Assert.True(eMail.IsValid);
        }

        [Fact]
        public async Task TestEMailER_New()
        {
            var eMail = await EMailER.NewEMailER();

            Assert.NotNull(eMail);
            Assert.False(eMail.IsValid);
        }

        [Fact]
        public async void TestEMailER_Update()
        {
            var eMail = await EMailER.GetEMailER(1);
            var notesUpdate = $"These are updated Notes {DateTime.Now}";
            eMail.Notes = notesUpdate;

            var result = await eMail.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal(notesUpdate, result.Notes);
        }

        [Fact]
        public async void TestEMailER_Insert()
        {
            var eMail = await EMailER.NewEMailER();
            await BuildEMail(eMail);

            var savedEMail = await eMail.SaveAsync();

            Assert.NotNull(savedEMail);
            Assert.IsType<EMailER>(savedEMail);
            Assert.True(savedEMail.Id > 0);
            Assert.NotNull(savedEMail.RowVersion);
        }

        [Fact]
        public async Task TestEMailER_Delete()
        {
            await EMailER.DeleteEMailER(99);

            await Assert.ThrowsAsync<Csla.DataPortalException>
                (() => EMailER.GetEMailER(99));
        }


          // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestEMailER_TestInvalidSave()
        {
            var eMail = await EMailER.NewEMailER();
            await BuildEMail(eMail);
            eMail.EMailAddress = string.Empty;

            Assert.False(eMail.IsValid);
            await Assert.ThrowsAsync<ValidationException>(() => eMail.SaveAsync());
        }

        [Fact]
        public async Task EMailER_TestSaveOutOfOrder()
        {
            var emailType1 = await EMailER.GetEMailER(1);
            var emailType2 = await EMailER.GetEMailER(1);
            emailType1.Notes = "set up timestamp issue"; // turn on IsDirty
            emailType2.Notes = "set up timestamp issue";

            var emailType2_2 = await emailType2.SaveAsync();

            Assert.NotEqual(emailType2_2.RowVersion, emailType1.RowVersion);
            Assert.Equal("set up timestamp issue", emailType2_2.Notes);
            await Assert.ThrowsAsync<DataPortalException>(() => emailType1.SaveAsync());
        }

        [Fact]
        public async Task EMailER_TestSubsequentSaves()
        {
            var emailType = await EMailER.GetEMailER(1);
            emailType.Notes = "set up timestamp issue"; // turn on IsDirty

            var emailType2 = await emailType.SaveAsync();
            var rowVersion1 = emailType2.RowVersion;
            emailType2.Notes = "another timestamp trigger";

            var emailType3 = await emailType2.SaveAsync();

            Assert.NotEqual(emailType2.RowVersion, emailType3.RowVersion);
        }

        [Fact]
        public async Task TestEMailER_InvalidGet()
        {
            await Assert.ThrowsAsync<DataPortalException>(() => EMailER.GetEMailER(999));
        }

        [Fact]
        public async Task TestEMailER_EMailAddressRequired()
        {
            var eMail = await EMailER.NewEMailER();
            await BuildEMail(eMail);
            var isObjectValidInit = eMail.IsValid;
            eMail.EMailAddress = string.Empty;

            Assert.NotNull(eMail);
            Assert.True(isObjectValidInit);
            Assert.False(eMail.IsValid);
            Assert.Equal("EMailAddress",eMail.BrokenRulesCollection[0].Property);
            Assert.Equal("EMailAddress required",eMail.BrokenRulesCollection[0].Description);
            
        }

        [Fact]
        public async Task TestEMailER_EMailAddressMaxLengthLessThan255()
        {
            var eMailType = await EMailER.NewEMailER();
            await BuildEMail(eMailType);
            var isObjectValidInit = eMailType.IsValid;
            eMailType.EMailAddress =  "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                      "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                      "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                      "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(eMailType);
            Assert.True(isObjectValidInit);
            Assert.False(eMailType.IsValid);
            Assert.Equal("EMailAddress",eMailType.BrokenRulesCollection[0].Property);
            Assert.Equal("EMailAddress can not exceed 255 characters",eMailType.BrokenRulesCollection[0].Description);
        }
       
        [Fact]
        public async Task TestEMailER_LastUpdatedByRequired()
        {
            var eMailType = await EMailER.NewEMailER();
            await BuildEMail(eMailType);
            var isObjectValidInit = eMailType.IsValid;
            eMailType.LastUpdatedBy = string.Empty;

            Assert.NotNull(eMailType);
            Assert.True(isObjectValidInit);
            Assert.False(eMailType.IsValid);
            Assert.Equal("LastUpdatedBy",eMailType.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy required",eMailType.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestEMailER_LastUpdatedByMaxLengthLessThan255()
        {
            var eMailType = await EMailER.NewEMailER();
            await BuildEMail(eMailType);
            var isObjectValidInit = eMailType.IsValid;
            eMailType.LastUpdatedBy =  "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                      "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                      "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                      "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(eMailType);
            Assert.True(isObjectValidInit);
            Assert.False(eMailType.IsValid);
            Assert.Equal("LastUpdatedBy",eMailType.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy can not exceed 255 characters",eMailType.BrokenRulesCollection[0].Description);
        }
        
        private async Task BuildEMail(EMailER eMailToBuild)
        {
            eMailToBuild.Notes = "member type notes";
            eMailToBuild.EMailAddress = "edm@ecs.com";
            eMailToBuild.LastUpdatedBy = "edm";
            eMailToBuild.LastUpdatedDate = DateTime.Now;
            eMailToBuild.EMailType = await EMailTypeEC.GetEMailTypeEC(
                new EMailType()
                {
                    Id = 1,
                    Notes = "EMailType notes",
                    Description = "Email description"
                }
            );
        }
        
    }
}
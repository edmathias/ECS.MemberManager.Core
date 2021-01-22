using System;
using System.IO;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EMailEdit_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EMailEdit_Tests()
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
        public async Task TestEMailEdit_Get()
        {
            var eMail = await EMailEdit.GetEMailEdit(1);

            Assert.NotNull(eMail);
            Assert.IsType<EMailEdit>(eMail);
            Assert.NotNull(eMail.EMailType);
            Assert.Equal(1, eMail.Id);
            Assert.True(eMail.IsValid);
        }

        [Fact]
        public async Task TestEMailEdit_New()
        {
            var eMail = await EMailEdit.NewEMailEdit();

            Assert.NotNull(eMail);
            Assert.False(eMail.IsValid);
        }

        [Fact]
        public async void TestEMailEdit_Update()
        {
            var eMail = await EMailEdit.GetEMailEdit(1);
            var notesUpdate = $"These are updated Notes {DateTime.Now}";
            eMail.Notes = notesUpdate;

            var result = await eMail.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal(notesUpdate, result.Notes);
        }

        [Fact]
        public async void TestEMailEdit_Insert()
        {
            var eMail = await EMailEdit.NewEMailEdit();
            await BuildEMail(eMail);

            var savedEMail = await eMail.SaveAsync();

            Assert.NotNull(savedEMail);
            Assert.IsType<EMailEdit>(savedEMail);
            Assert.True(savedEMail.Id > 0);
            Assert.NotNull(savedEMail.RowVersion);
        }

        [Fact]
        public async Task TestEMailEdit_Delete()
        {
            await EMailEdit.DeleteEMailEdit(99);

            await Assert.ThrowsAsync<Csla.DataPortalException>
                (() => EMailEdit.GetEMailEdit(99));
        }


          // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestEMailEdit_TestInvalidSave()
        {
            var eMail = await EMailEdit.NewEMailEdit();
            await BuildEMail(eMail);
            eMail.EMailAddress = string.Empty;

            Assert.False(eMail.IsValid);
            await Assert.ThrowsAsync<ValidationException>(() => eMail.SaveAsync());
        }

        [Fact]
        public async Task EMailEdit_TestSaveOutOfOrder()
        {
            var emailType1 = await EMailEdit.GetEMailEdit(1);
            var emailType2 = await EMailEdit.GetEMailEdit(1);
            emailType1.Notes = "set up timestamp issue"; // turn on IsDirty
            emailType2.Notes = "set up timestamp issue";

            var emailType2_2 = await emailType2.SaveAsync();

            Assert.NotEqual(emailType2_2.RowVersion, emailType1.RowVersion);
            Assert.Equal("set up timestamp issue", emailType2_2.Notes);
            await Assert.ThrowsAsync<DataPortalException>(() => emailType1.SaveAsync());
        }

        [Fact]
        public async Task EMailEdit_TestSubsequentSaves()
        {
            var emailType = await EMailEdit.GetEMailEdit(1);
            emailType.Notes = "set up timestamp issue"; // turn on IsDirty

            var emailType2 = await emailType.SaveAsync();
            var rowVersion1 = emailType2.RowVersion;
            emailType2.Notes = "another timestamp trigger";

            var emailType3 = await emailType2.SaveAsync();

            Assert.NotEqual(emailType2.RowVersion, emailType3.RowVersion);
        }

        [Fact]
        public async Task TestEMailEdit_InvalidGet()
        {
            await Assert.ThrowsAsync<DataPortalException>(() => EMailEdit.GetEMailEdit(999));
        }

        [Fact]
        public async Task TestEMailEdit_EMailAddressRequired()
        {
            var eMailType = await EMailEdit.NewEMailEdit();
            await BuildEMail(eMailType);
            var isObjectValidInit = eMailType.IsValid;
            eMailType.EMailAddress = string.Empty;

            Assert.NotNull(eMailType);
            Assert.True(isObjectValidInit);
            Assert.False(eMailType.IsValid);
            Assert.Equal("EMailAddress",eMailType.BrokenRulesCollection[0].OriginProperty);
        }

        [Fact]
        public async Task TestEMailEdit_EMailAddressMaxLengthLessThan255()
        {
            var eMailType = await EMailEdit.NewEMailEdit();
            await BuildEMail(eMailType);
            var isObjectValidInit = eMailType.IsValid;
            eMailType.EMailAddress =  "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                      "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                      "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                      "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(eMailType);
            Assert.True(isObjectValidInit);
            Assert.False(eMailType.IsValid);
            Assert.Equal("EMailAddress",eMailType.BrokenRulesCollection[0].OriginProperty);
        }
       
        [Fact]
        public async Task TestEMailEdit_LastUpdatedByRequired()
        {
            var eMailType = await EMailEdit.NewEMailEdit();
            await BuildEMail(eMailType);
            var isObjectValidInit = eMailType.IsValid;
            eMailType.LastUpdatedBy = string.Empty;

            Assert.NotNull(eMailType);
            Assert.True(isObjectValidInit);
            Assert.False(eMailType.IsValid);
            Assert.Equal("LastUpdatedBy",eMailType.BrokenRulesCollection[0].OriginProperty);
        }

        [Fact]
        public async Task TestEMailEdit_LastUpdatedByMaxLengthLessThan255()
        {
            var eMailType = await EMailEdit.NewEMailEdit();
            await BuildEMail(eMailType);
            var isObjectValidInit = eMailType.IsValid;
            eMailType.LastUpdatedBy =  "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                      "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                      "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                      "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(eMailType);
            Assert.True(isObjectValidInit);
            Assert.False(eMailType.IsValid);
            Assert.Equal("LastUpdatedBy",eMailType.BrokenRulesCollection[0].OriginProperty);
        }
        
        private async Task BuildEMail(EMailEdit eMailToBuild)
        {
            eMailToBuild.Notes = "member type notes";
            eMailToBuild.EMailAddress = "edm@ecs.com";
            eMailToBuild.LastUpdatedBy = "edm";
            eMailToBuild.LastUpdatedDate = DateTime.Now;
            eMailToBuild.EMailType = await EMailTypeEdit.GetEMailTypeEdit(1);
        }
        
    }
}
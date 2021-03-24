using System;
using System.IO;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TermInOfficeER_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public TermInOfficeER_Tests()
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
        public async Task TermInOfficeER_TestGetTermInOffice()
        {
            var termObj = await TermInOfficeER.GetTermInOfficeER(1);

            Assert.NotNull(termObj);
            Assert.IsType<TermInOfficeER>(termObj);
            Assert.Equal(1, termObj.Id);
        }

        [Fact]
        public async Task TermInOfficeER_TestGetNewTermInOfficeER()
        {
            var termObj = await TermInOfficeER.NewTermInOfficeER();

            Assert.NotNull(termObj);
            Assert.False(termObj.IsValid);
        }

        [Fact]
        public async Task TermInOfficeER_TestUpdateExistingTermInOfficeER()
        {
            var termObj = await TermInOfficeER.GetTermInOfficeER(1);
            termObj.Notes = "These are updated Notes";

            var result = await termObj.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes", result.Notes);
        }

        [Fact]
        public async Task TermInOfficeER_TestInsertNewTermInOfficeER()
        {
            var termObj = await TermInOfficeER.NewTermInOfficeER();
            await BuildTermInOffice(termObj);

            var savedTermInOffice = await termObj.SaveAsync();

            Assert.NotNull(savedTermInOffice);
            Assert.IsType<TermInOfficeER>(savedTermInOffice);
            Assert.True(savedTermInOffice.Id > 0);
        }

        [Fact]
        public async Task TermInOfficeER_TestDeleteObjectFromDatabase()
        {
            const int ID_TO_DELETE = 99;

            await TermInOfficeER.DeleteTermInOfficeER(ID_TO_DELETE);

            await Assert.ThrowsAsync<DataPortalException>(() => TermInOfficeER.GetTermInOfficeER(ID_TO_DELETE));
        }

        [Fact]
        public async Task TermInOfficeER_TestInvalidSaveTermInOfficeER()
        {
            var termObj = await TermInOfficeER.NewTermInOfficeER();
            termObj.LastUpdatedBy = String.Empty;

            Assert.False(termObj.IsValid);
            await Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => termObj.SaveAsync());
        }

        private async Task BuildTermInOffice(TermInOfficeER termObj)
        {
            termObj.Office = await OfficeEC.GetOfficeEC(new Office() {Id = 1});
            termObj.Person = await PersonEC.GetPersonEC(new Person() {Id = 1});
            termObj.StartDate = DateTime.Now;
            termObj.LastUpdatedBy = "edm";
            termObj.LastUpdatedDate = DateTime.Now;
            termObj.Notes = "notes for doctype";
        }
    }
}
using System;
using System.Data;
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
    public class OfficeEdit_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public OfficeEdit_Tests()
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
        public async Task OfficeEdit_TestGetOfficeEdit()
        {
            var office = await OfficeEdit.GetOfficeEdit(1);

            Assert.NotNull(office);
            Assert.IsType<OfficeEdit>(office);
            Assert.Equal(1, office.Id);
            Assert.True(office.IsValid);
        }

        [Fact]
        public async Task OfficeEdit_TestNewOfficeEdit()
        {
            var office = await OfficeEdit.NewOfficeEdit();

            Assert.NotNull(office);
            Assert.False(office.IsValid);
        }

        [Fact]
        public async void OfficeEdit_TestUpdateOfficeEdit()
        {
            var office = await OfficeEdit.GetOfficeEdit(1);
            var notesUpdate = $"These are updated description.";
            office.Notes = notesUpdate;

            var result = await office.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal(notesUpdate, result.Notes);
        }

        [Fact]
        public async void OfficeEdit_TestInsertOfficeEdit()
        {
            var office = await OfficeEdit.NewOfficeEdit();
            BuildValidOffice(office); 

            var savedOffice = await office.SaveAsync();

            Assert.NotNull(savedOffice);
            Assert.IsType<OfficeEdit>(savedOffice);
            Assert.True(savedOffice.Id > 0);
            Assert.NotNull(savedOffice.RowVersion);
        }

        [Fact]
        public async Task OfficeEdit_DeleteOfficeEdit()
        {
            await OfficeEdit.DeleteOfficeEdit(99);

            var officeToCheck = await Assert.ThrowsAsync<Csla.DataPortalException>
                (() => OfficeEdit.GetOfficeEdit(99));
        }


        [Fact]
        public async Task OfficeEdit_TestInvalidSave()
        {
            var office = await OfficeEdit.NewOfficeEdit();
            office.Name = String.Empty;

            Assert.False(office.IsValid);
            await Assert.ThrowsAsync<ValidationException>(() => office.SaveAsync());
        }
    
        [Fact]
        public async Task OfficeEdit_TestSaveOutOfOrder()
        {
            var office1 = await OfficeEdit.GetOfficeEdit(1);
            var office2 = await OfficeEdit.GetOfficeEdit(1);
            office1.Notes = "set up timestamp issue";  // turn on IsDirty
            office2.Notes = "set up timestamp issue";

            var office2_2 = await office2.SaveAsync();
            
            Assert.NotEqual(office2_2.RowVersion, office1.RowVersion);
            Assert.Equal("set up timestamp issue",office2_2.Notes);
            await Assert.ThrowsAsync<DataPortalException>(() => office1.SaveAsync());
        }

        [Fact]
        public async Task OfficeEdit_TestSubsequentSaves()
        {
            var office = await OfficeEdit.GetOfficeEdit(1);
            office.Notes = "set up timestamp issue";  // turn on IsDirty

            var office2 = await office.SaveAsync();
            var rowVersion1 = office2.RowVersion;
            office2.Notes = "another timestamp trigger";

            var office3 = await office2.SaveAsync();
            
            Assert.NotEqual(office2.RowVersion, office3.RowVersion);
        }
        
        [Fact]
        public async Task OfficeEdit_InvalidGet()
        {
            await Assert.ThrowsAsync<DataPortalException>(() => OfficeEdit.GetOfficeEdit(999));
        }

        private void BuildValidOffice(OfficeEdit officeEdit)
        {
            officeEdit.Appointer = "appointer";
            officeEdit.Name = "office name";
            officeEdit.Term = 1;
            officeEdit.CalendarPeriod = "annual";
            officeEdit.ChosenHow = 1;
            officeEdit.LastUpdatedBy = "edm";
            officeEdit.LastUpdatedDate = DateTime.Now;
            officeEdit.Notes = "office notes";
        }
    }
}
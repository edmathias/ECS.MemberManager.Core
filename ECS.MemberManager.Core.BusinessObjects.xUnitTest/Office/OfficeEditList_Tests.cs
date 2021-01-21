using System;
using System.IO;
using System.Linq;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Xunit;
using DalManager = ECS.MemberManager.Core.DataAccess.ADO.DalManager;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OfficeEditList_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public OfficeEditList_Tests()
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
        private async void OfficeEditList_TestNewOfficeEditList()
        {
            var officeEdit = await OfficeEditList.NewOfficeEditList();

            Assert.NotNull(officeEdit);
            Assert.IsType<OfficeEditList>(officeEdit);
        }
        
        [Fact]
        private async void OfficeEditList_TestGetOfficeEditList()
        {
            var officeEdit = await OfficeEditList.GetOfficeEditList();

            Assert.NotNull(officeEdit);
            Assert.Equal(3, officeEdit.Count);
        }
        
        [Fact]
        private async void OfficeEditList_TestDeleteOfficesEntry()
        {
            var officeEdit = await OfficeEditList.GetOfficeEditList();
            var listCount = officeEdit.Count;
            var officeToDelete = officeEdit.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = officeEdit.Remove(officeToDelete); 

            var officeListAfterDelete = await officeEdit.SaveAsync();

            Assert.NotNull(officeListAfterDelete);
            Assert.IsType<OfficeEditList>(officeListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,officeListAfterDelete.Count);
        }

        [Fact]
        private async void OfficeEditList_TestUpdateOfficesEntry()
        {
            const int idToUpdate = 1;
            
            var officeEditList = await OfficeEditList.GetOfficeEditList();
            var countBeforeUpdate = officeEditList.Count;
            var officeToUpdate = officeEditList.First(a => a.Id == idToUpdate);
            officeToUpdate.Notes = "This was updated";

            var updatedList = await officeEditList.SaveAsync();
            
            Assert.Equal("This was updated",updatedList.First(a => a.Id == idToUpdate).Notes);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void OfficeEditList_TestAddOfficesEntry()
        {
            var officeEditList = await OfficeEditList.GetOfficeEditList();
            var countBeforeAdd = officeEditList.Count;
            
            var officeToAdd = officeEditList.AddNew();
            BuildOffice(officeToAdd);

            var updatedOfficesList = await officeEditList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedOfficesList.Count);
        }

        private void BuildOffice(OfficeEdit officeToBuild)
        {
            officeToBuild.Appointer = "appointer";
            officeToBuild.Name = "office name";
            officeToBuild.Term = 1;
            officeToBuild.CalendarPeriod = "annual";
            officeToBuild.ChosenHow = 1;
            officeToBuild.LastUpdatedBy = "edm";
            officeToBuild.LastUpdatedDate = DateTime.Now;
            officeToBuild.Notes = "office notes";
        }
        
 
    }
}
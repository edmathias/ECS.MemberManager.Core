using System;
using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OfficeERL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public OfficeERL_Tests()
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
        private async void OfficeERL_TestNewOfficeList()
        {
            var officeErl = await OfficeERL.NewOfficeERL();

            Assert.NotNull(officeErl);
            Assert.IsType<OfficeERL>(officeErl);
        }
        
        [Fact]
        private async void OfficeERL_TestGetOfficeERL()
        {
            var officeEditList = await OfficeERL.GetOfficeERL();

            Assert.NotNull(officeEditList);
            Assert.Equal(3, officeEditList.Count);
        }
        
        [Fact]
        private async void OfficeERL_TestDeleteOfficesEntry()
        {
            var officeErl = await OfficeERL.GetOfficeERL();
            var listCount = officeErl.Count;
            var officeToDelete = officeErl.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = officeErl.Remove(officeToDelete); 

            var officeListAfterDelete = await officeErl.SaveAsync();

            Assert.NotNull(officeListAfterDelete);
            Assert.IsType<OfficeERL>(officeListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,officeListAfterDelete.Count);
        }

        [Fact]
        private async void OfficeERL_TestUpdateOfficesEntry()
        {
            const int idToUpdate = 1;
            
            var officeEditList = await OfficeERL.GetOfficeERL();
            var countBeforeUpdate = officeEditList.Count;
            var officeToUpdate = officeEditList.First(a => a.Id == idToUpdate);
            officeToUpdate.Notes = "This was updated";

            var updatedList = await officeEditList.SaveAsync();
            
            Assert.Equal("This was updated",updatedList.First(a => a.Id == idToUpdate).Notes);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void OfficeERL_TestAddOfficesEntry()
        {
            var officeEditList = await OfficeERL.GetOfficeERL();
            var countBeforeAdd = officeEditList.Count;
            
            var officeToAdd = officeEditList.AddNew();
            officeToAdd.Term = 1;
            officeToAdd.CalendarPeriod = "annual";
            officeToAdd.Name = "office name";
            officeToAdd.ChosenHow = 2;
            officeToAdd.Appointer = "members";
            officeToAdd.LastUpdatedBy = "edm";
            officeToAdd.LastUpdatedDate = DateTime.Now;

            var updatedOfficesList = await officeEditList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedOfficesList.Count);
        }
    }
}
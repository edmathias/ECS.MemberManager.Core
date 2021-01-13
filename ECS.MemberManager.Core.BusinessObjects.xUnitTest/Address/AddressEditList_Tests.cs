using System;
using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class AddressEditList_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public AddressEditList_Tests()
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
        private async void AddressEditList_TestNewEMailList()
        {
            var addressErl = await AddressEditList.NewAddressEditList();

            Assert.NotNull(addressErl);
            Assert.IsType<AddressEditList>(addressErl);
        }
        
        [Fact]
        private async void AddressEditList_TestGetAddressEditList()
        {
            var addressEditList = await AddressEditList.GetAddressEditList();

            Assert.NotNull(addressEditList);
            Assert.Equal(3, addressEditList.Count);
        }
        
        [Fact]
        private async void AddressEditList_TestDeleteEMailsEntry()
        {
            var addressErl = await AddressEditList.GetAddressEditList();
            var listCount = addressErl.Count;
            var addressToDelete = addressErl.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = addressErl.Remove(addressToDelete); 

            var addressListAfterDelete = await addressErl.SaveAsync();

            Assert.NotNull(addressListAfterDelete);
            Assert.IsType<AddressEditList>(addressListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,addressListAfterDelete.Count);
        }

        [Fact]
        private async void AddressEditList_TestUpdateEMailsEntry()
        {
            const int idToUpdate = 1;
            
            var addressEditList = await AddressEditList.GetAddressEditList();
            var countBeforeUpdate = addressEditList.Count;
            var addressToUpdate = addressEditList.First(a => a.Id == idToUpdate);
            addressToUpdate.Notes = "This was updated";

            var updatedList = await addressEditList.SaveAsync();
            
            Assert.Equal("This was updated",updatedList.First(a => a.Id == idToUpdate).Notes);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void AddressEditList_TestAddEMailsEntry()
        {
            var addressEditList = await AddressEditList.GetAddressEditList();
            var countBeforeAdd = addressEditList.Count;
            
            var addressToAdd = addressEditList.AddNew();
            BuildValidAddress(addressToAdd);

            var updatedEMailsList = await addressEditList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedEMailsList.Count);
        }

        private void BuildValidAddress(AddressEdit address)
        {
            address.Address1 = "8365 Gildersleeve Lane";
            address.Address2 = "Unit 103";
            address.City = "Kirtland";
            address.State = "OH";
            address.PostCode = "44094";
            address.Notes = "address notes";
            address.LastUpdatedBy = "edm";
            address.LastUpdatedDate = DateTime.Now;
        }
        
 
    }
}
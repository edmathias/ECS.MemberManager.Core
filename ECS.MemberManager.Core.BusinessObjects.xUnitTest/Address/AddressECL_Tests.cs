using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class AddressECL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public AddressECL_Tests()
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
        private async void AddressECL_TestNewAddressECL()
        {
            var addressErl = await AddressECL.NewAddressECL();

            Assert.NotNull(addressErl);
            Assert.IsType<AddressECL>(addressErl);
        }
        
        [Fact]
        private async void AddressECL_TestGetAddressECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
            var addresses = await dal.Fetch();

            var addressECL = await AddressECL.GetAddressECL(addresses);

            Assert.NotNull(addressECL);
            Assert.Equal(3, addressECL.Count);
        }
        
        [Fact]
        private async void AddressECL_TestDeleteAddressEditChildEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
            var addresses = await dal.Fetch();

            var addressErl = await AddressECL.GetAddressECL(addresses);
            var listCount = addressErl.Count;
            var addressToDelete = addressErl.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = addressErl.Remove(addressToDelete); 

            var addressListAfterDelete = await addressErl.SaveAsync();

            Assert.NotNull(addressListAfterDelete);
            Assert.IsType<AddressECL>(addressListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,addressListAfterDelete.Count);
        }

        [Fact]
        private async void AddressECL_TestUpdateAddressEditChildEntry()
        {
            const int idToUpdate = 1;
            
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
            var addresses = await dal.Fetch();
            
            var addressECL = await AddressECL.GetAddressECL(addresses);
            var countBeforeUpdate = addressECL.Count;
            var addressToUpdate = addressECL.First(a => a.Id == idToUpdate);
            addressToUpdate.Notes = "This was updated";

            var updatedList = await addressECL.SaveAsync();
            
            Assert.Equal("This was updated",updatedList.First(a => a.Id == idToUpdate).Notes);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void AddressECL_TestAddAddressEditChildEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
            var addresses = await dal.Fetch();

            var addressECL = await AddressECL.GetAddressECL(addresses);
            var countBeforeAdd = addressECL.Count;
            
            var addressToAdd = addressECL.AddNew();
            BuildValidAddress(addressToAdd);

            var updatedList = await addressECL.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedList.Count);
        }

        private void BuildValidAddress(AddressEC address)
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
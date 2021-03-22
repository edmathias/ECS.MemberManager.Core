using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.EF;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class AddressERL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public AddressERL_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");
            
            if(testLibrary == "Mock")
                MockDb.ResetMockDb();
            else if(testLibrary == "ADO")
            {
                if (!IsDatabaseBuilt)
                {
                    var adoDb = new ADODb();
                    adoDb.BuildMemberManagerADODb();
                    IsDatabaseBuilt = true;
                }
            }
            else if (testLibrary == "EF")
            {
                if (!IsDatabaseBuilt)
                {
                    var efDb = new EFDb();
                    efDb.BuildMemberManagerEFDb();
                    IsDatabaseBuilt = true;
                }                
            }
        }

        [Fact]
        private async void AddressERL_TestNewAddressERL()
        {
            var addressErl = await AddressERL.NewAddressERL();

            Assert.NotNull(addressErl);
            Assert.IsType<AddressERL>(addressErl);
        }
        
        [Fact]
        private async void AddressERL_TestGetAddressERL()
        {
            var addressERL = await AddressERL.GetAddressERL();

            Assert.NotNull(addressERL);
            Assert.Equal(3, addressERL.Count);
        }
        
        [Fact]
        private async void AddressERL_TestDeleteAddressEditChildEntry()
        {
            var addressErl = await AddressERL.GetAddressERL();
            var listCount = addressErl.Count;
            var addressToDelete = addressErl.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = addressErl.Remove(addressToDelete); 

            var addressListAfterDelete = await addressErl.SaveAsync();

            Assert.NotNull(addressListAfterDelete);
            Assert.IsType<AddressERL>(addressListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,addressListAfterDelete.Count);
        }

        [Fact]
        private async void AddressERL_TestUpdateAddressEditChildEntry()
        {
            const int idToUpdate = 1;
            
            var addressERL = await AddressERL.GetAddressERL();
            var countBeforeUpdate = addressERL.Count;
            var addressToUpdate = addressERL.First(a => a.Id == idToUpdate);
            addressToUpdate.Notes = "This was updated";

            var updatedList = await addressERL.SaveAsync();
            
            Assert.Equal("This was updated",updatedList.First(a => a.Id == idToUpdate).Notes);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void AddressERL_TestAddAddressEditChildEntry()
        {
            var addressERL = await AddressERL.GetAddressERL();
            var countBeforeAdd = addressERL.Count;
            
            var addressToAdd = addressERL.AddNew();
            BuildValidAddress(addressToAdd);

            var updatedList = await addressERL.SaveAsync();
            
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
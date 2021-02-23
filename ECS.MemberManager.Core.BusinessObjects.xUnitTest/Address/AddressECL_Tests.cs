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
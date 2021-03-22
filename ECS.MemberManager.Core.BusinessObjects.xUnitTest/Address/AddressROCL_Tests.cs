using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class AddressROCL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public AddressROCL_Tests()
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
        private async void AddressROCL_TestGetAddressROCL()
        {
            var addresses = BuildAddress();
            
            var addressTypeInfoList = await AddressROCL.GetAddressROCL(addresses);
            
            Assert.NotNull(addressTypeInfoList);
            Assert.True(addressTypeInfoList.IsReadOnly);
            Assert.Equal(3, addressTypeInfoList.Count);
        }

        private List<Address> BuildAddress()
        {
            List <Address> addresses = new List<Address>()
            {
                new Address()
                {
                    Id = 1, Address1 = "8321 Oxford Drive", Address2 = "Apt 103", City = "Greendale",
                    State = "WI", PostCode = "53129", LastUpdatedBy = "edm", Notes = "some notes",
                    LastUpdatedDate = DateTime.Now,
                    Organizations = new List<Organization>()
                    {
                        new Organization()
                        {
                            Id = 1,
                        }
                    },
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
            new Address()
            {
                Id = 2, Address1 = "921 S. Brittany Way", City = "Englewood",
                State = "CO", PostCode = "80112", LastUpdatedBy = "edm", Notes = "notes",
                LastUpdatedDate = DateTime.Now,
                Organizations = new List<Organization>()
                {
                    new Organization()
                    {
                        Id = 2
                    }
                },
                RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
            },
            // use this record for delete only otherwise xunit will contend
            new Address()
            {
                Id = 99, Address1 = "921 Delete St.", City = "Kirtland",
                State = "OH", PostCode = "44094", LastUpdatedBy = "edm", Notes = "more notes",
                LastUpdatedDate = DateTime.Now,
                Organizations = new List<Organization>()
                {
                    new Organization()
                    {
                        Id = 2
                    }
                },                RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
            }
        };

        return addresses;
        }                
    }
}
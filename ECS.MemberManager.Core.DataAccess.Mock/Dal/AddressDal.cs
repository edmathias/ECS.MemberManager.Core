using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class AddressDal : IAddressDal
    {
        public async Task<Address> Fetch(int id)
        {
            return MockDb.Addresses.FirstOrDefault( a => a.Id == id);
        }

        public async Task<List<Address>> Fetch()
        {
            return MockDb.Addresses.ToList();
        }

        public async Task<Address> Insert( Address address)
        {
            var lastAddress = MockDb.Addresses.ToList().OrderByDescending(a =>a.Id).First();
            address.Id = lastAddress.Id+1;
            MockDb.Addresses.Add(address);
            
            return address;
        }

        public async Task<Address> Update(Address address)
        {
            // in memory database already updated
            return address;
        }

        public async Task Delete(int id)
        {
            var addressToDelete = MockDb.Addresses.FirstOrDefault(a => a.Id == id);
            var listIndex = MockDb.Addresses.IndexOf(addressToDelete);
            if(listIndex > -1)
                MockDb.Addresses.RemoveAt(listIndex);
        }

        public void Dispose()
        {
        }
    }
}
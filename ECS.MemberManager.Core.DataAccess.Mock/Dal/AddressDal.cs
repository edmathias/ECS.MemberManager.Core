using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class AddressDal : IDal<Address>
    {
        public Task<Address> Fetch(int id)
        {
            return Task.FromResult(MockDb.Addresses.FirstOrDefault(a => a.Id == id));
        }

        public Task<List<Address>> Fetch()
        {
            return Task.FromResult(MockDb.Addresses.ToList());
        }

        public Task<Address> Insert(Address address)
        {
            var lastAddress = MockDb.Addresses.ToList().OrderByDescending(a => a.Id).First();
            address.Id = lastAddress.Id + 1;
            address.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.Addresses.Add(address);

            return Task.FromResult(address);
        }

        public Task<Address> Update(Address address)
        {
            var savedAddress =
                MockDb.Addresses.FirstOrDefault(em => em.Id == address.Id &&
                                                      em.RowVersion.SequenceEqual(address.RowVersion));

            if (savedAddress == null)
                throw new Csla.DataPortalException(null);

            savedAddress.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(savedAddress);
        }

        public Task Delete(int id)
        {
            var addressToDelete = MockDb.Addresses.FirstOrDefault(a => a.Id == id);
            var listIndex = MockDb.Addresses.IndexOf(addressToDelete);
            if (listIndex > -1)
                MockDb.Addresses.RemoveAt(listIndex);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
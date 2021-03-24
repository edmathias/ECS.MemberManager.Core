using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IAddressDal : IDisposable
    {
        Task<Address> Fetch(int id);
        Task<List<Address>> Fetch();
        Task<Address> Insert(Address address);
        Task<Address> Update(Address address);
        Task Delete(int id);
    }
}
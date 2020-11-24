using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IAddressDal : IDisposable
    {
        Address Fetch(int id);
        List<Address> Fetch();
        int Insert(Address address);
        void Update(Address address );
        void Delete(int id);
    }
}
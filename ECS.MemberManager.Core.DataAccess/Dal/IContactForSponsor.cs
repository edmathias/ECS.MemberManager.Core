using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IContactForSponsorDal : IDisposable
    {
        ContactForSponsor Fetch(int id);
        List<ContactForSponsor> Fetch();
        int Insert(ContactForSponsor address);
        void Update(ContactForSponsor address );
        void Delete(int id);
    }
}
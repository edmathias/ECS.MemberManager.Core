using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IContactForSponsorDal : IDisposable
    {
        Task<List<ContactForSponsor>> Fetch();
        Task<ContactForSponsor> Fetch(int id);
        Task<ContactForSponsor> Insert(ContactForSponsor eMailToInsert);
        Task<ContactForSponsor> Update(ContactForSponsor eMailToUpdate);
        Task Delete(int id);
    }
}
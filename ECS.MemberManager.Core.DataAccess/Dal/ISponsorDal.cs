using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface ISponsorDal : IDisposable
    {
        Task<Sponsor> Fetch(int id);
        Task<List<Sponsor>> Fetch();
        Task<Sponsor> Insert(Sponsor person);
        Task<Sponsor> Update(Sponsor person);
        Task Delete(int id);
    }
}
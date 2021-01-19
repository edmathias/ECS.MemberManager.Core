using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IMemberStatusDal : IDisposable
    {
        Task<MemberStatus> Fetch(int id);
        Task<List<MemberStatus>> Fetch();
        Task<MemberStatus> Insert(MemberStatus memberStatus);
        Task<MemberStatus> Update(MemberStatus memberStatus );
        Task Delete(int id);
    }
}
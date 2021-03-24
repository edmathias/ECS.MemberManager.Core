using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IMembershipTypeDal : IDisposable
    {
        Task<MembershipType> Fetch(int id);
        Task<List<MembershipType>> Fetch();
        Task<MembershipType> Insert(MembershipType documentType);
        Task<MembershipType> Update(MembershipType documentType);
        Task Delete(int id);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IPhoneDal : IDisposable
    {
        Task<Phone> Fetch(int id);
        Task<List<Phone>> Fetch();
        Task<Phone> Insert(Phone documentType);
        Task<Phone> Update(Phone documentType );
        Task Delete(int id);
    }
}
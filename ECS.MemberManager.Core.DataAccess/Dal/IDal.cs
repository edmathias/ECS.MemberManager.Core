using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IDal<T> : IDisposable
    {
        Task<T> Fetch(int id);
        Task<List<T>> Fetch();
        Task<T> Insert(T objectToInsert);
        Task<T> Update(T objectToUpdate);
        Task Delete(int id);
    }
}
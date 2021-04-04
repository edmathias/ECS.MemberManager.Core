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
        Task<T> Insert(T item);
        Task<T> Update(T item);
        Task Delete(int id);
    }
}
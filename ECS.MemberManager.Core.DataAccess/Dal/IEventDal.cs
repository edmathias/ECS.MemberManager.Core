using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IEventDal : IDisposable
    {
        Task<List<Event>> Fetch();
        Task<Event> Fetch(int id);
        Task<Event> Insert(Event eventToInsert);
        Task<Event> Update(Event eventToUpdate);
        Task Delete(int id);
    }
}
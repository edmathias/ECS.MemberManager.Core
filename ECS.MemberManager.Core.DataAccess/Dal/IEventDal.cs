using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IEventDal : IDisposable
    {
        List<Event> Fetch();
        Event Fetch(int id);
        int Insert(Event eventToInsert);
        void Update(Event eventToUpdate);
        void Delete(int id);
    }
}
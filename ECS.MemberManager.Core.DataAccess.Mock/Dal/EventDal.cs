using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class EventDal : IDal<Event>
    {
        public void Dispose()
        {
        }

        public Task<Event> Fetch(int id)
        {
            return Task.FromResult(MockDb.Events.FirstOrDefault(ms => ms.Id == id));
        }

        public Task<List<Event>> Fetch()
        {
            return Task.FromResult(MockDb.Events.ToList());
        }

        public Task<Event> Insert(Event eventToInsert)
        {
            var lastEvent = MockDb.Events.ToList().OrderByDescending(e => e.Id).First();
            eventToInsert.Id = 1 + lastEvent.Id;
            eventToInsert.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.Events.Add(eventToInsert);

            return Task.FromResult(eventToInsert);
        }

        public Task<Event> Update(Event eventUpdate)
        {
            var eventToUpdate =
                MockDb.Events.FirstOrDefault(em => em.Id == eventUpdate.Id &&
                                                   em.RowVersion.SequenceEqual(eventUpdate.RowVersion));

            if (eventToUpdate == null)
                throw new Csla.DataPortalException(null);

            eventToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(eventToUpdate);
        }

        public Task Delete(int id)
        {
            var eventToDelete = MockDb.Events.FirstOrDefault(e => e.Id == id);
            var listIndex = MockDb.Events.IndexOf(eventToDelete);
            if (listIndex > -1)
                MockDb.Events.RemoveAt(listIndex);
            
            return Task.CompletedTask;
        }
    }
}
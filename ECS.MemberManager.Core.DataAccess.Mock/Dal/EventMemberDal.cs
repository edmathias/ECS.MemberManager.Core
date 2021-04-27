using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class EventMemberDal : IDal<EventMember>
    {
        public void Dispose()
        {
        }

        public Task<EventMember> Fetch(int id)
        {
            return Task.FromResult(MockDb.EventMembers.FirstOrDefault(ms => ms.Id == id));
        }

        public Task<List<EventMember>> Fetch()
        {
            return Task.FromResult(MockDb.EventMembers.ToList());
        }

        public Task<EventMember> Insert(EventMember eventToInsert)
        {
            var lastEventMember = MockDb.EventMembers.ToList().OrderByDescending(e => e.Id).First();
            eventToInsert.Id = 1 + lastEventMember.Id;
            eventToInsert.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.EventMembers.Add(eventToInsert);

            return Task.FromResult(eventToInsert);
        }

        public Task<EventMember> Update(EventMember eventUpdate)
        {
            var eventToUpdate =
                MockDb.EventMembers.FirstOrDefault(em => em.Id == eventUpdate.Id);

            if (eventToUpdate == null)
                throw new Csla.DataPortalException(null);
            
            eventToUpdate.Event = eventUpdate.Event;
            eventToUpdate.Notes = eventUpdate.Notes;
            eventToUpdate.Role = eventUpdate.Role;
            eventToUpdate.MemberInfo = eventUpdate.MemberInfo;
            eventToUpdate.LastUpdatedBy = eventUpdate.LastUpdatedBy;
            eventToUpdate.LastUpdatedDate = eventUpdate.LastUpdatedDate;
            eventToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            
            return Task.FromResult(eventToUpdate);
        }

        public Task Delete(int id)
        {
            var eventToDelete = MockDb.EventMembers.FirstOrDefault(e => e.Id == id);
            var listIndex = MockDb.EventMembers.IndexOf(eventToDelete);
            if (listIndex > -1)
                MockDb.EventMembers.RemoveAt(listIndex);
            
            return Task.CompletedTask;
        }
    }
}
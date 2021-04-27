using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class PersonalNoteDal : IDal<PersonalNote>
    {
        public void Dispose()
        {
        }

        public Task<PersonalNote> Fetch(int id)
        {
            return Task.FromResult(MockDb.PersonalNotes.FirstOrDefault(ms => ms.Id == id));
        }

        public Task<List<PersonalNote>> Fetch()
        {
            return Task.FromResult(MockDb.PersonalNotes.ToList());
        }

        public Task<PersonalNote> Insert(PersonalNote eventToInsert)
        {
            var lastPersonalNote = MockDb.PersonalNotes.ToList().OrderByDescending(e => e.Id).First();
            eventToInsert.Id = 1 + lastPersonalNote.Id;
            eventToInsert.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.PersonalNotes.Add(eventToInsert);

            return Task.FromResult(eventToInsert);
        }

        public Task<PersonalNote> Update(PersonalNote eventUpdate)
        {
            var eventToUpdate =
                MockDb.PersonalNotes.FirstOrDefault(em => em.Id == eventUpdate.Id );

            if (eventToUpdate == null)
                throw new Csla.DataPortalException(null);

            eventToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(eventToUpdate);
        }

        public Task Delete(int id)
        {
            var eventToDelete = MockDb.PersonalNotes.FirstOrDefault(e => e.Id == id);
            var listIndex = MockDb.PersonalNotes.IndexOf(eventToDelete);
            if (listIndex > -1)
                MockDb.PersonalNotes.RemoveAt(listIndex);
            
            return Task.CompletedTask;
        }
    }
}
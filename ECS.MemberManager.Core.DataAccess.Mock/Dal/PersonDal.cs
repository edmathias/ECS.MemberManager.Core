using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class PersonDal : IDal<Person>
    {
        public Task<Person> Fetch(int id)
        {
            return Task.FromResult(MockDb.Persons.FirstOrDefault(p => p.Id == id));
        }

        public Task<List<Person>> Fetch()
        {
            return Task.FromResult(MockDb.Persons.ToList());
        }

        public Task<Person> Insert(Person person)
        {
            var lastPerson = MockDb.Persons.ToList().OrderByDescending(p => p.Id).First();
            person.Id = 1 + lastPerson.Id;
            person.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.Persons.Add(person);

            return Task.FromResult(person);
        }

        public Task<Person> Update(Person person)
        {
            var personToUpdate =
                MockDb.Persons.FirstOrDefault(em => em.Id == person.Id);

            if (personToUpdate == null)
                throw new Csla.DataPortalException(null);

            personToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(personToUpdate);
        }

        public Task Delete(int id)
        {
            var personToDelete = MockDb.Persons.FirstOrDefault(p => p.Id == id);
            var listIndex = MockDb.Persons.IndexOf(personToDelete);
            if (listIndex > -1)
                MockDb.Persons.RemoveAt(listIndex);
            
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
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
        public async Task<Person> Fetch(int id)
        {
            return MockDb.Persons.FirstOrDefault(p => p.Id == id);
        }

        public async Task<List<Person>> Fetch()
        {
            return MockDb.Persons.ToList();
        }

        public async Task<Person> Insert(Person person)
        {
            var lastPerson = MockDb.Persons.ToList().OrderByDescending(p => p.Id).First();
            person.Id = 1 + lastPerson.Id;
            person.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.Persons.Add(person);

            return person;
        }

        public async Task<Person> Update(Person person)
        {
            var personToUpdate =
                MockDb.Persons.FirstOrDefault(em => em.Id == person.Id &&
                                                    em.RowVersion.SequenceEqual(person.RowVersion));

            if (personToUpdate == null)
                throw new Csla.DataPortalException(null);

            personToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return personToUpdate;
        }

        public async Task Delete(int id)
        {
            var personToDelete = MockDb.Persons.FirstOrDefault(p => p.Id == id);
            var listIndex = MockDb.Persons.IndexOf(personToDelete);
            if (listIndex > -1)
                MockDb.Persons.RemoveAt(listIndex);
        }

        public void Dispose()
        {
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class PersonDal : IPersonDal
    {
        public Person Fetch(int id)
        {
            return MockDb.Persons.FirstOrDefault(p => p.Id == id);
        }

        public List<Person> Fetch()
        {
            return MockDb.Persons.ToList();
        }

        public int Insert( Person person)
        {
            var lastPerson = MockDb.Persons.ToList().OrderByDescending(p => p.Id).First();
            person.Id = ++lastPerson.Id;
            MockDb.Persons.Add(person);
            
            return person.Id;
        }

        public void Update(Person person)
        {
            // mockdb in memory list reference already updated 
        }

        public void Delete(int id)
        {
            var personToDelete = MockDb.Persons.FirstOrDefault(p => p.Id == id);
            var listIndex = MockDb.Persons.IndexOf(personToDelete);
            if(listIndex > -1)
                MockDb.Persons.RemoveAt(listIndex);
        }

        public void Dispose()
        {
        }
    }
}
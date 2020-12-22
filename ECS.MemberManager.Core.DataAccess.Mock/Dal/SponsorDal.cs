using System.Collections.Generic;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class SponsorDal : ISponsorDal
    {
        public void Dispose()
        {
        }

        public Sponsor Fetch(int id)
        {
            return MockDb.Sponsors.FirstOrDefault(p => p.Id == id);
        }

        public List<Sponsor> Fetch()
        {
            return MockDb.Sponsors.ToList();
        }

        public int Insert(Sponsor person)
        {
            var lastSponsor = MockDb.Sponsors.ToList().OrderByDescending(p => p.Id).First();
            person.Id = 1+lastSponsor.Id;
            MockDb.Sponsors.Add(person);
            
            return person.Id;
        }

        public void Update(Sponsor person)
        {
            // mockdb in memory list reference already updated 
        }

        public void Delete(int id)
        {
            var personToDelete = MockDb.Sponsors.First(p => p.Id == id);
            var listIndex = MockDb.Sponsors.IndexOf(personToDelete);
            if(listIndex > -1)
                MockDb.Sponsors.RemoveAt(listIndex);
        }
    }
}
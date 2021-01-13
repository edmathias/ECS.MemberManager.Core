using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class SponsorDal : ISponsorDal
    {
        public void Dispose()
        {
        }

        public async Task<Sponsor> Fetch(int id)
        {
            return MockDb.Sponsors.FirstOrDefault(p => p.Id == id);
        }

        public async Task<List<Sponsor>> Fetch()
        {
            return MockDb.Sponsors.ToList();
        }

        public async Task<Sponsor> Insert(Sponsor sponsor)
        {
            var lastSponsor = MockDb.Sponsors.ToList().OrderByDescending(p => p.Id).First();
            sponsor.Id = 1+lastSponsor.Id;
            MockDb.Sponsors.Add(sponsor);
            
            return sponsor;
        }

        public async Task<Sponsor> Update(Sponsor sponsor)
        {
            // mockdb in memory list reference already updated 
            return sponsor;
        }

        public async Task Delete(int id)
        {
            var sponsorToDelete = MockDb.Sponsors.First(p => p.Id == id);
            var listIndex = MockDb.Sponsors.IndexOf(sponsorToDelete);
            if(listIndex > -1)
                MockDb.Sponsors.RemoveAt(listIndex);
        }
    }
}
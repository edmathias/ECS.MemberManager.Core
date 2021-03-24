using System;
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
            sponsor.Id = 1 + lastSponsor.Id;
            sponsor.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.Sponsors.Add(sponsor);

            return sponsor;
        }

        public async Task<Sponsor> Update(Sponsor sponsor)
        {
            var sponsorToUpdate =
                MockDb.Sponsors.FirstOrDefault(em => em.Id == sponsor.Id &&
                                                     em.RowVersion.SequenceEqual(sponsor.RowVersion));

            if (sponsorToUpdate == null)
                throw new Csla.DataPortalException(null);

            sponsorToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return sponsorToUpdate;
        }

        public async Task Delete(int id)
        {
            var sponsorToDelete = MockDb.Sponsors.First(p => p.Id == id);
            var listIndex = MockDb.Sponsors.IndexOf(sponsorToDelete);
            if (listIndex > -1)
                MockDb.Sponsors.RemoveAt(listIndex);
        }
    }
}
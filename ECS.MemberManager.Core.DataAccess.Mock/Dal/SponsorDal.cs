using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class SponsorDal : IDal<Sponsor>
    {
        public void Dispose()
        {
        }

        public Task<Sponsor> Fetch(int id)
        {
            return Task.FromResult(MockDb.Sponsors.FirstOrDefault(p => p.Id == id));
        }

        public Task<List<Sponsor>> Fetch()
        {
            return Task.FromResult(MockDb.Sponsors.ToList());
        }

        public Task<Sponsor> Insert(Sponsor sponsor)
        {
            var lastSponsor = MockDb.Sponsors.ToList().OrderByDescending(p => p.Id).First();
            sponsor.Id = 1 + lastSponsor.Id;
            sponsor.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.Sponsors.Add(sponsor);

            return Task.FromResult(sponsor);
        }

        public Task<Sponsor> Update(Sponsor sponsor)
        {
            var sponsorToUpdate =
                MockDb.Sponsors.FirstOrDefault(em => em.Id == sponsor.Id); 

            if (sponsorToUpdate == null)
                throw new Csla.DataPortalException(null);

            sponsorToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(sponsorToUpdate);
        }

        public Task Delete(int id)
        {
            var sponsorToDelete = MockDb.Sponsors.First(p => p.Id == id);
            var listIndex = MockDb.Sponsors.IndexOf(sponsorToDelete);
            if (listIndex > -1)
                MockDb.Sponsors.RemoveAt(listIndex);
            
            return Task.CompletedTask;
        }
    }
}
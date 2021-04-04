using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class SponsorDal : ISponsorDal
    {
        public async Task<List<Sponsor>> Fetch()
        {
            List<Sponsor> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.Sponsors
                    .Include(s => s.Person)
                    .Include(s => s.Organization)
                    .ToListAsync();
            }

            return list;
        }

        public async Task<Sponsor> Fetch(int id)
        {
            Sponsor sponsor = null;

            using (var context = new MembershipManagerDataContext())
            {
                sponsor = await context.Sponsors.Where(s => s.Id == id)
                    .Include(s => s.Person)
                    .Include(s => s.Organization)
                    .FirstAsync();
            }

            return sponsor;
        }

        public async Task<Sponsor> Insert(Sponsor sponsorToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(sponsorToInsert.Person).State = EntityState.Unchanged;
                context.Entry(sponsorToInsert.Organization).State = EntityState.Unchanged;
                
                await context.Sponsors.AddAsync(sponsorToInsert);

                var result = await context.SaveChangesAsync();
                
                if (result != 1)
                    throw new ApplicationException("Error inserting Sponsor");
            }

            return sponsorToInsert;
        }

        public async Task<Sponsor> Update(Sponsor sponsorToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(sponsorToUpdate.Person).State = EntityState.Unchanged;
                context.Entry(sponsorToUpdate.Organization).State = EntityState.Unchanged;
                
                context.Update(sponsorToUpdate);

                var result = await context.SaveChangesAsync();
                
                if (result != 1)
                    throw new ApplicationException("Error updating Sponsor");
                
            }

            return sponsorToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.Sponsors.SingleAsync(a => a.Id == id));
                var result = await context.SaveChangesAsync();
                
                if (result != 1)
                    throw new ApplicationException("Error updating Sponsor");
            }
        }

        public void Dispose()
        {
        }
    }
}
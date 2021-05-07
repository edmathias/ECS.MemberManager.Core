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
    public class SponsorDal : IDal<Sponsor>
    {
        private MembershipManagerDataContext _context;

        public SponsorDal()
        {
            _context = new MembershipManagerDataContext();
        }

        public SponsorDal(MembershipManagerDataContext context)
        {
            _context = context;
        }

        public async Task<List<Sponsor>> Fetch()
        {
            return await _context.Sponsors
                .Include(s => s.Person)
                .Include(s => s.Organization)
                .ToListAsync();
        }

        public async Task<Sponsor> Fetch(int id)
        {
            return await _context.Sponsors.Where(s => s.Id == id)
                .Include(s => s.Person)
                .Include(s => s.Organization)
                .FirstAsync();
        }

        public async Task<Sponsor> Insert(Sponsor sponsorToInsert)
        {
            _context.Entry(sponsorToInsert.Person).State = EntityState.Unchanged;
            _context.Entry(sponsorToInsert.Organization).State = EntityState.Unchanged;

            await _context.Sponsors.AddAsync(sponsorToInsert);

            var result = await _context.SaveChangesAsync();

            if (result != 1)
                throw new ApplicationException("Error inserting Sponsor");

            return sponsorToInsert;
        }

        public async Task<Sponsor> Update(Sponsor sponsorToUpdate)
        {
            _context.Entry(sponsorToUpdate.Person).State = EntityState.Unchanged;
            _context.Entry(sponsorToUpdate.Organization).State = EntityState.Unchanged;

            _context.Update(sponsorToUpdate);

            var result = await _context.SaveChangesAsync();

            if (result != 1)
                throw new ApplicationException("Error updating Sponsor");

            return sponsorToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.Sponsors.SingleAsync(a => a.Id == id));
            var result = await _context.SaveChangesAsync();

            if (result != 1)
                throw new ApplicationException("Error updating Sponsor");
        }

        public void Dispose()
        {
        }
    }
}
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
    public class ContactForSponsorDal : IDal<ContactForSponsor>
    {
        private MembershipManagerDataContext _context;

        public ContactForSponsorDal()
        {
            _context = new MembershipManagerDataContext();
        }

        public ContactForSponsorDal(MembershipManagerDataContext context)
        {
            _context = context;
        }

        public async Task<List<ContactForSponsor>> Fetch()
        {
            return await _context.ContactForSponsors
                .Include(p => p.Person)
                .ThenInclude(t => t.Title)
                .Include(p => p.Person)
                .ThenInclude(e => e.EMail)
                .Include(s => s.Sponsor)
                .ThenInclude(p => p.Person)
                .Include(s => s.Sponsor)
                .ThenInclude(s => s.Organization)
                .ToListAsync();
        }

        public async Task<ContactForSponsor> Fetch(int id)
        {
            return await _context.ContactForSponsors
                .Where(c => c.Id == id)
                .Include(p => p.Person)
                .ThenInclude(t => t.Title)
                .Include(p => p.Person)
                .ThenInclude(e => e.EMail)
                .Include(s => s.Sponsor)
                .ThenInclude(p => p.Person)
                .Include(s => s.Sponsor)
                .ThenInclude(s => s.Organization)
                .FirstAsync();
        }

        public async Task<ContactForSponsor> Insert(ContactForSponsor contactToInsert)
        {
            _context.Entry(contactToInsert.Person).State = EntityState.Unchanged;
            _context.Entry(contactToInsert.Sponsor).State = EntityState.Unchanged;

            await _context.ContactForSponsors.AddAsync(contactToInsert);

            var result = await _context.SaveChangesAsync();

            if (result != 1)
                throw new ApplicationException("Error updating ContactForSponsor");

            return contactToInsert;
        }

        public async Task<ContactForSponsor> Update(ContactForSponsor contactToUpdate)
        {
            _context.Entry(contactToUpdate.Person).State = EntityState.Unchanged;
            _context.Entry(contactToUpdate.Sponsor).State = EntityState.Unchanged;

            _context.Update(contactToUpdate);

            var result = await _context.SaveChangesAsync();
            if (result != 1)
                throw new ApplicationException("Error updating ContactForSponsor");

            return contactToUpdate;
        }

        public async Task Delete(int id)
        {
            var contact = await _context.ContactForSponsors.SingleAsync(a => a.Id == id);
            _context.Remove(contact);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
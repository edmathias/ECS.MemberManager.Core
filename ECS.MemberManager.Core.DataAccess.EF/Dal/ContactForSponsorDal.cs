﻿using System;
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
        public async Task<List<ContactForSponsor>> Fetch()
        {
            List<ContactForSponsor> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.ContactForSponsors
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

            return list;
        }

        public async Task<ContactForSponsor> Fetch(int id)
        {
            ContactForSponsor contact = null;

            using (var context = new MembershipManagerDataContext())
            {
                contact = await context.ContactForSponsors
                    .Where (c => c.Id == id)
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

            return contact;
        }

        public async Task<ContactForSponsor> Insert(ContactForSponsor contactToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(contactToInsert.Person).State = EntityState.Unchanged;
                context.Entry(contactToInsert.Sponsor).State = EntityState.Unchanged;

                await context.ContactForSponsors.AddAsync(contactToInsert);

                var result = await context.SaveChangesAsync();

                if (result != 1)
                    throw new ApplicationException("Error updating ContactForSponsor");
            }

            return contactToInsert;
        }

        public async Task<ContactForSponsor> Update(ContactForSponsor contactToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(contactToUpdate.Person).State = EntityState.Unchanged;
                context.Entry(contactToUpdate.Sponsor).State = EntityState.Unchanged;

                context.Update(contactToUpdate);

                var result = await context.SaveChangesAsync();
                if (result != 1)
                    throw new ApplicationException("Error updating ContactForSponsor");
                
            }

            return contactToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                var contact = await context.ContactForSponsors.SingleAsync(a => a.Id == id); 
                context.Remove(contact);
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}
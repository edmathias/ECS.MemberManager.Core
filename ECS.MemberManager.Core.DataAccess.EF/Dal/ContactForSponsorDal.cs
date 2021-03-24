using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class ContactForSponsorDal : IContactForSponsorDal
    {
        public async Task<List<ContactForSponsor>> Fetch()
        {
            List<ContactForSponsor> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.ContactForSponsors
                    .Include(a => a.Person)
                    .Include(a => a.Sponsor)
                    .ToListAsync();
            }

            return list;
        }

        public async Task<ContactForSponsor> Fetch(int id)
        {
            List<ContactForSponsor> list = null;

            ContactForSponsor contact = null;

            using (var context = new MembershipManagerDataContext())
            {
                contact = await context.ContactForSponsors.Where(a => a.Id == id)
                    .Include(a => a.Person)
                    .Include(a => a.Sponsor)
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

                await context.SaveChangesAsync();
            }

            ;

            return contactToInsert;
        }

        public async Task<ContactForSponsor> Update(ContactForSponsor contactToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(contactToUpdate.Person).State = EntityState.Unchanged;
                context.Entry(contactToUpdate.Sponsor).State = EntityState.Unchanged;

                context.Update(contactToUpdate);

                await context.SaveChangesAsync();
            }

            return contactToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.ContactForSponsors.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}
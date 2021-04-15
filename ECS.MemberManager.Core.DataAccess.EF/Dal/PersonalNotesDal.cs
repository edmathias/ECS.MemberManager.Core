using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class PersonalNoteDal : IDal<PersonalNote>
    {
        public async Task<List<PersonalNote>> Fetch()
        {
            List<PersonalNote> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.PersonalNotes
                    .Include(e => e.Person)
                    .ToListAsync();
            }

            return list;
        }

        public async Task<PersonalNote> Fetch(int id)
        {
            PersonalNote payment = null;

            using (var context = new MembershipManagerDataContext())
            {
                payment = await context.PersonalNotes.Where(a => a.Id == id)
                    .Include(e => e.Person)
                    .FirstAsync();
            }

            return payment;
        }

        public async Task<PersonalNote> Insert(PersonalNote paymentToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(paymentToInsert.Person).State = EntityState.Unchanged;

                await context.PersonalNotes.AddAsync(paymentToInsert);

                await context.SaveChangesAsync();
            }

            return paymentToInsert;
        }

        public async Task<PersonalNote> Update(PersonalNote paymentToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(paymentToUpdate.Person).State = EntityState.Unchanged;

                context.Update(paymentToUpdate);

                await context.SaveChangesAsync();
            }

            return paymentToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.PersonalNotes.SingleAsync(a => a.Id == id));
                
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}
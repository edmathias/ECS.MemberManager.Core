using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class EMailDal : IDal<EMail>
    {
        public async Task<List<EMail>> Fetch()
        {
            List<EMail> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.EMails
                    .Include(e => e.EMailType)
                    .ToListAsync();
            }

            return list;
        }

        public async Task<EMail> Fetch(int id)
        {
            EMail contact = null;

            using (var context = new MembershipManagerDataContext())
            {
                contact = await context.EMails.Where(a => a.Id == id)
                    .Include(e => e.EMailType)
                    .FirstAsync();
            }

            return contact;
        }

        public async Task<EMail> Insert(EMail contactToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(contactToInsert.EMailType).State = EntityState.Unchanged;

                await context.EMails.AddAsync(contactToInsert);

                await context.SaveChangesAsync();
            }

            ;

            return contactToInsert;
        }

        public async Task<EMail> Update(EMail contactToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(contactToUpdate.EMailType).State = EntityState.Unchanged;

                context.Update(contactToUpdate);

                await context.SaveChangesAsync();
            }

            return contactToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.EMails.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}
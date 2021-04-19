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
    public class PhoneDal : IDal<Phone> 
    {
        public async Task<List<Phone>> Fetch()
        {
            List<Phone> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.Phones.ToListAsync();
            }

            return list;
        }

        public async Task<Phone> Fetch(int id)
        {
            Phone phone = null;

            using (var context = new MembershipManagerDataContext())
            {
                phone = await context.Phones.Where(a => a.Id == id).FirstAsync();
            }

            return phone;
        }

        public async Task<Phone> Insert(Phone phoneToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                await context.Phones.AddAsync(phoneToInsert);
                await context.SaveChangesAsync();
            }

            return phoneToInsert;
        }

        public async Task<Phone> Update(Phone phoneToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Update(phoneToUpdate);
            
                await context.SaveChangesAsync();
            }

            return phoneToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.Phones.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}
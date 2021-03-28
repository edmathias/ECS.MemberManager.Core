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
    public class MemberInfoDal : IMemberInfoDal
    {
        public async Task<List<MemberInfo>> Fetch()
        {
            List<MemberInfo> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.MemberInfo
                    .Include( mi => mi.Person)
                    .Include( mi => mi.PrivacyLevel)
                    .Include( mi => mi.MemberStatus)
                    .Include( mi => mi.MembershipType)
                    .ToListAsync();
            }

            return list;
        }

        public async Task<MemberInfo> Fetch(int id)
        {
            List<MemberInfo> list = null;

            MemberInfo person = null;

            using (var context = new MembershipManagerDataContext())
            {
                person = await context.MemberInfo.Where(a => a.Id == id)
                    .Include( mi => mi.Person)
                    .Include( mi => mi.PrivacyLevel)
                    .Include( mi => mi.MemberStatus)
                    .Include( mi => mi.MembershipType)
                    .FirstAsync();
            }

            return person;
        }

        public async Task<MemberInfo> Insert(MemberInfo personToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(personToInsert.Person).State = EntityState.Unchanged;
                context.Entry(personToInsert.PrivacyLevel).State = EntityState.Unchanged;
                context.Entry(personToInsert.MemberStatus).State = EntityState.Unchanged;
                context.Entry(personToInsert.MembershipType).State = EntityState.Unchanged;
                await context.MemberInfo.AddAsync(personToInsert);

                await context.SaveChangesAsync();
            }

            return personToInsert;
        }

        public async Task<MemberInfo> Update(MemberInfo personToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(personToUpdate.Person).State = EntityState.Unchanged;
                context.Entry(personToUpdate.PrivacyLevel).State = EntityState.Unchanged;
                context.Entry(personToUpdate.MemberStatus).State = EntityState.Unchanged;
                context.Entry(personToUpdate.MembershipType).State = EntityState.Unchanged;
                
                context.Update(personToUpdate);

                var result = await context.SaveChangesAsync();

                if (result != 1)
                    throw new ApplicationException("MemberInfo domain object was not updated");
            }

            return personToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.MemberInfo.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}
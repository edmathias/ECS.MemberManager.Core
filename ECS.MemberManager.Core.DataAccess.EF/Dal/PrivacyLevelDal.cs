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
    public class PrivacyLevelDal : IDal<PrivacyLevel> 
    {
        public async Task<List<PrivacyLevel>> Fetch()
        {
            List<PrivacyLevel> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.PrivacyLevels.ToListAsync();
            }

            return list;
        }

        public async Task<PrivacyLevel> Fetch(int id)
        {
            PrivacyLevel privacyLevel = null;

            using (var context = new MembershipManagerDataContext())
            {
                privacyLevel = await context.PrivacyLevels.Where(a => a.Id == id).FirstAsync();
            }

            return privacyLevel;
        }

        public async Task<PrivacyLevel> Insert(PrivacyLevel privacyLevelToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                await context.PrivacyLevels.AddAsync(privacyLevelToInsert);
                await context.SaveChangesAsync();
            }

            return privacyLevelToInsert;
        }

        public async Task<PrivacyLevel> Update(PrivacyLevel privacyLevelToUpdate)
        {
            int result = 0;
            
            using (var context = new MembershipManagerDataContext())
            {
                context.Update(privacyLevelToUpdate);
                try
                {
                    result = await context.SaveChangesAsync();
                }
                catch (Exception exc)
                {
                    System.Console.WriteLine(exc.Message);
                }
            }

            return privacyLevelToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.PrivacyLevels.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}
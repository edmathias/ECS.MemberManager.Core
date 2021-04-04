using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class MemberStatusDal : IDal<MemberStatus>
    {
        public async Task<List<MemberStatus>> Fetch()
        {
            List<MemberStatus> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.MemberStatuses
                    .ToListAsync();
            }

            return list;
        }

        public async Task<MemberStatus> Fetch(int id)
        {
            MemberStatus memberStatus = null;

            using (var context = new MembershipManagerDataContext())
            {
                memberStatus = await context.MemberStatuses.Where(a => a.Id == id)
                    .FirstAsync();
            }

            return memberStatus;
        }

        public async Task<MemberStatus> Insert(MemberStatus memberStatusToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                await context.MemberStatuses.AddAsync(memberStatusToInsert);

                await context.SaveChangesAsync();
            }

            return memberStatusToInsert;
        }

        public async Task<MemberStatus> Update(MemberStatus memberStatusToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Update(memberStatusToUpdate);

                await context.SaveChangesAsync();
            }

            return memberStatusToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.MemberStatuses.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}
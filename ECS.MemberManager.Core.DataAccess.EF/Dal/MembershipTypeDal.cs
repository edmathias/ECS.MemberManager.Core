using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class MembershipTypeDal : IDal<MembershipType>
    {
        public async Task<List<MembershipType>> Fetch()
        {
            List<MembershipType> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.MembershipTypes
                    .ToListAsync();
            }

            return list;
        }

        public async Task<MembershipType> Fetch(int id)
        {
            MembershipType membershipType = null;

            using (var context = new MembershipManagerDataContext())
            {
                membershipType = await context.MembershipTypes.Where(a => a.Id == id)
                    .FirstAsync();
            }

            return membershipType;
        }

        public async Task<MembershipType> Insert(MembershipType membershipTypeToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                await context.MembershipTypes.AddAsync(membershipTypeToInsert);

                await context.SaveChangesAsync();
            }

            ;

            return membershipTypeToInsert;
        }

        public async Task<MembershipType> Update(MembershipType membershipTypeToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Update(membershipTypeToUpdate);

                await context.SaveChangesAsync();
            }

            return membershipTypeToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.MembershipTypes.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}
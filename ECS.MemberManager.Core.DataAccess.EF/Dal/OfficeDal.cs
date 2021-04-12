using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class OfficeDal : IDal<Office>
    {
        public async Task<List<Office>> Fetch()
        {
            List<Office> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.Offices
                    .ToListAsync();
            }

            return list;
        }

        public async Task<Office> Fetch(int id)
        {
            Office officeObj = null;

            using (var context = new MembershipManagerDataContext())
            {
                officeObj = await context.Offices.Where(a => a.Id == id)
                    .FirstAsync();
            }

            return officeObj;
        }

        public async Task<Office> Insert(Office officeObjToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                await context.Offices.AddAsync(officeObjToInsert);

                await context.SaveChangesAsync();
            }

            return officeObjToInsert;
        }

        public async Task<Office> Update(Office officeObjToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Update(officeObjToUpdate);

                await context.SaveChangesAsync();
            }

            return officeObjToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.Offices.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}
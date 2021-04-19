using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class OrganizationDal : IDal<Organization>
    {
        public async Task<List<Organization>> Fetch()
        {
            List<Organization> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.Organizations
                    .Include(a => a.OrganizationType)
                    .Include(a => a.CategoryOfOrganization)
                    .ToListAsync();
            }

            return list;
        }

        public async Task<Organization> Fetch(int id)
        {
            Organization organizationObj = null;

            using (var context = new MembershipManagerDataContext())
            {
                organizationObj = await context.Organizations.Where(a => a.Id == id)
                    .Include(a => a.OrganizationType)
                    .Include(a => a.CategoryOfOrganization)
                    .FirstAsync();
            }

            return organizationObj;
        }

        public async Task<Organization> Insert(Organization organizationObjToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(organizationObjToInsert.OrganizationType).State = EntityState.Unchanged;
                context.Entry(organizationObjToInsert.CategoryOfOrganization).State = EntityState.Unchanged;
                
                await context.Organizations.AddAsync(organizationObjToInsert);

                await context.SaveChangesAsync();
            }

            ;

            return organizationObjToInsert;
        }

        public async Task<Organization> Update(Organization organizationObjToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(organizationObjToUpdate.OrganizationType).State = EntityState.Unchanged;
                context.Entry(organizationObjToUpdate.CategoryOfOrganization).State = EntityState.Unchanged;
                context.Update(organizationObjToUpdate);

                await context.SaveChangesAsync();
            }

            return organizationObjToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.Organizations.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}
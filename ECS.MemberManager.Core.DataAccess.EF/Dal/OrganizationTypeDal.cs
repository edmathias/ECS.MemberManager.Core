using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class OrganizationTypeDal : IDal<OrganizationType>
    {
        public async Task<List<OrganizationType>> Fetch()
        {
            List<OrganizationType> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.OrganizationTypes
                    .Include(ot => ot.CategoryOfOrganization)
                    .ToListAsync();
            }

            return list;
        }

        public async Task<OrganizationType> Fetch(int id)
        {
            OrganizationType organizationType = null;

            using (var context = new MembershipManagerDataContext())
            {
                organizationType = await context.OrganizationTypes.Where(a => a.Id == id)
                    .Include(ot => ot.CategoryOfOrganization)
                    .FirstAsync();
            }

            return organizationType;
        }

        public async Task<OrganizationType> Insert(OrganizationType organizationTypeToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(organizationTypeToInsert.CategoryOfOrganization).State = EntityState.Unchanged;
                await context.OrganizationTypes.AddAsync(organizationTypeToInsert);

                await context.SaveChangesAsync();
            }

            return organizationTypeToInsert;
        }

        public async Task<OrganizationType> Update(OrganizationType organizationTypeToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(organizationTypeToUpdate.CategoryOfOrganization).State = EntityState.Unchanged;
                context.Update(organizationTypeToUpdate);

                await context.SaveChangesAsync();
            }

            return organizationTypeToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.OrganizationTypes.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}
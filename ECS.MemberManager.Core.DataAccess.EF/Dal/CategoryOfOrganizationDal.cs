using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class CategoryOfOrganizationDal : ICategoryOfOrganizationDal
    {
        public async Task<List<CategoryOfOrganization>> Fetch()
        {
            List<CategoryOfOrganization> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.CategoryOfOrganizations.ToListAsync();
            }

            return list;
        }

        public async Task<CategoryOfOrganization> Fetch(int id)
        {
            List<CategoryOfOrganization> list = null;

            CategoryOfOrganization category = null;

            using (var context = new MembershipManagerDataContext())
            {
                category = await context.CategoryOfOrganizations.Where(a => a.Id == id).FirstAsync();
            }

            return category;
        }

        public async Task<CategoryOfOrganization> Insert(CategoryOfOrganization categoryToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                await context.CategoryOfOrganizations.AddAsync(categoryToInsert);
                await context.SaveChangesAsync();
            }

            ;

            return categoryToInsert;
        }

        public async Task<CategoryOfOrganization> Update(CategoryOfOrganization categoryToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Update(categoryToUpdate);
                await context.SaveChangesAsync();
            }

            return categoryToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.CategoryOfOrganizations.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}
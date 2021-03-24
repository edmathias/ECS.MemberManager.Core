using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class CategoryOfPersonDal : ICategoryOfPersonDal
    {
        public async Task<List<CategoryOfPerson>> Fetch()
        {
            List<CategoryOfPerson> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.CategoryOfPersons.ToListAsync();
            }

            return list;
        }

        public async Task<CategoryOfPerson> Fetch(int id)
        {
            List<CategoryOfPerson> list = null;

            CategoryOfPerson category = null;

            using (var context = new MembershipManagerDataContext())
            {
                category = await context.CategoryOfPersons.Where(a => a.Id == id).FirstAsync();
            }

            return category;
        }

        public async Task<CategoryOfPerson> Insert(CategoryOfPerson categoryToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                await context.CategoryOfPersons.AddAsync(categoryToInsert);
                await context.SaveChangesAsync();
            }

            ;

            return categoryToInsert;
        }

        public async Task<CategoryOfPerson> Update(CategoryOfPerson categoryToUpdate)
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
                context.Remove(await context.CategoryOfPersons.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}
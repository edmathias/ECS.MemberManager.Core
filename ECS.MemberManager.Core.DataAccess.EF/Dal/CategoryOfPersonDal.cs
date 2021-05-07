using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class CategoryOfPersonDal : IDal<CategoryOfPerson>
    {
        private MembershipManagerDataContext _context;

        public CategoryOfPersonDal()
        {
            _context = new MembershipManagerDataContext();
        }

        public CategoryOfPersonDal(MembershipManagerDataContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryOfPerson>> Fetch()
        {
            return await _context.CategoryOfPersons.ToListAsync();
        }

        public async Task<CategoryOfPerson> Fetch(int id)
        {
            return await _context.CategoryOfPersons.Where(a => a.Id == id).FirstAsync();
        }

        public async Task<CategoryOfPerson> Insert(CategoryOfPerson categoryToInsert)
        {
            await _context.CategoryOfPersons.AddAsync(categoryToInsert);
            await _context.SaveChangesAsync();

            return categoryToInsert;
        }

        public async Task<CategoryOfPerson> Update(CategoryOfPerson categoryToUpdate)
        {
            _context.Update(categoryToUpdate);
            await _context.SaveChangesAsync();

            return categoryToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.CategoryOfPersons.SingleAsync(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
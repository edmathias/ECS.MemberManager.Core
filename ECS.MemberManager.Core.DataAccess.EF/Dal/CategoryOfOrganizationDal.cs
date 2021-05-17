using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class CategoryOfOrganizationDal : IDal<CategoryOfOrganization>
    {
        private MembershipManagerDataContext _context;

        public CategoryOfOrganizationDal() => _context = new MembershipManagerDataContext();

        public CategoryOfOrganizationDal(MembershipManagerDataContext context) => _context = context;

        public async Task<List<CategoryOfOrganization>> Fetch()
        {
            return await _context.CategoryOfOrganizations.ToListAsync();
        }

        public async Task<CategoryOfOrganization> Fetch(int id)
        {
            return await _context.CategoryOfOrganizations.Where(a => a.Id == id).FirstAsync();
        }

        public async Task<CategoryOfOrganization> Insert(CategoryOfOrganization categoryToInsert)
        {
            await _context.CategoryOfOrganizations.AddAsync(categoryToInsert);
            await _context.SaveChangesAsync();

            return categoryToInsert;
        }

        public async Task<CategoryOfOrganization> Update(CategoryOfOrganization categoryToUpdate)
        {
            _context.Update(categoryToUpdate);
            await _context.SaveChangesAsync();

            return categoryToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.CategoryOfOrganizations.SingleAsync(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
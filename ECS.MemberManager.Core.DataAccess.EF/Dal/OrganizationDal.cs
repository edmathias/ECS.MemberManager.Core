using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class OrganizationDal : IDal<Organization>
    {
        private MembershipManagerDataContext _context;

        public OrganizationDal() => _context = new MembershipManagerDataContext();

        public OrganizationDal(MembershipManagerDataContext context) => _context = context;

        public async Task<List<Organization>> Fetch()
        {
            return await _context.Organizations
                .Include(a => a.OrganizationType)
                .Include(a => a.CategoryOfOrganization)
                .ToListAsync();
        }

        public async Task<Organization> Fetch(int id)
        {
            return await _context.Organizations.Where(a => a.Id == id)
                .Include(a => a.OrganizationType)
                .Include(a => a.CategoryOfOrganization)
                .FirstAsync();
        }

        public async Task<Organization> Insert(Organization organizationObjToInsert)
        {
            _context.Entry(organizationObjToInsert.OrganizationType).State = EntityState.Unchanged;
            _context.Entry(organizationObjToInsert.CategoryOfOrganization).State = EntityState.Unchanged;

            await _context.Organizations.AddAsync(organizationObjToInsert);

            await _context.SaveChangesAsync();

            return organizationObjToInsert;
        }

        public async Task<Organization> Update(Organization organizationObjToUpdate)
        {
            _context.Entry(organizationObjToUpdate.OrganizationType).State = EntityState.Unchanged;
            _context.Entry(organizationObjToUpdate.CategoryOfOrganization).State = EntityState.Unchanged;
            _context.Update(organizationObjToUpdate);

            await _context.SaveChangesAsync();

            return organizationObjToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.Organizations.SingleAsync(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
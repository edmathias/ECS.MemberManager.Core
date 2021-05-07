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
    public class OrganizationTypeDal : IDal<OrganizationType>
    {
        private MembershipManagerDataContext _context;

        public OrganizationTypeDal()
        {
            _context = new MembershipManagerDataContext();
        }

        public OrganizationTypeDal(MembershipManagerDataContext context)
        {
            _context = context;
        }

        public async Task<List<OrganizationType>> Fetch()
        {
            return await _context.OrganizationTypes
                .Include(ot => ot.CategoryOfOrganization)
                .ToListAsync();
        }

        public async Task<OrganizationType> Fetch(int id)
        {
            return await _context.OrganizationTypes.Where(a => a.Id == id)
                .Include(ot => ot.CategoryOfOrganization)
                .FirstAsync();
        }

        public async Task<OrganizationType> Insert(OrganizationType organizationTypeToInsert)
        {
            _context.Entry(organizationTypeToInsert.CategoryOfOrganization).State = EntityState.Unchanged;
            await _context.OrganizationTypes.AddAsync(organizationTypeToInsert);

            await _context.SaveChangesAsync();

            return organizationTypeToInsert;
        }

        public async Task<OrganizationType> Update(OrganizationType organizationTypeToUpdate)
        {
            _context.Entry(organizationTypeToUpdate.CategoryOfOrganization).State = EntityState.Unchanged;
            _context.Update(organizationTypeToUpdate);

            await _context.SaveChangesAsync();

            return organizationTypeToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.OrganizationTypes.SingleAsync(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
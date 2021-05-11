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
        private MembershipManagerDataContext _context;

        public OfficeDal() => _context = new MembershipManagerDataContext();

        public OfficeDal(MembershipManagerDataContext context) => _context = context;
        
        public async Task<List<Office>> Fetch()
        {
            return await _context.Offices
                .ToListAsync();
        }

        public async Task<Office> Fetch(int id)
        {
            return await _context.Offices.Where(a => a.Id == id)
                .FirstAsync();
        }

        public async Task<Office> Insert(Office officeObjToInsert)
        {
            await _context.Offices.AddAsync(officeObjToInsert);

            await _context.SaveChangesAsync();

            return officeObjToInsert;
        }

        public async Task<Office> Update(Office officeObjToUpdate)
        {
            _context.Update(officeObjToUpdate);

            await _context.SaveChangesAsync();

            return officeObjToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.Offices.SingleAsync(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
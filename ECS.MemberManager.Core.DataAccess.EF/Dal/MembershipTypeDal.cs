using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class MembershipTypeDal : IDal<MembershipType>
    {
        private MembershipManagerDataContext _context;

        public MembershipTypeDal()
        {
            _context = new MembershipManagerDataContext();
        }

        public MembershipTypeDal(MembershipManagerDataContext context)
        {
            _context = context;
        }

        public async Task<List<MembershipType>> Fetch()
        {
            return await _context.MembershipTypes
                .ToListAsync();
        }

        public async Task<MembershipType> Fetch(int id)
        {
            return await _context.MembershipTypes.Where(a => a.Id == id)
                .FirstAsync();
        }

        public async Task<MembershipType> Insert(MembershipType membershipTypeToInsert)
        {
            await _context.MembershipTypes.AddAsync(membershipTypeToInsert);

            await _context.SaveChangesAsync();

            return membershipTypeToInsert;
        }

        public async Task<MembershipType> Update(MembershipType membershipTypeToUpdate)
        {
            _context.Update(membershipTypeToUpdate);

            await _context.SaveChangesAsync();

            return membershipTypeToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.MembershipTypes.SingleAsync(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class MemberStatusDal : IDal<MemberStatus>
    {
        private MembershipManagerDataContext _context;

        public MemberStatusDal() => _context = new MembershipManagerDataContext();

        public MemberStatusDal(MembershipManagerDataContext context) => _context = new MembershipManagerDataContext();

        public async Task<List<MemberStatus>> Fetch()
        {
            return await _context.MemberStatuses.ToListAsync();
        }

        public async Task<MemberStatus> Fetch(int id)
        {
            return await _context.MemberStatuses.Where(a => a.Id == id).FirstAsync();
        }

        public async Task<MemberStatus> Insert(MemberStatus memberStatusToInsert)
        {
            await _context.MemberStatuses.AddAsync(memberStatusToInsert);
            await _context.SaveChangesAsync();

            return memberStatusToInsert;
        }

        public async Task<MemberStatus> Update(MemberStatus memberStatusToUpdate)
        {
            _context.Update(memberStatusToUpdate);
            await _context.SaveChangesAsync();

            return memberStatusToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.MemberStatuses.SingleAsync(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
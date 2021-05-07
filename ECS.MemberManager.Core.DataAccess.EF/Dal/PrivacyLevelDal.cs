using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class PrivacyLevelDal : IDal<PrivacyLevel>
    {
        private MembershipManagerDataContext _context;

        public PrivacyLevelDal()
        {
            _context = new MembershipManagerDataContext();
        }

        public PrivacyLevelDal(MembershipManagerDataContext context)
        {
            _context = context;
        }

        public async Task<List<PrivacyLevel>> Fetch()
        {
            return await _context.PrivacyLevels.ToListAsync();
        }

        public async Task<PrivacyLevel> Fetch(int id)
        {
            return await _context.PrivacyLevels.Where(a => a.Id == id).FirstAsync();
        }

        public async Task<PrivacyLevel> Insert(PrivacyLevel privacyLevelToInsert)
        {
            await _context.PrivacyLevels.AddAsync(privacyLevelToInsert);
            await _context.SaveChangesAsync();

            return privacyLevelToInsert;
        }

        public async Task<PrivacyLevel> Update(PrivacyLevel privacyLevelToUpdate)
        {
            _context.Update(privacyLevelToUpdate);
            await _context.SaveChangesAsync();

            return privacyLevelToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.PrivacyLevels.SingleAsync(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
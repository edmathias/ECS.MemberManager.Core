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
    public class MemberInfoDal : IDal<MemberInfo>
    {
        private MembershipManagerDataContext _context;

        public MemberInfoDal() =>  _context = new MembershipManagerDataContext();

        public MemberInfoDal(MembershipManagerDataContext context) => _context = context;

        public async Task<List<MemberInfo>> Fetch()
        {
            return await _context.MemberInfo
                .Include(mi => mi.Person)
                .Include(mi => mi.PrivacyLevel)
                .Include(mi => mi.MemberStatus)
                .Include(mi => mi.MembershipType)
                .ToListAsync();
        }

        public async Task<MemberInfo> Fetch(int id)
        {
            return await _context.MemberInfo.Where(a => a.Id == id)
                .Include(mi => mi.Person)
                .Include(mi => mi.PrivacyLevel)
                .Include(mi => mi.MemberStatus)
                .Include(mi => mi.MembershipType)
                .FirstAsync();
        }

        public async Task<MemberInfo> Insert(MemberInfo personToInsert)
        {
            _context.Entry(personToInsert.Person).State = EntityState.Unchanged;
            _context.Entry(personToInsert.PrivacyLevel).State = EntityState.Unchanged;
            _context.Entry(personToInsert.MemberStatus).State = EntityState.Unchanged;
            _context.Entry(personToInsert.MembershipType).State = EntityState.Unchanged;
            await _context.MemberInfo.AddAsync(personToInsert);

            await _context.SaveChangesAsync();

            return personToInsert;
        }

        public async Task<MemberInfo> Update(MemberInfo personToUpdate)
        {
            _context.Entry(personToUpdate.Person).State = EntityState.Unchanged;
            _context.Entry(personToUpdate.PrivacyLevel).State = EntityState.Unchanged;
            _context.Entry(personToUpdate.MemberStatus).State = EntityState.Unchanged;
            _context.Entry(personToUpdate.MembershipType).State = EntityState.Unchanged;

            _context.Update(personToUpdate);

            var result = await _context.SaveChangesAsync();

            if (result != 1)
                throw new ApplicationException("MemberInfo domain object was not updated");

            return personToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.MemberInfo.SingleAsync(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class EMailDal : IDal<EMail>
    {
        private MembershipManagerDataContext _context;

        public EMailDal()
        {
            _context = new MembershipManagerDataContext();
        }

        public EMailDal(MembershipManagerDataContext context)
        {
            _context = context;
        }

        public async Task<List<EMail>> Fetch()
        {
            return await _context.EMails
                .Include(e => e.EMailType)
                .ToListAsync();
        }

        public async Task<EMail> Fetch(int id)
        {
            return await _context.EMails.Where(a => a.Id == id)
                .Include(e => e.EMailType)
                .FirstAsync();
        }

        public async Task<EMail> Insert(EMail contactToInsert)
        {
            _context.Entry(contactToInsert.EMailType).State = EntityState.Unchanged;

            await _context.EMails.AddAsync(contactToInsert);

            await _context.SaveChangesAsync();

            return contactToInsert;
        }

        public async Task<EMail> Update(EMail contactToUpdate)
        {
            _context.Entry(contactToUpdate.EMailType).State = EntityState.Unchanged;

            _context.Update(contactToUpdate);

            await _context.SaveChangesAsync();

            return contactToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.EMails.SingleAsync(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
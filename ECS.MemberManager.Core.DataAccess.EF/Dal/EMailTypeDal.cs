using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class EMailTypeDal : IDal<EMailType>
    {
        private MembershipManagerDataContext _context;

        public EMailTypeDal()
        {
            _context = new MembershipManagerDataContext();
        }

        public EMailTypeDal(MembershipManagerDataContext context)
        {
            _context = context;
        }

        public async Task<List<EMailType>> Fetch()
        {
            return await _context.EMailTypes
                .ToListAsync();
        }

        public async Task<EMailType> Fetch(int id)
        {
            return await _context.EMailTypes.Where(a => a.Id == id)
                .FirstAsync();
        }

        public async Task<EMailType> Insert(EMailType emailTypeToInsert)
        {
            await _context.EMailTypes.AddAsync(emailTypeToInsert);

            await _context.SaveChangesAsync();

            return emailTypeToInsert;
        }

        public async Task<EMailType> Update(EMailType emailTypeToUpdate)
        {
            _context.Update(emailTypeToUpdate);

            await _context.SaveChangesAsync();

            return emailTypeToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.EMailTypes.SingleAsync(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class AddressDal : IDal<Address>
    {
        private MembershipManagerDataContext _context;

        public AddressDal() => _context = new MembershipManagerDataContext();

        public AddressDal(MembershipManagerDataContext context) => _context = context;

        public async Task<List<Address>> Fetch()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<Address> Fetch(int id)
        {
            return await _context.Addresses.Where(a => a.Id == id).FirstAsync();
        }

        public async Task<Address> Insert(Address addressToInsert)
        {
            await _context.Addresses.AddAsync(addressToInsert);
            await _context.SaveChangesAsync();

            return addressToInsert;
        }

        public async Task<Address> Update(Address addressToUpdate)
        {
            _context.Update(addressToUpdate);
            await _context.SaveChangesAsync();

            return addressToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.Addresses.SingleAsync(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
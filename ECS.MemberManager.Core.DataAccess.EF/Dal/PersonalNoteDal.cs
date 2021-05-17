using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class PersonalNoteDal : IDal<PersonalNote>
    {
        private MembershipManagerDataContext _context;

        public PersonalNoteDal() => _context = new MembershipManagerDataContext();

        public PersonalNoteDal(MembershipManagerDataContext context) => _context = context;

        public async Task<List<PersonalNote>> Fetch()
        {
            return await _context.PersonalNotes
                .Include(e => e.Person)
                .ToListAsync();
        }

        public async Task<PersonalNote> Fetch(int id)
        {
            return await _context.PersonalNotes.Where(a => a.Id == id)
                .Include(e => e.Person)
                .FirstAsync();
        }

        public async Task<PersonalNote> Insert(PersonalNote paymentToInsert)
        {
            _context.Entry(paymentToInsert.Person).State = EntityState.Unchanged;
            await _context.PersonalNotes.AddAsync(paymentToInsert);
            await _context.SaveChangesAsync();

            return paymentToInsert;
        }

        public async Task<PersonalNote> Update(PersonalNote paymentToUpdate)
        {
            _context.Entry(paymentToUpdate.Person).State = EntityState.Unchanged;
            _context.Update(paymentToUpdate);

            await _context.SaveChangesAsync();

            return paymentToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.PersonalNotes.SingleAsync(a => a.Id == id));

            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
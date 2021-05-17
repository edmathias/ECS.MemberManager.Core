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
    public class TermInOfficeDal : IDal<TermInOffice>
    {
        private MembershipManagerDataContext _context;

        public TermInOfficeDal() => _context = new MembershipManagerDataContext();

        public TermInOfficeDal(MembershipManagerDataContext context) => _context = context;

        public async Task<List<TermInOffice>> Fetch()
        {
            return await _context.TermInOffices
                .Include(p => p.Person)
                .Include(p => p.Office)
                .ToListAsync();
        }

        public async Task<TermInOffice> Fetch(int id)
        {
            return await _context.TermInOffices.Where(a => a.Id == id)
                .Include(p => p.Person)
                .Include(p => p.Office)
                .FirstAsync();
        }

        public async Task<TermInOffice> Insert(TermInOffice termToInsert)
        {
            _context.Entry(termToInsert.Person).State = EntityState.Unchanged;
            _context.Entry(termToInsert.Office).State = EntityState.Unchanged;

            await _context.TermInOffices.AddAsync(termToInsert);

            await _context.SaveChangesAsync();

            return termToInsert;
        }

        public async Task<TermInOffice> Update(TermInOffice termToUpdate)
        {
            _context.Entry(termToUpdate.Person).State = EntityState.Unchanged;
            _context.Entry(termToUpdate.Office).State = EntityState.Unchanged;

            _context.Update(termToUpdate);

            var result = await _context.SaveChangesAsync();

            if (result != 1)
                throw new ApplicationException("TermInOffice domain object was not updated");

            return termToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.TermInOffices.SingleAsync(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
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
    public class PersonDal : IDal<Person>
    {
        private MembershipManagerDataContext _context;

        public PersonDal()
        {
            _context = new MembershipManagerDataContext();
        }

        public PersonDal(MembershipManagerDataContext context)
        {
            _context = context;
        }

        public async Task<List<Person>> Fetch()
        {
            return await _context.Persons
                .Include(p => p.Title)
                .Include(p => p.EMail)
                .ToListAsync();
        }

        public async Task<Person> Fetch(int id)
        {
            return await _context.Persons.Where(a => a.Id == id)
                .Include(p => p.Title)
                .Include(p => p.EMail)
                .FirstAsync();
        }

        public async Task<Person> Insert(Person personToInsert)
        {
            _context.Entry(personToInsert.Title).State = EntityState.Unchanged;
            _context.Entry(personToInsert.EMail).State = EntityState.Unchanged;
            await _context.Persons.AddAsync(personToInsert);

            await _context.SaveChangesAsync();

            return personToInsert;
        }

        public async Task<Person> Update(Person personToUpdate)
        {
            _context.Entry(personToUpdate.Title).State = EntityState.Unchanged;
            _context.Entry(personToUpdate.EMail).State = EntityState.Unchanged;

            _context.Update(personToUpdate);

            var result = await _context.SaveChangesAsync();

            if (result != 1)
                throw new ApplicationException("Person domain object was not updated");

            return personToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.Persons.SingleAsync(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
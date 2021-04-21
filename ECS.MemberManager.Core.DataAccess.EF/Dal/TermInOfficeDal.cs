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
        public async Task<List<TermInOffice>> Fetch()
        {
            List<TermInOffice> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.TermInOffices
                    .Include(p => p.Person)
                    .Include(p => p.Office)
                    .ToListAsync();
            }

            return list;
        }

        public async Task<TermInOffice> Fetch(int id)
        {
            TermInOffice term = null;

            using (var context = new MembershipManagerDataContext())
            {
                term = await context.TermInOffices.Where(a => a.Id == id)
                    .Include(p => p.Person)
                    .Include(p => p.Office)
                    .FirstAsync();
            }

            return term;
        }

        public async Task<TermInOffice> Insert(TermInOffice termToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(termToInsert.Person).State = EntityState.Unchanged;
                context.Entry(termToInsert.Office).State = EntityState.Unchanged;
                
                await context.TermInOffices.AddAsync(termToInsert);

                await context.SaveChangesAsync();
            }

            return termToInsert;
        }

        public async Task<TermInOffice> Update(TermInOffice termToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(termToUpdate.Person).State = EntityState.Unchanged;
                context.Entry(termToUpdate.Office).State = EntityState.Unchanged;
                
                context.Update(termToUpdate);

                var result = await context.SaveChangesAsync();

                if (result != 1)
                    throw new ApplicationException("TermInOffice domain object was not updated");
            }

            return termToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.TermInOffices.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}
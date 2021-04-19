﻿using System;
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
        public async Task<List<Person>> Fetch()
        {
            List<Person> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.Persons
                    .Include(p => p.Title)
                    .Include(p => p.EMail)
                    .ToListAsync();
            }

            return list;
        }

        public async Task<Person> Fetch(int id)
        {
            Person person = null;

            using (var context = new MembershipManagerDataContext())
            {
                person = await context.Persons.Where(a => a.Id == id)
                    .Include(p => p.Title)
                    .Include(p => p.EMail)
                    .FirstAsync();
            }

            return person;
        }

        public async Task<Person> Insert(Person personToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(personToInsert.Title).State = EntityState.Unchanged;
                context.Entry(personToInsert.EMail).State = EntityState.Unchanged;
                
                await context.Persons.AddAsync(personToInsert);

                await context.SaveChangesAsync();
            }

            return personToInsert;
        }

        public async Task<Person> Update(Person personToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(personToUpdate.Title).State = EntityState.Unchanged;
                context.Entry(personToUpdate.EMail).State = EntityState.Unchanged;
                
                context.Update(personToUpdate);

                var result = await context.SaveChangesAsync();

                if (result != 1)
                    throw new ApplicationException("Person domain object was not updated");
            }

            return personToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.Persons.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}
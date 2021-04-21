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
    public class TitleDal : IDal<Title> 
    {
        public async Task<List<Title>> Fetch()
        {
            List<Title> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.Titles.ToListAsync();
            }

            return list;
        }

        public async Task<Title> Fetch(int id)
        {
            Title title = null;

            using (var context = new MembershipManagerDataContext())
            {
                title = await context.Titles.Where(a => a.Id == id).FirstAsync();
            }

            return title;
        }

        public async Task<Title> Insert(Title titleToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                await context.Titles.AddAsync(titleToInsert);
                await context.SaveChangesAsync();
            }

            return titleToInsert;
        }

        public async Task<Title> Update(Title titleToUpdate)
        {
            int result = 0;
            
            using (var context = new MembershipManagerDataContext())
            {
                context.Update(titleToUpdate);
                try
                {
                    result = await context.SaveChangesAsync();
                }
                catch (Exception exc)
                {
                    System.Console.WriteLine(exc.Message);
                }
            }

            return titleToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.Titles.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}
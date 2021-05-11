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
        private MembershipManagerDataContext _context;

        public TitleDal() => _context = new MembershipManagerDataContext();

        public TitleDal(MembershipManagerDataContext context) => _context = context;

        public async Task<List<Title>> Fetch()
        {
            return await _context.Titles.ToListAsync();
        }

        public async Task<Title> Fetch(int id)
        {
            return await _context.Titles.Where(a => a.Id == id).FirstAsync();
        }

        public async Task<Title> Insert(Title titleToInsert)
        {
            await _context.Titles.AddAsync(titleToInsert);
            await _context.SaveChangesAsync();

            return titleToInsert;
        }

        public async Task<Title> Update(Title titleToUpdate)
        {
            _context.Update(titleToUpdate);
            await _context.SaveChangesAsync();

            return titleToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.Titles.SingleAsync(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
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
    public class PhoneDal : IDal<Phone>
    {
        private MembershipManagerDataContext _context;

        public PhoneDal()
        {
            _context = new MembershipManagerDataContext();
        }

        public PhoneDal(MembershipManagerDataContext context)
        {
            _context = context;
        }
        public async Task<List<Phone>> Fetch()
        {
            List<Phone> list;

            using (var _context = new MembershipManagerDataContext())
            {
                list = await _context.Phones.ToListAsync();
            }

            return list;
        }

        public async Task<Phone> Fetch(int id)
        {
            Phone phone = null;

            using (var _context = new MembershipManagerDataContext())
            {
                phone = await _context.Phones.Where(a => a.Id == id).FirstAsync();
            }

            return phone;
        }

        public async Task<Phone> Insert(Phone phoneToInsert)
        {
            using (var _context = new MembershipManagerDataContext())
            {
                await _context.Phones.AddAsync(phoneToInsert);
                await _context.SaveChangesAsync();
            }

            return phoneToInsert;
        }

        public async Task<Phone> Update(Phone phoneToUpdate)
        {
            using (var _context = new MembershipManagerDataContext())
            {
                _context.Update(phoneToUpdate);
            
                await _context.SaveChangesAsync();
            }

            return phoneToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var _context = new MembershipManagerDataContext())
            {
                _context.Remove(await _context.Phones.SingleAsync(a => a.Id == id));
                await _context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}
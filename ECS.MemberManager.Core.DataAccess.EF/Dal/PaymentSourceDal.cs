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
    public class PaymentSourceDal : IDal<PaymentSource>
    {
        private MembershipManagerDataContext _context;

        public PaymentSourceDal()
        {
            _context = new MembershipManagerDataContext();
        }

        public PaymentSourceDal( MembershipManagerDataContext context)
        {
            _context = context;
        }

        public async Task<List<PaymentSource>> Fetch()
        {
            return await _context.PaymentSources.ToListAsync();
        }

        public async Task<PaymentSource> Fetch(int id)
        {
            return await _context.PaymentSources.Where(a => a.Id == id).FirstAsync();
        }

        public async Task<PaymentSource> Insert(PaymentSource paymentSourceToInsert)
        {
            await _context.PaymentSources.AddAsync(paymentSourceToInsert);
            await _context.SaveChangesAsync();

            return paymentSourceToInsert;
        }

        public async Task<PaymentSource> Update(PaymentSource paymentSourceToUpdate)
        {
            _context.Update(paymentSourceToUpdate);

            await _context.SaveChangesAsync();

            return paymentSourceToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.PaymentSources.SingleAsync(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
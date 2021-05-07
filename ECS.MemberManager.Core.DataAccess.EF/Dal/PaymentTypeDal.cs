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
    public class PaymentTypeDal : IDal<PaymentType>
    {
        private MembershipManagerDataContext _context;

        public PaymentTypeDal()
        {
            _context = new MembershipManagerDataContext();
        }

        public PaymentTypeDal(MembershipManagerDataContext context)
        {
            _context = context;
        }

        public async Task<List<PaymentType>> Fetch()
        {
            return await _context.PaymentTypes.ToListAsync();
        }

        public async Task<PaymentType> Fetch(int id)
        {
            return await _context.PaymentTypes.Where(a => a.Id == id).FirstAsync();
        }

        public async Task<PaymentType> Insert(PaymentType paymentTypeToInsert)
        {
            await _context.PaymentTypes.AddAsync(paymentTypeToInsert);
            await _context.SaveChangesAsync();

            return paymentTypeToInsert;
        }

        public async Task<PaymentType> Update(PaymentType paymentTypeToUpdate)
        {
            _context.Update(paymentTypeToUpdate);
            await _context.SaveChangesAsync();

            return paymentTypeToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.PaymentTypes.SingleAsync(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
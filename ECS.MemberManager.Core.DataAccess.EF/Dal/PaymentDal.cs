using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class PaymentDal : IDal<Payment>
    {
        private MembershipManagerDataContext _context;

        public PaymentDal() => _context = new MembershipManagerDataContext();

        public PaymentDal(MembershipManagerDataContext context) => _context = context;

        public async Task<List<Payment>> Fetch()
        {
            return await _context.Payments
                .Include(e => e.PaymentType)
                .Include(e => e.Person)
                .Include(e => e.PaymentSource)
                .ToListAsync();
        }

        public async Task<Payment> Fetch(int id)
        {
            return await _context.Payments.Where(a => a.Id == id)
                .Include(e => e.PaymentType)
                .Include(e => e.Person)
                .Include(e => e.PaymentSource)
                .FirstAsync();
        }

        public async Task<Payment> Insert(Payment paymentToInsert)
        {
            _context.Entry(paymentToInsert.PaymentType).State = EntityState.Unchanged;
            _context.Entry(paymentToInsert.PaymentSource).State = EntityState.Unchanged;
            _context.Entry(paymentToInsert.Person).State = EntityState.Unchanged;

            await _context.Payments.AddAsync(paymentToInsert);

            await _context.SaveChangesAsync();

            return paymentToInsert;
        }

        public async Task<Payment> Update(Payment paymentToUpdate)
        {
            _context.Entry(paymentToUpdate.PaymentType).State = EntityState.Unchanged;
            _context.Entry(paymentToUpdate.PaymentSource).State = EntityState.Unchanged;
            _context.Entry(paymentToUpdate.Person).State = EntityState.Unchanged;

            _context.Update(paymentToUpdate);

            await _context.SaveChangesAsync();

            return paymentToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.Payments.SingleAsync(a => a.Id == id));

            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
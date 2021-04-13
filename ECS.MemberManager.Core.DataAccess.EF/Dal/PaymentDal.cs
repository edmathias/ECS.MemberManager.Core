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
        public async Task<List<Payment>> Fetch()
        {
            List<Payment> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.Payments
                    .Include(e => e.PaymentType)
                    .Include(e => e.Person)
                    .Include(e => e.PaymentSource)
                    .ToListAsync();
            }

            return list;
        }

        public async Task<Payment> Fetch(int id)
        {
            Payment payment = null;

            using (var context = new MembershipManagerDataContext())
            {
                payment = await context.Payments.Where(a => a.Id == id)
                    .Include(e => e.PaymentType)
                    .Include(e => e.Person)
                    .Include(e => e.PaymentSource)
                    .FirstAsync();
            }

            return payment;
        }

        public async Task<Payment> Insert(Payment paymentToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(paymentToInsert.PaymentType).State = EntityState.Unchanged;
                context.Entry(paymentToInsert.PaymentSource).State = EntityState.Unchanged;
                context.Entry(paymentToInsert.Person).State = EntityState.Unchanged;

                await context.Payments.AddAsync(paymentToInsert);

                await context.SaveChangesAsync();
            }

            return paymentToInsert;
        }

        public async Task<Payment> Update(Payment paymentToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(paymentToUpdate.PaymentType).State = EntityState.Unchanged;
                context.Entry(paymentToUpdate.PaymentSource).State = EntityState.Unchanged;
                context.Entry(paymentToUpdate.Person).State = EntityState.Unchanged;

                context.Update(paymentToUpdate);

                await context.SaveChangesAsync();
            }

            return paymentToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.Payments.SingleAsync(a => a.Id == id));
                
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}
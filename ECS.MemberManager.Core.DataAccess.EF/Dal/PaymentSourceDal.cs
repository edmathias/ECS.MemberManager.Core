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
        public async Task<List<PaymentSource>> Fetch()
        {
            List<PaymentSource> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.PaymentSources.ToListAsync();
            }

            return list;
        }

        public async Task<PaymentSource> Fetch(int id)
        {
            PaymentSource paymentSource = null;

            using (var context = new MembershipManagerDataContext())
            {
                paymentSource = await context.PaymentSources.Where(a => a.Id == id).FirstAsync();
            }

            return paymentSource;
        }

        public async Task<PaymentSource> Insert(PaymentSource paymentSourceToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                await context.PaymentSources.AddAsync(paymentSourceToInsert);
                await context.SaveChangesAsync();
            }

            return paymentSourceToInsert;
        }

        public async Task<PaymentSource> Update(PaymentSource paymentSourceToUpdate)
        {
            int result = 0;
            
            using (var context = new MembershipManagerDataContext())
            {
                context.Update(paymentSourceToUpdate);
                try
                {
                    result = await context.SaveChangesAsync();
                }
                catch (Exception exc)
                {
                    System.Console.WriteLine(exc.Message);
                }
            }

            return paymentSourceToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.PaymentSources.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}
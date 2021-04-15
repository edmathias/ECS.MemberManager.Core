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
        public async Task<List<PaymentType>> Fetch()
        {
            List<PaymentType> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.PaymentTypes.ToListAsync();
            }

            return list;
        }

        public async Task<PaymentType> Fetch(int id)
        {
            PaymentType paymentType = null;

            using (var context = new MembershipManagerDataContext())
            {
                paymentType = await context.PaymentTypes.Where(a => a.Id == id).FirstAsync();
            }

            return paymentType;
        }

        public async Task<PaymentType> Insert(PaymentType paymentTypeToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                await context.PaymentTypes.AddAsync(paymentTypeToInsert);
                await context.SaveChangesAsync();
            }

            return paymentTypeToInsert;
        }

        public async Task<PaymentType> Update(PaymentType paymentTypeToUpdate)
        {
            int result = 0;
            
            using (var context = new MembershipManagerDataContext())
            {
                context.Update(paymentTypeToUpdate);
                try
                {
                    result = await context.SaveChangesAsync();
                }
                catch (Exception exc)
                {
                    System.Console.WriteLine(exc.Message);
                }
            }

            return paymentTypeToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.PaymentTypes.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}
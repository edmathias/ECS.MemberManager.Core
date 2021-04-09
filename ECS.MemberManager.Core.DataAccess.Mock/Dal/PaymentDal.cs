using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class PaymentDal : IDal<Payment>
    {
        public void Dispose()
        {
        }

        public Task<Payment> Fetch(int id)
        {
            return Task.FromResult(MockDb.Payments.FirstOrDefault(dt => dt.Id == id));
        }

        public Task<List<Payment>> Fetch()
        {
            return Task.FromResult(MockDb.Payments.ToList());
        }

        public Task<Payment> Insert(Payment payment)
        {
            var lastPayment = MockDb.Payments.ToList().OrderByDescending(dt => dt.Id).First();
            payment.Id = 1 + lastPayment.Id;
            payment.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.Payments.Add(payment);

            return Task.FromResult(payment);
        }

        public Task<Payment> Update(Payment payment)
        {
            var paymentToUpdate =
                MockDb.Payments.FirstOrDefault(em => em.Id == payment.Id &&
                                                     em.RowVersion.SequenceEqual(payment.RowVersion));

            if (paymentToUpdate == null)
                throw new Csla.DataPortalException(null);

            paymentToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(paymentToUpdate);
        }

        public Task Delete(int id)
        {
            var paymentsToDelete = MockDb.Payments.FirstOrDefault(dt => dt.Id == id);
            var listIndex = MockDb.Payments.IndexOf(paymentsToDelete);
            if (listIndex > -1)
                MockDb.Payments.RemoveAt(listIndex);
            
            return Task.CompletedTask;
        }
    }
}
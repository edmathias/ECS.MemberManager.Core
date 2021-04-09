using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class PaymentSourceDal : IDal<PaymentSource>
    {
        public Task<PaymentSource> Fetch(int id)
        {
            return Task.FromResult(MockDb.PaymentSources.FirstOrDefault(ms => ms.Id == id));
        }

        public Task<List<PaymentSource>> Fetch()
        {
            return Task.FromResult(MockDb.PaymentSources.ToList());
        }

        public Task<PaymentSource> Insert(PaymentSource paymentSource)
        {
            var lastPaymentSource = MockDb.PaymentSources.ToList().OrderByDescending(ms => ms.Id).First();
            paymentSource.Id = 1 + lastPaymentSource.Id;
            paymentSource.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.PaymentSources.Add(paymentSource);

            return Task.FromResult(paymentSource);
        }

        public Task<PaymentSource> Update(PaymentSource paymentSource)
        {
            var paymentSourceToUpdate =
                MockDb.PaymentSources.FirstOrDefault(em => em.Id == paymentSource.Id &&
                                                           em.RowVersion.SequenceEqual(paymentSource.RowVersion));

            if (paymentSourceToUpdate == null)
                throw new Csla.DataPortalException(null);

            paymentSourceToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(paymentSourceToUpdate);
        }

        public Task Delete(int id)
        {
            var paymentSourceToDelete = MockDb.PaymentSources.FirstOrDefault(ms => ms.Id == id);
            var listIndex = MockDb.PaymentSources.IndexOf(paymentSourceToDelete);
            if (listIndex > -1)
                MockDb.PaymentSources.RemoveAt(listIndex);
            
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
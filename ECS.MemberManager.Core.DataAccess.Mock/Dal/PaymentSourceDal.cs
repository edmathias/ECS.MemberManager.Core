using System;
using System.Collections.Generic;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class PaymentSourceDal : IPaymentSourceDal
    {
        public PaymentSource Fetch(int id)
        {
            return MockDb.PaymentSources.FirstOrDefault(ms => ms.Id == id);
        }

        public List<PaymentSource> Fetch()
        {
            return MockDb.PaymentSources.ToList();
        }

        public int Insert( PaymentSource paymentSource)
        {
            var lastPaymentSource = MockDb.PaymentSources.ToList().OrderByDescending(ms => ms.Id).First();
            paymentSource.Id = ++lastPaymentSource.Id;
            MockDb.PaymentSources.Add(paymentSource);
            
            return paymentSource.Id;
        }

        public void Update(PaymentSource paymentSource)
        {
            // mockdb in memory list reference already updated 
        }

        public void Delete(int id)
        {
            var paymentSourceToDelete = MockDb.PaymentSources.FirstOrDefault(ms => ms.Id == id);
            var listIndex = MockDb.PaymentSources.IndexOf(paymentSourceToDelete);
            if(listIndex > -1)
                MockDb.PaymentSources.RemoveAt(listIndex);
        }

        public void Dispose()
        {
        }
    }
}
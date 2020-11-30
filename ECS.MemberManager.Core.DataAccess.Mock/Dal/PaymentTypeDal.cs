using System;
using System.Collections.Generic;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class PaymentTypeDal : IPaymentTypeDal
    {
        public PaymentType Fetch(int id)
        {
            return MockDb.PaymentTypes.FirstOrDefault(ms => ms.Id == id);
        }

        public List<PaymentType> Fetch()
        {
            return MockDb.PaymentTypes.ToList();
        }

        public int Insert( PaymentType paymentType)
        {
            var lastPaymentType = MockDb.PaymentTypes.ToList().OrderByDescending(ms => ms.Id).First();
            paymentType.Id = ++lastPaymentType.Id;
            MockDb.PaymentTypes.Add(paymentType);
            
            return paymentType.Id;
        }

        public void Update(PaymentType paymentType)
        {
            // mockdb in memory list reference already updated 
        }

        public void Delete(int id)
        {
            var paymentTypeToDelete = MockDb.PaymentTypes.FirstOrDefault(ms => ms.Id == id);
            var listIndex = MockDb.PaymentTypes.IndexOf(paymentTypeToDelete);
            if(listIndex > -1)
                MockDb.PaymentTypes.RemoveAt(listIndex);
        }

        public void Dispose()
        {
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class PaymentDal : IPaymentDal
    {
        public void Dispose()
        {
        }

        public Payment Fetch(int id)
        {
            return MockDb.Payments.FirstOrDefault(dt => dt.Id == id);
        }

        public List<Payment> Fetch()
        {
            return MockDb.Payments.ToList();
        }

        public int Insert(Payment payment)
        {
            var lastPayment = MockDb.Payments.ToList().OrderByDescending(dt => dt.Id).First();
            payment.Id = 1+lastPayment.Id;
            MockDb.Payments.Add(payment);
            
            return payment.Id;
        }

        public void Update(Payment payment)
        {
            // mockdb in memory list reference already updated 
        }

        public void Delete(int id)
        {
            var paymentsToDelete = MockDb.Payments.FirstOrDefault(dt => dt.Id == id);
            var listIndex = MockDb.Payments.IndexOf(paymentsToDelete);
            if(listIndex > -1)
                MockDb.Payments.RemoveAt(listIndex);
        }
    }
}
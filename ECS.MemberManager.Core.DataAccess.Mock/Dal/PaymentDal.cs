using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class PaymentDal : IPaymentDal
    {
        public void Dispose()
        {
        }

        public async Task<Payment> Fetch(int id)
        {
            return MockDb.Payments.FirstOrDefault(dt => dt.Id == id);
        }

        public async Task<List<Payment>> Fetch()
        {
            return MockDb.Payments.ToList();
        }

        public async Task<Payment> Insert(Payment payment)
        {
            var lastPayment = MockDb.Payments.ToList().OrderByDescending(dt => dt.Id).First();
            payment.Id = 1+lastPayment.Id;
            MockDb.Payments.Add(payment);
            
            return payment;
        }

        public async Task<Payment> Update(Payment payment)
        {
            // mockdb in memory list reference already updated 
            return payment;
        }

        public async Task Delete(int id)
        {
            var paymentsToDelete = MockDb.Payments.FirstOrDefault(dt => dt.Id == id);
            var listIndex = MockDb.Payments.IndexOf(paymentsToDelete);
            if(listIndex > -1)
                MockDb.Payments.RemoveAt(listIndex);
        }
    }
}
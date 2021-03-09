using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class PaymentTypeDal : IPaymentTypeDal
    {
        public async Task<PaymentType> Fetch(int id)
        {
            return MockDb.PaymentTypes.FirstOrDefault(ms => ms.Id == id);
        }

        public async Task<List<PaymentType>> Fetch()
        {
            return MockDb.PaymentTypes.ToList();
        }

        public async Task<PaymentType> Insert( PaymentType paymentType)
        {
            var lastPaymentType = MockDb.PaymentTypes.ToList().OrderByDescending(ms => ms.Id).First();
            paymentType.Id = 1+lastPaymentType.Id;
            paymentType.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            
            MockDb.PaymentTypes.Add(paymentType);
            
            return paymentType;
        }

        public async Task<PaymentType> Update(PaymentType paymentType)
        {
            var paymentTypeToUpdate =
                MockDb.PaymentTypes.FirstOrDefault(em => em.Id == paymentType.Id &&
                                                         em.RowVersion.SequenceEqual(paymentType.RowVersion));

            if(paymentTypeToUpdate == null)
                throw new Csla.DataPortalException(null);
           
            paymentTypeToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return paymentTypeToUpdate;
            
        }

        public async Task Delete(int id)
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
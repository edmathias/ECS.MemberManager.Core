using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IPaymentSourceDal : IDisposable
    {
        Task<List<PaymentSource>> Fetch();
        Task<PaymentSource> Fetch(int id);
        Task<PaymentSource> Insert(PaymentSource paymentSourceToInsert);
        Task<PaymentSource> Update(PaymentSource paymentSourceToUpdate);
        Task Delete(int id);
    }
}
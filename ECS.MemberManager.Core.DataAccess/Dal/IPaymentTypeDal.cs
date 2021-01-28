using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IPaymentTypeDal : IDisposable
    {
        Task<List<PaymentType>> Fetch();
        Task<PaymentType> Fetch(int id);
        Task<PaymentType> Insert(PaymentType eMailTypeToInsert);
        Task<PaymentType> Update(PaymentType eMailTypeToUpdate);
        Task Delete(int id);
    }
}
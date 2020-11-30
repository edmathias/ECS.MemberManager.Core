using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IPaymentTypeDal : IDisposable
    {
        List<PaymentType> Fetch();
        PaymentType Fetch(int id);
        int Insert(PaymentType eMailTypeToInsert);
        void Update(PaymentType eMailTypeToUpdate);
        void Delete(int id);
    }
}
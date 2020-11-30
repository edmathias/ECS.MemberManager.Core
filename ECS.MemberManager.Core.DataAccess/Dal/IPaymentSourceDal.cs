using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IPaymentSourceDal : IDisposable
    {
        List<PaymentSource> Fetch();
        PaymentSource Fetch(int id);
        int Insert(PaymentSource eMailTypeToInsert);
        void Update(PaymentSource eMailTypeToUpdate);
        void Delete(int id);
    }
}
using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IPaymentDal : IDisposable
    {
        Payment Fetch(int id);
        List<Payment> Fetch();
        int Insert(Payment payment);
        void Update(Payment payment );
        void Delete(int id);
    }
}
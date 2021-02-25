using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IPaymentDal : IDisposable
    {
        Task<List<Payment>> Fetch();
        Task<Payment> Fetch(int id);
        Task<Payment> Insert(Payment organizationToInsert);
        Task<Payment> Update(Payment eMailToUpdate);
        Task Delete(int id);
    }
}
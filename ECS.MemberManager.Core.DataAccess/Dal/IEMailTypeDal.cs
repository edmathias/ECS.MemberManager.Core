using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IEMailTypeDal : IDisposable
    {
        Task<List<EMailType>> Fetch();
        Task<EMailType> Fetch(int id);
        Task<EMailType> Insert(EMailType eMailTypeToInsert);
        Task<EMailType> Update(EMailType eMailTypeToUpdate);
        Task Delete(int id);
    }
}
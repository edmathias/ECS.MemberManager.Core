using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IEMailDal : IDisposable
    {
        Task<List<EMail>> Fetch();
        Task<EMail> Fetch(int id);
        Task<EMail> Insert(EMail eMailToInsert);
        Task<EMail> Update(EMail eMailToUpdate);
        Task Delete(int id);
    }
}
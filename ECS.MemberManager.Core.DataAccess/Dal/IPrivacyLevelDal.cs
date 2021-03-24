using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IPrivacyLevelDal : IDisposable
    {
        Task<PrivacyLevel> Fetch(int id);
        Task<List<PrivacyLevel>> Fetch();
        Task<PrivacyLevel> Insert(PrivacyLevel privacyLevel);
        Task<PrivacyLevel> Update(PrivacyLevel privacyLevel);
        Task Delete(int id);
    }
}
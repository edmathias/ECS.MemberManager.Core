using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IPrivacyLevelDal : IDisposable
    {
        PrivacyLevel Fetch(int id);
        List<PrivacyLevel> Fetch();
        int Insert(PrivacyLevel privacyLevel);
        void Update(PrivacyLevel privacyLevel );
        void Delete(int id);
    }
}
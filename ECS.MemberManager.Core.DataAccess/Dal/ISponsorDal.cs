using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface ISponsorDal : IDisposable
    {
        Sponsor Fetch(int id);
        List<Sponsor> Fetch();
        int Insert(Sponsor person);
        void Update(Sponsor person );
        void Delete(int id);
    }
}
using System;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IMemberStatusDal : IDisposable
    {
        MemberStatus Fetch(int id);
        int Insert(MemberStatus memberStatus);
        void Update(MemberStatus memberStatus );
        void Delete(int id);
    }
}
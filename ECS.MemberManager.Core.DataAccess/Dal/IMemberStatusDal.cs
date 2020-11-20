using System;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IMemberStatusDal : IDisposable
    {
        public MemberStatus Fetch(int id);
        public int Insert(MemberStatus memberStatus);
        public void Update(MemberStatus memberStatus);
        public void Delete(MemberStatus memberStatus);
    }
}
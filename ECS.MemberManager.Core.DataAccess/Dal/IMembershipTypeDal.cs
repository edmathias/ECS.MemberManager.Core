using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IMembershipTypeDal : IDisposable
    {
        MembershipType Fetch(int id);
        List<MembershipType> Fetch();
        int Insert(MembershipType documentType);
        void Update(MembershipType documentType );
        void Delete(int id);
    }
}
using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IPhoneDal : IDisposable
    {
        Phone Fetch(int id);
        List<Phone> Fetch();
        int Insert(Phone documentType);
        void Update(Phone documentType );
        void Delete(int id);
    }
}
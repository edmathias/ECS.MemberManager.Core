using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IEMailDal : IDisposable
    {
        List<EMail> Fetch();
        EMail Fetch(int id);
        int Insert(EMail eMailToInsert);
        int Update(EMail eMailToUpdate);
        void Delete(int id);
    }
}
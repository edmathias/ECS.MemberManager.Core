using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IEMailDal : IDisposable
    {
        List<EMail> Fetch();
        EMail Fetch(int id);
        EMail Insert(EMail eMailToInsert);
        EMail Update(EMail eMailToUpdate);
        void Delete(int id);
    }
}
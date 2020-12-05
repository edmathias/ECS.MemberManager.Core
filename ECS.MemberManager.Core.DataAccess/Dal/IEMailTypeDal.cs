using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IEMailTypeDal : IDisposable
    {
        List<EMailType> Fetch();
        EMailType Fetch(int id);
        int Insert(EMailType eMailTypeToInsert);
        int Update(EMailType eMailTypeToUpdate);
        void Delete(int id);
    }
}
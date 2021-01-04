using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IEMailTypeDal : IDisposable
    {
        List<EMailType> Fetch();
        EMailType Fetch(int id);
        EMailType Insert(EMailType eMailTypeToInsert);
        EMailType Update(EMailType eMailTypeToUpdate);
        void Delete(int id);
    }
}
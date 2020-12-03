using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.SqlEF
{
    public class EMailDal : IEMailDal
    {
        public void Dispose()
        {
        }

        public List<EMail> Fetch()
        {
            
            throw new System.NotImplementedException();
        }

        public EMail Fetch(int id)
        {
            throw new System.NotImplementedException();
        }

        public int Insert(EMail eMailToInsert)
        {
            throw new System.NotImplementedException();
        }

        public void Update(EMail eMailToUpdate)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class EMailDal : IEMailDal
    {
        public EMail Fetch(int id)
        {
            return MockDb.EMails.FirstOrDefault(ms => ms.Id == id);
        }

        public List<EMail> Fetch()
        {
            return MockDb.EMails.ToList();
        }

        public int Insert( EMail eMail)
        {
            var lastEMail = MockDb.EMails.ToList().OrderByDescending(ms => ms.Id).First();
            eMail.Id = lastEMail.Id+1;
            MockDb.EMails.Add(eMail);
            
            return eMail.Id;
        }

        public int Update(EMail eMail)
        {
            // mockdb in memory list reference already updated 
            return 1;
        }

        public void Delete(int id)
        {
            var eMailToDelete = MockDb.EMails.FirstOrDefault(ms => ms.Id == id);
            var listIndex = MockDb.EMails.IndexOf(eMailToDelete);
            if(listIndex > -1)
                MockDb.EMails.RemoveAt(listIndex);
        }

        public void Dispose()
        {
        }
    }
}
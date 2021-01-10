using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class EMailDal : IEMailDal
    {
        public async Task<EMail> Fetch(int id)
        {
            return MockDb.EMails.FirstOrDefault(ms => ms.Id == id);
        }

        public List<EMail> Fetch()
        {
            return MockDb.EMails.ToList();
        }

        public async Task<EMail> Insert( EMail eMail)
        {
            var lastEMail = MockDb.EMails.ToList().OrderByDescending(ms => ms.Id).First();
            eMail.Id = lastEMail.Id+1;
            MockDb.EMails.Add(eMail);
            
            return eMail;
        }

        public async Task<EMail> Update(EMail eMail)
        {
            // mockdb in memory list reference already updated
            return eMail;
        }

        public async Task Delete(int id)
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
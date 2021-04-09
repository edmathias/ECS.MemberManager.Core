using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class EMailDal : IDal<EMail>
    {
        public Task<EMail> Fetch(int id)
        {
            return Task.FromResult(MockDb.EMails.FirstOrDefault(ms => ms.Id == id));
        }

        public Task<List<EMail>> Fetch()
        {
            return Task.FromResult(MockDb.EMails.ToList());
        }

        public Task<EMail> Insert(EMail eMail)
        {
            var lastEMail = MockDb.EMails.ToList().OrderByDescending(ms => ms.Id).First();
            eMail.Id = lastEMail.Id + 1;
            eMail.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.EMails.Add(eMail);

            return Task.FromResult(eMail);
        }

        public Task<EMail> Update(EMail eMail)
        {
            var emailToUpdate =
                MockDb.EMails.FirstOrDefault(em => em.Id == eMail.Id &&
                                                   em.RowVersion.SequenceEqual(eMail.RowVersion));

            if (emailToUpdate == null)
                throw new Csla.DataPortalException(null);

            emailToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(emailToUpdate);
        }

        public Task Delete(int id)
        {
            var eMailToDelete = MockDb.EMails.FirstOrDefault(ms => ms.Id == id);
            var listIndex = MockDb.EMails.IndexOf(eMailToDelete);
            if (listIndex > -1)
                MockDb.EMails.RemoveAt(listIndex);
            
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
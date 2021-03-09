using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class EMailTypeDal : IEMailTypeDal
    {
        public async Task<EMailType> Fetch(int id)
        {
            var email = MockDb.EMailTypes.FirstOrDefault(ms => ms.Id == id);
            return email;
        }

        public async Task<List<EMailType>> Fetch()
        {
            return MockDb.EMailTypes.ToList();
        }

        public async Task<EMailType> Insert( EMailType eMailType)
        {
            var lastEMailType = MockDb.EMailTypes.ToList().OrderByDescending(ms => ms.Id).First();
            eMailType.Id = 1+lastEMailType.Id;
            eMailType.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            
            MockDb.EMailTypes.Add(eMailType);
            
            return eMailType;
        }

        public async Task<EMailType> Update(EMailType eMailType)
        {
            var eMailTypeToUpdate =
                MockDb.EMailTypes.FirstOrDefault(em => em.Id == eMailType.Id &&
                                                               em.RowVersion.SequenceEqual(eMailType.RowVersion));

            if(eMailTypeToUpdate == null)
                throw new Csla.DataPortalException(null);
           
            eMailTypeToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return eMailTypeToUpdate;
        }

        public async Task Delete(int id)
        {
            var eMailTypeToDelete = MockDb.EMailTypes.FirstOrDefault(ms => ms.Id == id);
            var listIndex = MockDb.EMailTypes.IndexOf(eMailTypeToDelete);
            if(listIndex > -1)
                MockDb.EMailTypes.RemoveAt(listIndex);
        }

        public void Dispose()
        {
        }
    }
}
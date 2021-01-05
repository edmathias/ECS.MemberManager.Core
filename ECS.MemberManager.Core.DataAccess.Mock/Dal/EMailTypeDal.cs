using System;
using System.Collections.Generic;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class EMailTypeDal : IEMailTypeDal
    {
        public EMailType Fetch(int id)
        {
            return MockDb.EMailTypes.FirstOrDefault(ms => ms.Id == id);
        }

        public List<EMailType> Fetch()
        {
            return MockDb.EMailTypes.ToList();
        }

        public EMailType Insert( EMailType eMailType)
        {
            var lastEMailType = MockDb.EMailTypes.ToList().OrderByDescending(ms => ms.Id).First();
            eMailType.Id = 1+lastEMailType.Id;
            MockDb.EMailTypes.Add(eMailType);
            
            return eMailType;
        }

        public EMailType Update(EMailType eMailType)
        {
            // mockdb in memory list reference already updated
            return eMailType;
        }

        public void Delete(int id)
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
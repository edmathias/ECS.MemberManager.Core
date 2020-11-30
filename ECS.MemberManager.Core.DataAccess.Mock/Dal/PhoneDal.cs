using System;
using System.Collections.Generic;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class PhoneDal : IPhoneDal
    {
        public Phone Fetch(int id)
        {
            return MockDb.Phones.FirstOrDefault(dt => dt.Id == id);
        }

        public List<Phone> Fetch()
        {
            return MockDb.Phones.ToList();
        }

        public int Insert( Phone documentType)
        {
            var lastPhone = MockDb.Phones.ToList().OrderByDescending(dt => dt.Id).First();
            documentType.Id = ++lastPhone.Id;
            MockDb.Phones.Add(documentType);
            
            return documentType.Id;
        }

        public void Update(Phone documentType)
        {
            // mockdb in memory list ref already updated.
        }

        public void Delete(int id)
        {
            var documentTypesToDelete = MockDb.Phones.FirstOrDefault(dt => dt.Id == id);
            var listIndex = MockDb.Phones.IndexOf(documentTypesToDelete);
            if(listIndex > -1)
                MockDb.Phones.RemoveAt(listIndex);
        }

        public void Dispose()
        {
        }
    }
}
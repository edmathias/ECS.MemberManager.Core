using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class PhoneDal : IPhoneDal
    {
        public async Task<Phone> Fetch(int id)
        {
            return MockDb.Phones.FirstOrDefault(dt => dt.Id == id);
        }

        public async Task<List<Phone>> Fetch()
        {
            return MockDb.Phones.ToList();
        }

        public async Task<Phone> Insert( Phone documentType)
        {
            var lastPhone = MockDb.Phones.ToList().OrderByDescending(dt => dt.Id).First();
            documentType.Id = 1+lastPhone.Id;
            MockDb.Phones.Add(documentType);
            
            return documentType;
        }

        public async Task<Phone> Update(Phone phone)
        {
            return phone;
        }

        public async Task Delete(int id)
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
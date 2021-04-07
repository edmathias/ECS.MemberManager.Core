using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class PhoneDal : IDal<Phone>
    {
        public async Task<Phone> Fetch(int id)
        {
            return MockDb.Phones.FirstOrDefault(dt => dt.Id == id);
        }

        public async Task<List<Phone>> Fetch()
        {
            return MockDb.Phones.ToList();
        }

        public async Task<Phone> Insert(Phone phone)
        {
            var lastPhone = MockDb.Phones.ToList().OrderByDescending(dt => dt.Id).First();
            phone.Id = 1 + lastPhone.Id;
            phone.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.Phones.Add(phone);

            return phone;
        }

        public async Task<Phone> Update(Phone phone)
        {
            var phoneToUpdate =
                MockDb.Phones.FirstOrDefault(em => em.Id == phone.Id &&
                                                   em.RowVersion.SequenceEqual(phone.RowVersion));

            if (phoneToUpdate == null)
                throw new Csla.DataPortalException(null);

            phoneToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return phoneToUpdate;
        }

        public async Task Delete(int id)
        {
            var documentTypesToDelete = MockDb.Phones.FirstOrDefault(dt => dt.Id == id);
            var listIndex = MockDb.Phones.IndexOf(documentTypesToDelete);
            if (listIndex > -1)
                MockDb.Phones.RemoveAt(listIndex);
        }

        public void Dispose()
        {
        }
    }
}
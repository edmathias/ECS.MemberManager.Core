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
        public Task<Phone> Fetch(int id)
        {
            return Task.FromResult(MockDb.Phones.FirstOrDefault(dt => dt.Id == id));
        }

        public Task<List<Phone>> Fetch()
        {
            return Task.FromResult(MockDb.Phones.ToList());
        }

        public Task<Phone> Insert(Phone phone)
        {
            var lastPhone = MockDb.Phones.ToList().OrderByDescending(dt => dt.Id).First();
            phone.Id = 1 + lastPhone.Id;
            phone.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.Phones.Add(phone);

            return Task.FromResult(phone);
        }

        public Task<Phone> Update(Phone phone)
        {
            var phoneToUpdate =
                MockDb.Phones.FirstOrDefault(em => em.Id == phone.Id &&
                                                   em.RowVersion.SequenceEqual(phone.RowVersion));

            if (phoneToUpdate == null)
                throw new Csla.DataPortalException(null);

            phoneToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(phoneToUpdate);
        }

        public Task Delete(int id)
        {
            var documentTypesToDelete = MockDb.Phones.FirstOrDefault(dt => dt.Id == id);
            var listIndex = MockDb.Phones.IndexOf(documentTypesToDelete);
            if (listIndex > -1)
                MockDb.Phones.RemoveAt(listIndex);
            
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
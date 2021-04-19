using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class OfficeDal : IDal<Office>
    {
        public Task<Office> Fetch(int id)
        {
            return Task.FromResult(MockDb.Offices.FirstOrDefault(o => o.Id == id));
        }

        public Task<List<Office>> Fetch()
        {
            return Task.FromResult(MockDb.Offices.ToList());
        }

        public Task<Office> Insert(Office office)
        {
            var lastOffice = MockDb.Offices.ToList().OrderByDescending(o => o.Id).First();
            office.Id = 1 + lastOffice.Id;
            office.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.Offices.Add(office);

            return Task.FromResult(office);
        }

        public Task<Office> Update(Office office)
        {
            var officeToUpdate =
                MockDb.Offices.FirstOrDefault(em => em.Id == office.Id &&
                                                    em.RowVersion.SequenceEqual(office.RowVersion));

            if (officeToUpdate == null)
                throw new Csla.DataPortalException(null);

            officeToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(officeToUpdate);
        }

        public Task Delete(int id)
        {
            var officeToRemove = MockDb.Offices.FirstOrDefault(o => o.Id == id);
            if (officeToRemove == null)
                throw new Exception("Office record not found - Delete");

            MockDb.Offices.Remove(officeToRemove);
            
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
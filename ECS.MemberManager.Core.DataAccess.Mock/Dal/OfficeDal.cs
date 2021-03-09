using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class OfficeDal : IOfficeDal
    {
        public async Task<Office> Fetch(int id)
        {
            return MockDb.Offices.FirstOrDefault(o => o.Id == id);
        }

        public async Task<List<Office>> Fetch()
        {
            return MockDb.Offices.ToList();
        }

        public async Task<Office> Insert(Office office)
        {
            var lastOffice = MockDb.Offices.ToList().OrderByDescending(o => o.Id).First();
            office.Id = 1+lastOffice.Id;
            office.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            
            MockDb.Offices.Add(office);
            
            return office;
        }

        public async Task<Office> Update(Office office)
        {
            var officeToUpdate =
                MockDb.Offices.FirstOrDefault(em => em.Id == office.Id &&
                                                    em.RowVersion.SequenceEqual(office.RowVersion));

            if(officeToUpdate == null)
                throw new Csla.DataPortalException(null);
           
            officeToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return officeToUpdate;
        }

        public async Task Delete(int id)
        {
            var officeToRemove = MockDb.Offices.FirstOrDefault(o => o.Id == id);
            if (officeToRemove == null)
                throw new Exception("Office record not found - Delete");

            MockDb.Offices.Remove(officeToRemove);
        }
    }
}
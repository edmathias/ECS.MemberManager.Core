using System;
using System.Collections.Generic;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class OfficeDal : IOfficeDal
    {
        public Office Fetch(int id)
        {
            return MockDb.Offices.FirstOrDefault(o => o.Id == id);
        }

        public List<Office> Fetch()
        {
            return MockDb.Offices.ToList();
        }

        public int Insert(Office office)
        {
            var lastOffice = MockDb.Offices.ToList().OrderByDescending(o => o.Id).First();
            
            office.Id = 1+lastOffice.Id;
            MockDb.Offices.Add(office);
            
            return office.Id;
        }

        public void Update(Office office)
        {
            // in memory database ref is already updated

        }

        public void Delete(int id)
        {
            var officeToRemove = MockDb.Offices.FirstOrDefault(o => o.Id == id);
            if (officeToRemove == null)
                throw new Exception("Office record not found - Delete");

            MockDb.Offices.Remove(officeToRemove);
        }
    }
}
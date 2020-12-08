using System;
using System.Collections.Generic;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class OrganizationDal : IOrganizationDal
    
    {
        public Organization Fetch(int id)
        {
            return MockDb.Organizations.FirstOrDefault(ms => ms.Id == id);
        }

        public List<Organization> Fetch()
        {
            return MockDb.Organizations.ToList();
        }

        public int Insert( Organization organizationToInsert)
        {
            var lastOrganization = MockDb.Organizations.ToList().OrderByDescending(ms => ms.Id).First();
            organizationToInsert.Id = ++lastOrganization.Id;
            MockDb.Organizations.Add(organizationToInsert);
            
            return organizationToInsert.Id;
        }

        public int Update(Organization eMail)
        {
            // mockdb in memory list reference already updated 
            return 1;
        }

        public void Delete(int id)
        {
            var eMailToDelete = MockDb.Organizations.FirstOrDefault(ms => ms.Id == id);
            var listIndex = MockDb.Organizations.IndexOf(eMailToDelete);
            if(listIndex > -1)
                MockDb.Organizations.RemoveAt(listIndex);
        }

        public void Dispose()
        {
        }
    }
}
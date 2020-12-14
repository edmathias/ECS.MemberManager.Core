using System.Collections.Generic;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class OrganizationTypeDal : IOrganizationTypeDal
    {
        public OrganizationType Fetch(int id)
        {
            return MockDb.OrganizationTypes.FirstOrDefault(ms => ms.Id == id);
        }

        public List<OrganizationType> Fetch()
        {
            return MockDb.OrganizationTypes.ToList();
        }

        public int Insert(OrganizationType organizationType)
        {
            var lastOrganizationType = MockDb.OrganizationTypes.ToList().OrderByDescending(ms => ms.Id).First();
            organizationType.Id = 1+lastOrganizationType.Id;
            MockDb.OrganizationTypes.Add(organizationType);
            
            return organizationType.Id;
        }

        public void Update(OrganizationType organizationType)
        {
            // mockdb in memory list reference already updated 
        }

        public void Delete(int id)
        {
            var organizationTypeToDelete = MockDb.OrganizationTypes.FirstOrDefault(ms => ms.Id == id);
            var listIndex = MockDb.OrganizationTypes.IndexOf(organizationTypeToDelete);
            if(listIndex > -1)
                MockDb.OrganizationTypes.RemoveAt(listIndex);
        }
    }
}
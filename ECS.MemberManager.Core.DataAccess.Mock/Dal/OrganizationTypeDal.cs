using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class OrganizationTypeDal : IOrganizationTypeDal
    {
        public async Task<OrganizationType> Fetch(int id)
        {
            return MockDb.OrganizationTypes.FirstOrDefault(ms => ms.Id == id);
        }

        public async Task<List<OrganizationType>> Fetch()
        {
            return MockDb.OrganizationTypes.ToList();
        }

        public async Task<OrganizationType> Insert(OrganizationType organizationType)
        {
            var lastOrganizationType = MockDb.OrganizationTypes.ToList().OrderByDescending(ms => ms.Id).First();
            organizationType.Id = 1+lastOrganizationType.Id;
            organizationType.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            
            MockDb.OrganizationTypes.Add(organizationType);
            
            return organizationType;
        }

        public async Task<OrganizationType> Update(OrganizationType organizationType)
        {
            var organizationTypeToUpdate =
                MockDb.OrganizationTypes.FirstOrDefault(em => em.Id == organizationType.Id &&
                                                          em.RowVersion.SequenceEqual(organizationType.RowVersion));

            if(organizationTypeToUpdate == null)
                throw new Csla.DataPortalException(null);
           
            organizationTypeToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return organizationTypeToUpdate;
            
        }

        public async Task Delete(int id)
        {
            var organizationTypeToDelete = MockDb.OrganizationTypes.FirstOrDefault(ms => ms.Id == id);
            var listIndex = MockDb.OrganizationTypes.IndexOf(organizationTypeToDelete);
            if(listIndex > -1)
                MockDb.OrganizationTypes.RemoveAt(listIndex);
        }
    }
}
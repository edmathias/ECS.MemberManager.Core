using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class OrganizationTypeDal : IDal<OrganizationType>
    {
        public Task<OrganizationType> Fetch(int id)
        {
            return Task.FromResult(MockDb.OrganizationTypes.FirstOrDefault(ms => ms.Id == id));
        }

        public Task<List<OrganizationType>> Fetch()
        {
            return Task.FromResult(MockDb.OrganizationTypes.ToList());
        }

        public Task<OrganizationType> Insert(OrganizationType organizationType)
        {
            var lastOrganizationType = MockDb.OrganizationTypes.ToList().OrderByDescending(ms => ms.Id).First();
            organizationType.Id = 1 + lastOrganizationType.Id;
            organizationType.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.OrganizationTypes.Add(organizationType);

            return Task.FromResult(organizationType);
        }

        public Task<OrganizationType> Update(OrganizationType organizationType)
        {
            var organizationTypeToUpdate =
                MockDb.OrganizationTypes.FirstOrDefault(em => em.Id == organizationType.Id &&
                                                              em.RowVersion.SequenceEqual(organizationType.RowVersion));

            if (organizationTypeToUpdate == null)
                throw new Csla.DataPortalException(null);

            organizationTypeToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(organizationTypeToUpdate);
        }

        public Task Delete(int id)
        {
            var organizationTypeToDelete = MockDb.OrganizationTypes.FirstOrDefault(ms => ms.Id == id);
            var listIndex = MockDb.OrganizationTypes.IndexOf(organizationTypeToDelete);
            if (listIndex > -1)
                MockDb.OrganizationTypes.RemoveAt(listIndex);
            
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
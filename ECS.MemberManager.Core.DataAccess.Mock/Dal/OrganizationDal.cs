using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class OrganizationDal : IDal<Organization>
    {
        public async Task<Organization> Fetch(int id)
        {
            return MockDb.Organizations.FirstOrDefault(ms => ms.Id == id);
        }

        public async Task<List<Organization>> Fetch()
        {
            return MockDb.Organizations.ToList();
        }

        public async Task<Organization> Insert(Organization organizationToInsert)
        {
            var lastOrganization = MockDb.Organizations.ToList().OrderByDescending(ms => ms.Id).First();
            organizationToInsert.Id = 1 + lastOrganization.Id;
            organizationToInsert.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.Organizations.Add(organizationToInsert);

            return organizationToInsert;
        }

        public async Task<Organization> Update(Organization organization)
        {
            var organizationToUpdate =
                MockDb.Organizations.FirstOrDefault(em => em.Id == organization.Id &&
                                                          em.RowVersion.SequenceEqual(organization.RowVersion));

            if (organizationToUpdate == null)
                throw new Csla.DataPortalException(null);

            organizationToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return organizationToUpdate;
        }

        public async Task Delete(int id)
        {
            var eMailToDelete = MockDb.Organizations.FirstOrDefault(ms => ms.Id == id);
            var listIndex = MockDb.Organizations.IndexOf(eMailToDelete);
            if (listIndex > -1)
                MockDb.Organizations.RemoveAt(listIndex);
        }

        public void Dispose()
        {
        }
    }
}
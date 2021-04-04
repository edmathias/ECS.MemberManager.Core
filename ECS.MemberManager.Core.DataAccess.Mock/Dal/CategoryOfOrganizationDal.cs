using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class CategoryOfOrganizationDal : IDal<CategoryOfOrganization>
    {
        public void Dispose()
        {
        }

        public async Task<CategoryOfOrganization> Fetch(int id)
        {
            return MockDb.CategoryOfOrganizations.FirstOrDefault(co => co.Id == id);
        }

        public async Task<List<CategoryOfOrganization>> Fetch()
        {
            return MockDb.CategoryOfOrganizations.ToList();
        }

        public async Task<CategoryOfOrganization> Insert(CategoryOfOrganization categoryOfOrganization)
        {
            var lastCategory = MockDb.CategoryOfOrganizations.ToList().OrderByDescending(co => co.Id).First();
            categoryOfOrganization.Id = lastCategory.Id + 1;
            categoryOfOrganization.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            MockDb.CategoryOfOrganizations.Add(categoryOfOrganization);

            return categoryOfOrganization;
        }

        public async Task<CategoryOfOrganization> Update(CategoryOfOrganization categoryOfOrganization)
        {
            var savedCategoryOfOrganization =
                MockDb.CategoryOfOrganizations.FirstOrDefault(em => em.Id == categoryOfOrganization.Id &&
                                                                    em.RowVersion.SequenceEqual(categoryOfOrganization
                                                                        .RowVersion));

            if (savedCategoryOfOrganization == null)
                throw new Csla.DataPortalException(null);

            savedCategoryOfOrganization.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return savedCategoryOfOrganization;
        }

        public async Task Delete(int id)
        {
            var categoryToDelete = MockDb.CategoryOfOrganizations.FirstOrDefault(co => co.Id == id);
            var listIndex = MockDb.CategoryOfOrganizations.IndexOf(categoryToDelete);
            if (listIndex > -1)
                MockDb.CategoryOfOrganizations.RemoveAt(listIndex);
        }
    }
}
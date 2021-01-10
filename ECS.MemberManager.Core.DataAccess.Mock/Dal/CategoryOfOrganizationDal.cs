using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class CategoryOfOrganizationDal : ICategoryOfOrganizationDal
    {
        public void Dispose()
        {
            
        }

        public CategoryOfOrganization Fetch(int id)
        {
            return MockDb.CategoryOfOrganizations.FirstOrDefault(co => co.Id == id);
        }

        public async Task<List<CategoryOfOrganization>> Fetch()
        {
            return MockDb.CategoryOfOrganizations.ToList();
        }

        public CategoryOfOrganization Insert(CategoryOfOrganization categoryOfOrganization)
        {
            var lastCategory = MockDb.CategoryOfOrganizations.ToList().OrderByDescending( co => co.Id).First();
            categoryOfOrganization.Id = lastCategory.Id + 1;
            MockDb.CategoryOfOrganizations.Add(categoryOfOrganization);
            
            return categoryOfOrganization;
        }

        public CategoryOfOrganization Update(CategoryOfOrganization categoryOfOrganization)
        {
            var categoryToUpdate = MockDb.CategoryOfOrganizations.FirstOrDefault(co => co.Id == categoryOfOrganization.Id);

            if (categoryToUpdate == null) 
                throw new Exception("Record not found");

            categoryToUpdate.Category = categoryOfOrganization.Category;
            categoryToUpdate.DisplayOrder = categoryOfOrganization.DisplayOrder;

            return categoryToUpdate;
        }

        public void Delete(int id)
        {
            var categoryToDelete = MockDb.CategoryOfOrganizations.FirstOrDefault(co => co.Id == id);
            var listIndex = MockDb.CategoryOfOrganizations.IndexOf(categoryToDelete);
            if(listIndex > -1)
                MockDb.CategoryOfOrganizations.RemoveAt(listIndex);
        }
    }
}
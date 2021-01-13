using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class CategoryOfPersonDal : ICategoryOfPersonDal
    {
        public void Dispose()
        {
        }

        public async Task<CategoryOfPerson> Fetch(int id)
        {
            return MockDb.CategoryOfPersons.FirstOrDefault(co => co.Id == id);
        }

        public async Task<List<CategoryOfPerson>> Fetch()
        {
            return MockDb.CategoryOfPersons.ToList();
        }

        public async Task<CategoryOfPerson> Insert(CategoryOfPerson categoryOfPerson)
        {
            var lastCategory = MockDb.CategoryOfPersons.ToList().OrderByDescending( co => co.Id).First();
            categoryOfPerson.Id = lastCategory.Id + 1;
            MockDb.CategoryOfPersons.Add(categoryOfPerson);
            
            return categoryOfPerson;
        }

        public async Task<CategoryOfPerson> Update(CategoryOfPerson categoryOfPerson)
        {
            var categoryToUpdate = MockDb.CategoryOfPersons.FirstOrDefault(co => co.Id == categoryOfPerson.Id);

            if (categoryToUpdate == null) 
                throw new Exception("Record not found");

            categoryToUpdate.Category = categoryOfPerson.Category;
            categoryToUpdate.DisplayOrder = categoryOfPerson.DisplayOrder;

            return categoryToUpdate;
        }

        public async Task Delete(int id)
        {
            var categoryToDelete = MockDb.CategoryOfPersons.FirstOrDefault(co => co.Id == id);
            var listIndex = MockDb.CategoryOfPersons.IndexOf(categoryToDelete);
            if(listIndex > -1)
                MockDb.CategoryOfPersons.RemoveAt(listIndex);
        }
    }
}
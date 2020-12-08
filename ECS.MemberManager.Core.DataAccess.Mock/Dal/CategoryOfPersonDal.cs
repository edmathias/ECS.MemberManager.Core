using System;
using System.Collections.Generic;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class CategoryOfPersonDal : ICategoryOfPersonDal
    {
        public void Dispose()
        {
        }

        public CategoryOfPerson Fetch(int id)
        {
            return MockDb.CategoryOfPersons.FirstOrDefault(co => co.Id == id);
        }

        public List<CategoryOfPerson> Fetch()
        {
            return MockDb.CategoryOfPersons.ToList();
        }

        public int Insert(CategoryOfPerson categoryOfPerson)
        {
            var lastCategory = MockDb.CategoryOfPersons.ToList().OrderByDescending( co => co.Id).First();
            categoryOfPerson.Id = ++lastCategory.Id;
            MockDb.CategoryOfPersons.Add(categoryOfPerson);
            
            return categoryOfPerson.Id;
        }

        public void Update(CategoryOfPerson categoryOfPerson)
        {
            var categoryToUpdate = MockDb.CategoryOfPersons.FirstOrDefault(co => co.Id == categoryOfPerson.Id);

            if (categoryToUpdate == null) 
                throw new Exception("Record not found");

            categoryToUpdate.Category = categoryOfPerson.Category;
            categoryToUpdate.DisplayOrder = categoryOfPerson.DisplayOrder;
        }

        public void Delete(int id)
        {
            var categoryToDelete = MockDb.CategoryOfPersons.FirstOrDefault(co => co.Id == id);
            var listIndex = MockDb.CategoryOfPersons.IndexOf(categoryToDelete);
            if(listIndex > -1)
                MockDb.CategoryOfPersons.RemoveAt(listIndex);
        }
    }
}
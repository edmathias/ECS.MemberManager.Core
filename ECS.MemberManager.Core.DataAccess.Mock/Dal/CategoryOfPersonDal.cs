using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class CategoryOfPersonDal : IDal<CategoryOfPerson>
    {
        public void Dispose()
        {
        }

        public Task<CategoryOfPerson> Fetch(int id)
        {
            return Task.FromResult(MockDb.CategoryOfPersons.FirstOrDefault(co => co.Id == id));
        }

        public Task<List<CategoryOfPerson>> Fetch()
        {
            return Task.FromResult(MockDb.CategoryOfPersons.ToList());
        }

        public Task<CategoryOfPerson> Insert(CategoryOfPerson categoryOfPerson)
        {
            var lastCategory = MockDb.CategoryOfPersons.ToList().OrderByDescending(co => co.Id).First();
            categoryOfPerson.Id = lastCategory.Id + 1;
            categoryOfPerson.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            MockDb.CategoryOfPersons.Add(categoryOfPerson);

            return Task.FromResult(categoryOfPerson);
        }

        public Task<CategoryOfPerson> Update(CategoryOfPerson categoryOfPerson)
        {
            var savedCategoryOfPerson =
                MockDb.CategoryOfPersons.FirstOrDefault(em => em.Id == categoryOfPerson.Id &&
                                                              em.RowVersion.SequenceEqual(categoryOfPerson.RowVersion));

            if (savedCategoryOfPerson == null)
                throw new Csla.DataPortalException(null);

            savedCategoryOfPerson.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(savedCategoryOfPerson);
        }

        public Task Delete(int id)
        {
            var categoryToDelete = MockDb.CategoryOfPersons.FirstOrDefault(co => co.Id == id);
            var listIndex = MockDb.CategoryOfPersons.IndexOf(categoryToDelete);
            if (listIndex > -1)
                MockDb.CategoryOfPersons.RemoveAt(listIndex);
            
            return Task.CompletedTask;
        }
    }
}
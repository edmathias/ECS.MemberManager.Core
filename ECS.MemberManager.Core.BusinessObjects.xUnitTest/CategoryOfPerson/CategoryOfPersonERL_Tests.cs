using System;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfPersonERL_Tests
    {
        [Fact]
        private async void TestCategoryOfPersonERL_GetCategoryOfPersonList()
        {
            var listToTest = MockDb.CategoryOfPersons;
            
            var personErl = await CategoryOfPersonERL.GetCategoryOfPersonList(listToTest);

            Assert.NotNull(personErl);
            Assert.Equal(MockDb.CategoryOfPersons.Count, personErl.Count);
        }
        
        [Fact]
        private async void TestCategoryOfPersonERL_DeleteCategoryOfPersonEntry()
        {
            var listToTest = MockDb.CategoryOfPersons;
            var idToDelete = MockDb.CategoryOfPersons.Max(a => a.Id);
            
            var personErl = await CategoryOfPersonERL.GetCategoryOfPersonList(listToTest);

            var person = personErl.First(a => a.Id == idToDelete);

            // remove is deferred delete
            personErl.Remove(person); 

            var personListAfterDelete = await personErl.SaveAsync();
            
            Assert.NotNull(personListAfterDelete);
            Assert.Null(personListAfterDelete.FirstOrDefault(pl => pl.Id == idToDelete));
        }
        
    }
}
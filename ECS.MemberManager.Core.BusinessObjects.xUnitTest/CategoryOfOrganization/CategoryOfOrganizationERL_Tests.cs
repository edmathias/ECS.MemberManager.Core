using System;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CategoryOfOrganizationERL_Tests
    {
        public CategoryOfOrganizationERL_Tests()
        {
            MockDb.ResetMockDb();
        }
        
        [Fact]
        private async void TestCategoryOfOrganizationERL_GetCategoryOfOrganizationList()
        {
            var listToTest = MockDb.CategoryOfOrganizations;
            
            var addressErl = await CategoryOfOrganizationERL.GetCategoryOfOrganizationList(listToTest);

            Assert.NotNull(addressErl);
            Assert.Equal(MockDb.CategoryOfOrganizations.Count, addressErl.Count);
        }
        
        [Fact]
        private async void TestCategoryOfOrganizationERL_DeleteCategoryOfOrganizationEntry()
        {
            var listToTest = MockDb.CategoryOfOrganizations;
            var idToDelete = MockDb.CategoryOfOrganizations.Max(a => a.Id);
            
            var addressErl = await CategoryOfOrganizationERL.GetCategoryOfOrganizationList(listToTest);

            var address = addressErl.First(a => a.Id == idToDelete);

            // remove is deferred delete
            addressErl.Remove(address); 

            var addressListAfterDelete = await addressErl.SaveAsync();
            
            Assert.NotNull(addressListAfterDelete);
        }
        
    }
}
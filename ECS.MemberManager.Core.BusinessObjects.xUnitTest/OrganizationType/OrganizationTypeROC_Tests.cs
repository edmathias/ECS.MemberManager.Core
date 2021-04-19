using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class OrganizationTypeROC_Tests : CslaBaseTest
    {
        [Fact]
        public async void OrganizationTypeROC_TestGetById()
        {
            var categoryToTest = BuildOrganizationType();
            var category = await OrganizationTypeROC.GetOrganizationTypeROC(categoryToTest);

            Assert.NotNull(category);
            Assert.IsType<OrganizationTypeROC>(category);
            Assert.Equal(categoryToTest.Id, category.Id);
            Assert.Equal(categoryToTest.Name, category.Name);
            Assert.Equal(categoryToTest.Notes, category.Notes);
        }

        private OrganizationType BuildOrganizationType()
        {
            var category = new OrganizationType()
            {
                Id = 1,
                Name = "org Name 1",
                Notes = "organization notes"
            };

            return category;
        }
    }
}
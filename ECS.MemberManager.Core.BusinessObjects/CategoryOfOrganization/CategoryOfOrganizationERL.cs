using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class CategoryOfOrganizationERL : BusinessListBase<CategoryOfOrganizationERL,CategoryOfOrganizationEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<CategoryOfOrganizationERL> NewCategoryOfOrganizationECL()
        {
            return await DataPortal.CreateAsync<CategoryOfOrganizationERL>();
        }

        public static async Task<CategoryOfOrganizationERL> GetCategoryOfOrganizationECL(List<CategoryOfOrganization> childData)
        {
            return await DataPortal.FetchAsync<CategoryOfOrganizationERL>(childData);
        }
        
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();
            var childData = await dal.Fetch();

            await Fetch(childData);

        }

        [Fetch]
        private async Task Fetch(List<CategoryOfOrganization> childData)
        {
            using (LoadListMode)
            {
                foreach (var categoryOfOrganization in childData)
                {
                    var categoryOfOrganizationToAdd = 
                        await CategoryOfOrganizationEC.GetCategoryOfOrganizationEC(categoryOfOrganization);
                    Add(categoryOfOrganizationToAdd);
                }
            }
        }
        
        [Update]
        private void Update()
        {
            Child_Update();
        }
    }
}
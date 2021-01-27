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
    public class CategoryOfOrganizationECL : BusinessListBase<CategoryOfOrganizationECL,CategoryOfOrganizationEC>
    {
        #region Business Methods
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
        
        #region Factory Methods
 
        public static async Task<CategoryOfOrganizationECL> NewCategoryOfOrganizationECL()
        {
            return await DataPortal.CreateAsync<CategoryOfOrganizationECL>();
        }

        public static async Task<CategoryOfOrganizationECL> GetCategoryOfOrganizationECL(List<CategoryOfOrganization> childData)
        {
            return await DataPortal.FetchAsync<CategoryOfOrganizationECL>(childData);
        }
        
        #endregion
        
        #region Data Access
        
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
        
        #endregion
    }
}
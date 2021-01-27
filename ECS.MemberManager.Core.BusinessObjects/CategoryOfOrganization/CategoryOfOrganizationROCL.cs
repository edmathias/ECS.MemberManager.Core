using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class CategoryOfOrganizationROCL : ReadOnlyListBase<CategoryOfOrganizationROCL, CategoryOfOrganizationROC>
    {
        
        #region Business Rules
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
        
        #region Factory Methods
        
        public static async Task<CategoryOfOrganizationROCL> GetCategoryOfOrganizationROCL(List<CategoryOfOrganization> childData)
        {
            return await DataPortal.FetchChildAsync<CategoryOfOrganizationROCL>(childData);
        }
        
        #endregion
        
        #region Data Access

        [FetchChild]
        private async Task Fetch(List<CategoryOfOrganization> childData)
        {
            using (LoadListMode)
            {
                foreach (var category in childData)
                {
                    var categoryToAdd = await
                        CategoryOfOrganizationROC.GetCategoryOfOrganizationROC(category); 
                    Add(categoryToAdd);
                }
            }
        }
        
        #endregion
    }
}
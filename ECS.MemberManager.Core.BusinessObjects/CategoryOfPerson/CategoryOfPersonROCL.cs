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
    public class CategoryOfPersonROCL : ReadOnlyListBase<CategoryOfPersonROCL, CategoryOfPersonROC>
    {
        #region Business Rules
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }
        
        #endregion

        #region Factory Methods 
        
        internal static async Task<CategoryOfPersonROCL> GetCategoryOfPersonROCL(IList<CategoryOfPerson> childData)
        {
            return await DataPortal.FetchChildAsync<CategoryOfPersonROCL>(childData);
        }        
       
        #endregion 
        
        #region Data Access
        
        [FetchChild]
        private async Task FetchChild(IList<CategoryOfPerson> childData)
        {
            using (LoadListMode)
            {
                foreach (var categoryOfPersonData in childData)
                {
                    var category = await
                        CategoryOfPersonROC.GetCategoryOfPersonROC(categoryOfPersonData); 
                    Add(category);
                }
            }
        }
        
        #endregion
    }
}
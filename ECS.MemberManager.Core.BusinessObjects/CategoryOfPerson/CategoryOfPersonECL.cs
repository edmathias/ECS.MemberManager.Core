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
    public class CategoryOfPersonECL : BusinessListBase<CategoryOfPersonECL,CategoryOfPersonEC>
    {
        #region Business Methods
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
        
        #region Factory Methods
        
        public static async Task<CategoryOfPersonECL> NewCategoryOfPersonECL()
        {
            return await DataPortal.CreateAsync<CategoryOfPersonECL>();
        }

        public static async Task<CategoryOfPersonECL> GetCategoryOfPersonECL(List<CategoryOfPerson> childData)
        {
            return await DataPortal.FetchAsync<CategoryOfPersonECL>(childData);
        }
        
        #endregion

        #region Data Access
        
        [Fetch]
        private async Task Fetch(List<CategoryOfPerson> childData)
        {
            using (LoadListMode)
            {
                foreach (var categoryOfPerson in childData)
                {
                    var categoryOfPersonToAdd = 
                        await CategoryOfPersonEC.GetCategoryOfPersonEC(categoryOfPerson);
                    Add(categoryOfPersonToAdd);
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
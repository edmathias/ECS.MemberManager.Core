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
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<CategoryOfPersonROCL> GetCategoryOfPersonROCL()
        {
            return await DataPortal.FetchAsync<CategoryOfPersonROCL>();
        }        
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfPersonDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var categoryOfPersonData in childData)
                {
                    var category = await
                        CategoryOfPersonROC.GetCategoryOfPersonROC(categoryOfPersonData); 
                    this.Add(category);
                }
            }
        }
    }
}
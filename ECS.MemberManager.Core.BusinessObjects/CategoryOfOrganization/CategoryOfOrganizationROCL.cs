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
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<CategoryOfOrganizationROCL> GetCategoryOfOrganizationInfoChildList()
        {
            return await DataPortal.FetchAsync<CategoryOfOrganizationROCL>();
        }

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var categoryOfOrganizationData in childData)
                {
                    var category = await
                        CategoryOfOrganizationROC.GetCategoryOfOrganizationROC(categoryOfOrganizationData); 
                    this.Add(category);
                }
            }
        }
    }
}
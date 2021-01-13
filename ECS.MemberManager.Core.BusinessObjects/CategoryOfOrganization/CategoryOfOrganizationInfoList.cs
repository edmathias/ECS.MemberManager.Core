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
    public class CategoryOfOrganizationInfoList : ReadOnlyListBase<CategoryOfOrganizationInfoList, CategoryOfOrganizationInfo>
    {
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<CategoryOfOrganizationInfoList> GetCategoryOfOrganizationInfoList(IList<CategoryOfOrganization> listOfChildren)
        {
            return await DataPortal.FetchAsync<CategoryOfOrganizationInfoList>(listOfChildren);
        }
        
        public static async Task<CategoryOfOrganizationInfoList> GetCategoryOfOrganizationInfoList()
        {
            return await DataPortal.FetchAsync<CategoryOfOrganizationInfoList>();
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
        private async Task Fetch(IList<CategoryOfOrganization> listOfChildren)
        {
            using (LoadListMode)
            {
                foreach (var categoryOfOrganizationData in listOfChildren)
                {
                    var category = await
                        CategoryOfOrganizationInfo.GetCategoryOfOrganizationInfo(categoryOfOrganizationData); 
                    this.Add(category);
                }
            }
        }
    }
}
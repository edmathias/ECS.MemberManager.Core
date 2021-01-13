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
    public class CategoryOfPersonInfoList : ReadOnlyListBase<CategoryOfPersonInfoList, CategoryOfPersonInfo>
    {
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<CategoryOfPersonInfoList> GetCategoryOfPersonInfoList(IList<CategoryOfPerson> listOfChildren)
        {
            return await DataPortal.FetchAsync<CategoryOfPersonInfoList>(listOfChildren);
        }
        
        public static async Task<CategoryOfPersonInfoList> GetCategoryOfPersonInfoList()
        {
            return await DataPortal.FetchAsync<CategoryOfPersonInfoList>();
        }        
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfPersonDal>();
            var childData = await dal.Fetch();

            await Fetch(childData);
        }

        [Fetch]
        private async Task Fetch(IList<CategoryOfPerson> listOfChildren)
        {
            using (LoadListMode)
            {
                foreach (var categoryOfPersonData in listOfChildren)
                {
                    var category = await
                        CategoryOfPersonInfo.GetCategoryOfPersonInfo(categoryOfPersonData); 
                    this.Add(category);
                }
            }
        }
    }
}
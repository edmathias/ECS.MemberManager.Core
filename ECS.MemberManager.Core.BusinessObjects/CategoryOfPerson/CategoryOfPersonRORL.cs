using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class CategoryOfPersonRORL : ReadOnlyListBase<CategoryOfPersonRORL,CategoryOfPersonROC>
    {
        #region Business Methods
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }
        
        #endregion
        
        #region Factory Methods

        public static async Task<CategoryOfPersonRORL> GetCategoryOfPersonRORL()
        {
            return await DataPortal.FetchAsync<CategoryOfPersonRORL>();
        }
        
        #endregion
        
        #region Data Access

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfPersonDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var address in childData)
                {
                    var addressToAdd = await CategoryOfPersonROC.GetCategoryOfPersonROC(address);
                    Add(addressToAdd);
                }
            }
        }
        
        #endregion
    }
}
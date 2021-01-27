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
    public class CategoryOfOrganizationRORL : ReadOnlyListBase<CategoryOfOrganizationRORL,CategoryOfOrganizationROC>
    {
        #region Business Methods
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }
        
        #endregion
        
        #region Factory Methods

        public static async Task<CategoryOfOrganizationRORL> GetCategoryOfOrganizationRORL()
        {
            return await DataPortal.FetchAsync<CategoryOfOrganizationRORL>();
        }
        
        #endregion
        
        #region Data Access

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var address in childData)
                {
                    var addressToAdd = await CategoryOfOrganizationROC.GetCategoryOfOrganizationROC(address);
                    Add(addressToAdd);
                }
            }
        }
        
        #endregion
    }
}
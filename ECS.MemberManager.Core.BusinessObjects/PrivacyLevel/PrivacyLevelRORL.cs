using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PrivacyLevelRORL : ReadOnlyListBase<PrivacyLevelRORL,PrivacyLevelROC>
    {
        #region Business Methods
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }
        
        #endregion
        
        #region Factory Methods

        public static async Task<PrivacyLevelRORL> GetPrivacyLevelRORL()
        {
            return await DataPortal.FetchAsync<PrivacyLevelRORL>();
        }
        
        #endregion
        
        #region Data Access

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPrivacyLevelDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var privacyLevel in childData)
                {
                    var privacyLevelToAdd = await PrivacyLevelROC.GetPrivacyLevelROC(privacyLevel);
                    Add(privacyLevelToAdd);
                }
            }
        }
        
        #endregion
    }
}
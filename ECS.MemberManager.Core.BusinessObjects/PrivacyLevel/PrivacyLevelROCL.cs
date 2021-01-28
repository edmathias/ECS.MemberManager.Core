using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PrivacyLevelROCL : ReadOnlyListBase<PrivacyLevelROCL,PrivacyLevelROC>
    {
        #region Business Rules
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
        
        #region Factory Methods
        
        internal static async Task<PrivacyLevelROCL> GetPrivacyLevelROCL(IList<PrivacyLevel> childData)
        {
            return await DataPortal.FetchChildAsync<PrivacyLevelROCL>(childData);
        }

        #endregion 
       
        #region Data Access
        
        [FetchChild]
        private async Task FetchChild(List<PrivacyLevel> childData)
        {
            using (LoadListMode)
            {
                foreach (var privacyLevel in childData)
                {
                    var statusToAdd = await PrivacyLevelROC.GetPrivacyLevelROC(privacyLevel);
                    Add(statusToAdd);             
                }
            }
        }
       
        #endregion
    }
}
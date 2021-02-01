using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class EMailROCL : ReadOnlyListBase<EMailROCL,EMailROC>
    {
        #region Business Rules
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
        
        #region Factory Methods
        
        internal static async Task<EMailROCL> GetEMailROCL(IList<EMail> childData)
        {
            return await DataPortal.FetchChildAsync<EMailROCL>(childData);
        }

        #endregion 
       
        #region Data Access
        
        [FetchChild]
        private async Task FetchChild(List<EMail> childData)
        {
            using (LoadListMode)
            {
                foreach (var eMail in childData)
                {
                    var statusToAdd = await EMailROC.GetEMailROC(eMail);
                    Add(statusToAdd);             
                }
            }
        }
       
        #endregion
    }
}
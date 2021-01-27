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
    public class EMailTypeROCL : ReadOnlyListBase<EMailTypeROCL,EMailTypeROC>
    {
        #region Business Rules
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
        
        #region Factory Methods
        
        internal static async Task<EMailTypeROCL> GetEMailTypeROCL(IList<EMailType> childData)
        {
            return await DataPortal.FetchChildAsync<EMailTypeROCL>(childData);
        }

        #endregion 
       
        #region Data Access
        
        [FetchChild]
        private async Task FetchChild(List<EMailType> childData)
        {
            using (LoadListMode)
            {
                foreach (var eMailType in childData)
                {
                    var docTypeToAdd = await EMailTypeROC.GetEMailTypeROC(eMailType);
                    Add(docTypeToAdd);             
                }
            }
        }
       
        #endregion
    }
}
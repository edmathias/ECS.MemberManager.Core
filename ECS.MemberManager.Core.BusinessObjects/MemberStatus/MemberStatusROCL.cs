using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class MemberStatusROCL : ReadOnlyListBase<MemberStatusROCL,MemberStatusROC>
    {
        #region Business Rules
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
        
        #region Factory Methods
        
        internal static async Task<MemberStatusROCL> GetMemberStatusROCL(IList<MemberStatus> childData)
        {
            return await DataPortal.FetchChildAsync<MemberStatusROCL>(childData);
        }

        #endregion 
       
        #region Data Access
        
        [FetchChild]
        private async Task FetchChild(List<MemberStatus> childData)
        {
            using (LoadListMode)
            {
                foreach (var memberStatus in childData)
                {
                    var statusToAdd = await MemberStatusROC.GetMemberStatusROC(memberStatus);
                    Add(statusToAdd);             
                }
            }
        }
       
        #endregion
    }
}
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
    public class MembershipTypeROCL : ReadOnlyListBase<MembershipTypeROCL,MembershipTypeROC>
    {
        #region Business Rules
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
        
        #region Factory Methods
        
        internal static async Task<MembershipTypeROCL> GetMembershipTypeROCL(IList<MembershipType> childData)
        {
            return await DataPortal.FetchChildAsync<MembershipTypeROCL>(childData);
        }

        #endregion 
       
        #region Data Access
        
        [FetchChild]
        private async Task FetchChild(List<MembershipType> childData)
        {
            using (LoadListMode)
            {
                foreach (var eventObj in childData)
                {
                    var eventToAdd = await MembershipTypeROC.GetMembershipTypeROC(eventObj);
                    Add(eventToAdd);             
                }
            }
        }
       
        #endregion
    }
}
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
    public class EventROCL : ReadOnlyListBase<EventROCL,EventROC>
    {
        #region Business Rules
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
        
        #region Factory Methods
        
        internal static async Task<EventROCL> GetEventROCL(IList<Event> childData)
        {
            return await DataPortal.FetchChildAsync<EventROCL>(childData);
        }

        #endregion 
       
        #region Data Access
        
        [FetchChild]
        private async Task FetchChild(List<Event> childData)
        {
            using (LoadListMode)
            {
                foreach (var eventObj in childData)
                {
                    var eventToAdd = await EventROC.GetEventROC(eventObj);
                    Add(eventToAdd);             
                }
            }
        }
       
        #endregion
    }
}
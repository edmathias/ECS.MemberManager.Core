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
    public class EventDocumentROCL : ReadOnlyListBase<EventDocumentROCL,EventDocumentROC>
    {
        #region Business Rules
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
        
        #region Factory Methods
        
        internal static async Task<EventDocumentROCL> GetEventDocumentROCL(IList<EventDocument> childData)
        {
            return await DataPortal.FetchChildAsync<EventDocumentROCL>(childData);
        }

        #endregion 
       
        #region Data Access
        
        [FetchChild]
        private async Task FetchChild(List<EventDocument> childData)
        {
            using (LoadListMode)
            {
                foreach (var eventObj in childData)
                {
                    var eventToAdd = await EventDocumentROC.GetEventDocumentROC(eventObj);
                    Add(eventToAdd);             
                }
            }
        }
       
        #endregion
    }
}
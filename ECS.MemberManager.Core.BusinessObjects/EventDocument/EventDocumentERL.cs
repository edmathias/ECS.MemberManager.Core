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
    public class EventDocumentERL : BusinessListBase<EventDocumentERL,EventDocumentEC>
    {
        #region Authorization Rules
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
       
        #region Factory Methods
        
        public static async Task<EventDocumentERL> NewEventDocumentERL()
        {
            return await DataPortal.CreateAsync<EventDocumentERL>();
        }

        public static async Task<EventDocumentERL> GetEventDocumentERL()
        {
            return await DataPortal.FetchAsync<EventDocumentERL>();
        }
       
        #endregion
        
        #region Data Access
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDocumentDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var eventObj in childData)
                {
                    var eventToAdd = 
                        await EventDocumentEC.GetEventDocumentEC(eventObj);
                    Add(eventToAdd);
                }
            }
        }
        
        [Update]
        private void Update()
        {
            Child_Update();
        }
        
        #endregion
    }
}
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
    public class EventERL : BusinessListBase<EventERL,EventEC>
    {
        #region Authorization Rules
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
       
        #region Factory Methods
        
        public static async Task<EventERL> NewEventERL()
        {
            return await DataPortal.CreateAsync<EventERL>();
        }

        public static async Task<EventERL> GetEventERL()
        {
            return await DataPortal.FetchAsync<EventERL>();
        }
       
        #endregion
        
        #region Data Access
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var eventObj in childData)
                {
                    var eventToAdd = 
                        await EventEC.GetEventEC(eventObj);
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
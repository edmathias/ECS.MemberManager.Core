using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class EventInfoList : ReadOnlyListBase<EventInfoList,EventInfo>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<EventInfoList> GetEventInfoList()
        {
            return await DataPortal.FetchAsync<EventInfoList>();
        }

        public static async Task<EventInfoList> GetEventInfoList(List<EventInfo> childData)
        {
            return await DataPortal.FetchAsync<EventInfoList>(childData);
        }
        
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDal>();
            var childData = await dal.Fetch();

            await Fetch(childData);

        }

        [Fetch]
        private async Task Fetch(List<Event> childData)
        {
            using (LoadListMode)
            {
                foreach (var eventType in childData)
                {
                    var eventToAdd = await EventInfo.GetEventInfo(eventType);
                    Add(eventToAdd);
                }
            }
        }
    }
}
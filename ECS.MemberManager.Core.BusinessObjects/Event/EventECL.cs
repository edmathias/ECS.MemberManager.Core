using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class EventECL : BusinessListBase<EventECL, EventEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<EventECL> NewEventECL()
        {
            return await DataPortal.CreateAsync<EventECL>();
        }

        internal static async Task<EventECL> GetEventECL(List<Event> childData)
        {
            return await DataPortal.FetchAsync<EventECL>(childData);
        }

        [Fetch]
        private async Task Fetch(List<Event> childData)
        {
            using (LoadListMode)
            {
                foreach (var eventObj in childData)
                {
                    var eventToAdd = await EventEC.GetEventEC(eventObj);
                    Add(eventToAdd);
                }
            }
        }

        [Update]
        private void Update()
        {
            Child_Update();
        }
    }
}
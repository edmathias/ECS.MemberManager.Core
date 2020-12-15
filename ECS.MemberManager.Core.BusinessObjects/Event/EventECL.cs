using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class EventECL : BusinessListBase<EventECL, EventEC>
    {
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<EventECL> NewEventList()
        {
            return await DataPortal.CreateChildAsync<EventECL>();
        }

        internal static async Task<EventECL> GetEventList(IList<Event> listOfChildren)
        {
            return await DataPortal.FetchChildAsync<EventECL>(listOfChildren);
        }

        [FetchChild]
        private async void FetchChild(IList<Event> listOfChildren)
        {
            RaiseListChangedEvents = false;
            
            foreach (var eventData in listOfChildren)
            {
                this.Add(await EventEC.GetEvent(eventData));
            }

            RaiseListChangedEvents = true;
        }
            
    }
}
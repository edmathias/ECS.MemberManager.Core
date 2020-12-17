using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class EventERL : BusinessListBase<EventERL, EventEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<EventERL> NewEventList()
        {
            return await DataPortal.CreateAsync<EventERL>();
        }

        public static async Task<EventERL> GetEventList(IList<Event> listOfChildren)
        {
            return await DataPortal.FetchAsync<EventERL>(listOfChildren);
        }

        [Fetch]
        private async void Fetch(IList<Event> listOfChildren)
        {
            RaiseListChangedEvents = false;
            
            foreach (var eventData in listOfChildren)
            {
                this.Add(await EventEC.GetEvent(eventData));
            }
            
            RaiseListChangedEvents = true;
        }

        [Update]
        private void Update()
        {
            base.Child_Update();
        }
    }
}
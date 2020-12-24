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
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<EventECL> NewEventList()
        {
            return await DataPortal.CreateAsync<EventECL>();
        }

        public static async Task<EventECL> GetEventList(IList<Event> listOfChildren)
        {
            return await DataPortal.FetchAsync<EventECL>(listOfChildren);
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
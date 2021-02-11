using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class EventDocumentECL : BusinessListBase<EventDocumentECL, EventDocumentEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<EventDocumentECL> NewEventDocumentECL()
        {
            return await DataPortal.CreateAsync<EventDocumentECL>();
        }

        internal static async Task<EventDocumentECL> GetEventDocumentECL(List<EventDocument> childData)
        {
            return await DataPortal.FetchAsync<EventDocumentECL>(childData);
        }

        [Fetch]
        private async Task Fetch(List<EventDocument> childData)
        {
            using (LoadListMode)
            {
                foreach (var eventObj in childData)
                {
                    var eventToAdd = await EventDocumentEC.GetEventDocumentEC(eventObj);
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
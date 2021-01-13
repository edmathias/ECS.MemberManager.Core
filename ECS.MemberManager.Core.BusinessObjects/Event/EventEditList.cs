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
    public class EventEditList : BusinessListBase<EventEditList,EventEdit>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<EventEditList> NewEventEditList()
        {
            return await DataPortal.CreateAsync<EventEditList>();
        }

        public static async Task<EventEditList> GetEventEditList()
        {
            return await DataPortal.FetchAsync<EventEditList>();
        }

        public static async Task<EventEditList> GetEventEditList(List<EventEdit> childData)
        {
            return await DataPortal.FetchAsync<EventEditList>(childData);
        }
        
        
        [RunLocal]
        [Create]
        private void Create()
        {
            base.DataPortal_Create();
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
                    var eventTypeToAdd = await EventEdit.GetEventEdit(eventType);
                    Add(eventTypeToAdd);
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
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
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<EventDocumentERL> NewEventDocumentERL()
        {
            return await DataPortal.CreateAsync<EventDocumentERL>();
        }

        public static async Task<EventDocumentERL> GetEventDocumentERL()
        {
            return await DataPortal.FetchAsync<EventDocumentERL>();
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
            var dal = dalManager.GetProvider<IEventDocumentDal>();
            var childData = await dal.Fetch();

            await Fetch(childData);
        }

        private async Task Fetch(List<EventDocument> childData)
        {
            using (LoadListMode)
            {
                foreach (var eventDocument in childData)
                {
                    var eventDocumentToAdd = await EventDocumentEC.GetEventDocumentEC(eventDocument);
                    Add(eventDocumentToAdd);
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
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
    public class EventDocumentEditList : BusinessListBase<EventDocumentEditList,EventDocumentEdit>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<EventDocumentEditList> NewEventDocumentEditList()
        {
            return await DataPortal.CreateAsync<EventDocumentEditList>();
        }

        public static async Task<EventDocumentEditList> GetEventDocumentEditList()
        {
            return await DataPortal.FetchAsync<EventDocumentEditList>();
        }

        public static async Task<EventDocumentEditList> GetEventDocumentEditList(List<EventDocumentEdit> childData)
        {
            return await DataPortal.FetchAsync<EventDocumentEditList>(childData);
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

        [Fetch]
        private async Task Fetch(List<EventDocument> childData)
        {
            using (LoadListMode)
            {
                foreach (var eventDocument in childData)
                {
                    var eventDocumentToAdd = await EventDocumentEdit.GetEventDocumentEdit(eventDocument);
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
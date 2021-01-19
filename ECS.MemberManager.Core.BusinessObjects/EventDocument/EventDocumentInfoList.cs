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
    public class EventDocumentInfoList : ReadOnlyListBase<EventDocumentInfoList,EventDocumentInfo>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<EventDocumentInfoList> NewEventDocumentInfoList()
        {
            return await DataPortal.CreateAsync<EventDocumentInfoList>();
        }

        public static async Task<EventDocumentInfoList> GetEventDocumentInfoList()
        {
            return await DataPortal.FetchAsync<EventDocumentInfoList>();
        }

        public static async Task<EventDocumentInfoList> GetEventDocumentInfoList(List<EventDocumentInfo> childData)
        {
            return await DataPortal.FetchAsync<EventDocumentInfoList>(childData);
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
                    var eventDocumentToAdd = await EventDocumentInfo.GetEventDocumentInfo(eventDocument);
                    Add(eventDocumentToAdd);
                }
            }
        }
    }
}
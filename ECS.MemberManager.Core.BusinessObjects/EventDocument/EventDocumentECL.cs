using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class EventDocumentECL : BusinessListBase<EventDocumentECL, EventDocumentEC>
    {
        #region Factory Methods

        internal static async Task<EventDocumentECL> NewEventDocumentECL()
        {
            return await DataPortal.CreateChildAsync<EventDocumentECL>();
        }

        internal static async Task<EventDocumentECL> GetEventDocumentECL(IList<EventDocument> childData)
        {
            return await DataPortal.FetchChildAsync<EventDocumentECL>(childData);
        }

        #endregion

        #region Data Access

        [FetchChild]
        private async Task Fetch(IList<EventDocument> childData)
        {
            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await EventDocumentEC.GetEventDocumentEC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        [Update]
        private void Update()
        {
            Child_Update();
        }

        #endregion
    }
}
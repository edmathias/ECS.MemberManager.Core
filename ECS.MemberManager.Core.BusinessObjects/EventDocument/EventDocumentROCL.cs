


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
    public partial class EventDocumentROCL : ReadOnlyListBase<EventDocumentROCL,EventDocumentROC>
    {
        #region Factory Methods


        internal static async Task<EventDocumentROCL> GetEventDocumentROCL(IList<EventDocument> childData)
        {
            return await DataPortal.FetchChildAsync<EventDocumentROCL>(childData);
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
                    var objectToAdd = await EventDocumentROC.GetEventDocumentROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}

using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class EventDocumentRORL : ReadOnlyListBase<EventDocumentRORL, EventDocumentROC>
    {
        #region Factory Methods

        public static async Task<EventDocumentRORL> GetEventDocumentRORL()
        {
            return await DataPortal.FetchAsync<EventDocumentRORL>();
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task Fetch([Inject] IEventDocumentDal dal)
        {
            var childData = await dal.Fetch();

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
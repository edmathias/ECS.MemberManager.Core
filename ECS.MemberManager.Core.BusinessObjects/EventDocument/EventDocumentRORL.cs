


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
    public partial class EventDocumentRORL : ReadOnlyListBase<EventDocumentRORL,EventDocumentROC>
    {
        #region Factory Methods

        public static async Task<EventDocumentRORL> NewEventDocumentRORL()
        {
            return await DataPortal.CreateAsync<EventDocumentRORL>();
        }

        public static async Task<EventDocumentRORL> GetEventDocumentRORL( )
        {
            return await DataPortal.FetchAsync<EventDocumentRORL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDocumentDal>();
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

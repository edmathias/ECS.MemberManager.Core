


using System; 
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class EventMemberRORL : ReadOnlyListBase<EventMemberRORL,EventMemberROC>
    {
        #region Factory Methods


        public static async Task<EventMemberRORL> GetEventMemberRORL( )
        {
            return await DataPortal.FetchAsync<EventMemberRORL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch([Inject] IEventMemberDal dal)
        {
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await EventMemberROC.GetEventMemberROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}

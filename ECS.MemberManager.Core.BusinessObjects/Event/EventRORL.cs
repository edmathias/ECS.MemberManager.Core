using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class EventRORL : ReadOnlyListBase<EventRORL,EventROC>
    {
        #region Business Methods
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }
        
        #endregion
        
        #region Factory Methods

        public static async Task<EventRORL> GetEventRORL()
        {
            return await DataPortal.FetchAsync<EventRORL>();
        }
        
        #endregion
        
        #region Data Access

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var eMailType in childData)
                {
                    var eMailTypeToAdd = await EventROC.GetEventROC(eMailType);
                    Add(eMailTypeToAdd);
                }
            }
        }
        
        #endregion
    }
}
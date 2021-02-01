using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class EMailRORL : ReadOnlyListBase<EMailRORL,EMailROC>
    {
        #region Business Methods
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }
        
        #endregion
        
        #region Factory Methods

        public static async Task<EMailRORL> GetEMailRORL()
        {
            return await DataPortal.FetchAsync<EMailRORL>();
        }
        
        #endregion
        
        #region Data Access

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var paymentType in childData)
                {
                    var paymentTypeToAdd = await EMailROC.GetEMailROC(paymentType);
                    Add(paymentTypeToAdd);
                }
            }
        }
        
        #endregion
    }
}
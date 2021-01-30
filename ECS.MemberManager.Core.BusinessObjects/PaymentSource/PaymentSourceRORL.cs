using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PaymentSourceRORL : ReadOnlyListBase<PaymentSourceRORL,PaymentSourceROC>
    {
        #region Business Methods
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }
        
        #endregion
        
        #region Factory Methods

        public static async Task<PaymentSourceRORL> GetPaymentSourceRORL()
        {
            return await DataPortal.FetchAsync<PaymentSourceRORL>();
        }
        
        #endregion
        
        #region Data Access

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentSourceDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var paymentSource in childData)
                {
                    var paymentSourceToAdd = await PaymentSourceROC.GetPaymentSourceROC(paymentSource);
                    Add(paymentSourceToAdd);
                }
            }
        }
        
        #endregion
    }
}
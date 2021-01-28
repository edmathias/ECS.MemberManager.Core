using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PaymentTypeRORL : ReadOnlyListBase<PaymentTypeRORL,PaymentTypeROC>
    {
        #region Business Methods
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }
        
        #endregion
        
        #region Factory Methods

        public static async Task<PaymentTypeRORL> GetPaymentTypeRORL()
        {
            return await DataPortal.FetchAsync<PaymentTypeRORL>();
        }
        
        #endregion
        
        #region Data Access

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentTypeDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var paymentType in childData)
                {
                    var paymentTypeToAdd = await PaymentTypeROC.GetPaymentTypeROC(paymentType);
                    Add(paymentTypeToAdd);
                }
            }
        }
        
        #endregion
    }
}
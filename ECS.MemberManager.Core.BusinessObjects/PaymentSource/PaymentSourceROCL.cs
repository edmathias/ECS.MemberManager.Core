using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PaymentSourceROCL : ReadOnlyListBase<PaymentSourceROCL,PaymentSourceROC>
    {
        #region Business Rules
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
        
        #region Factory Methods
        
        internal static async Task<PaymentSourceROCL> GetPaymentSourceROCL(IList<PaymentSource> childData)
        {
            return await DataPortal.FetchChildAsync<PaymentSourceROCL>(childData);
        }

        #endregion 
       
        #region Data Access
        
        [FetchChild]
        private async Task FetchChild(List<PaymentSource> childData)
        {
            using (LoadListMode)
            {
                foreach (var paymentSource in childData)
                {
                    var statusToAdd = await PaymentSourceROC.GetPaymentSourceROC(paymentSource);
                    Add(statusToAdd);             
                }
            }
        }
       
        #endregion
    }
}
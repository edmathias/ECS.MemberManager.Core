using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PaymentTypeROCL : ReadOnlyListBase<PaymentTypeROCL,PaymentTypeROC>
    {
        #region Business Rules
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
        
        #region Factory Methods
        
        internal static async Task<PaymentTypeROCL> GetPaymentTypeROCL(IList<PaymentType> childData)
        {
            return await DataPortal.FetchChildAsync<PaymentTypeROCL>(childData);
        }

        #endregion 
       
        #region Data Access
        
        [FetchChild]
        private async Task FetchChild(List<PaymentType> childData)
        {
            using (LoadListMode)
            {
                foreach (var paymentType in childData)
                {
                    var statusToAdd = await PaymentTypeROC.GetPaymentTypeROC(paymentType);
                    Add(statusToAdd);             
                }
            }
        }
       
        #endregion
    }
}
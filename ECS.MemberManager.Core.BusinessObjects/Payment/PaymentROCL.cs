﻿


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
    public partial class PaymentROCL : ReadOnlyListBase<PaymentROCL,PaymentROC>
    {
        #region Factory Methods

        internal static async Task<PaymentROCL> NewPaymentROCL()
        {
            return await DataPortal.CreateChildAsync<PaymentROCL>();
        }

        internal static async Task<PaymentROCL> GetPaymentROCL(List<Payment> childData)
        {
            return await DataPortal.FetchChildAsync<PaymentROCL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(List<Payment> childData)
        {

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await PaymentROC.GetPaymentROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}
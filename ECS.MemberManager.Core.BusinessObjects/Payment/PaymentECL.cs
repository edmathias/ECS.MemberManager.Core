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
    public partial class PaymentECL : BusinessListBase<PaymentECL,PaymentEC>
    {
        #region Factory Methods

        internal static async Task<PaymentECL> NewPaymentECL()
        {
            return await DataPortal.CreateChildAsync<PaymentECL>();
        }

        internal static async Task<PaymentECL> GetPaymentECL(List<Payment> childData)
        {
            return await DataPortal.FetchChildAsync<PaymentECL>(childData);
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
                    var objectToAdd = await PaymentEC.GetPaymentEC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }
       
        [Update]
        private void Update()
        {
            Child_Update();
        }

        #endregion

     }
}
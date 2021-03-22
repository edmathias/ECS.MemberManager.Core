


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/18/2021 16:28:31
//******************************************************************************    

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
    public partial class PaymentSourceECL : BusinessListBase<PaymentSourceECL,PaymentSourceEC>
    {
        #region Factory Methods

        internal static async Task<PaymentSourceECL> NewPaymentSourceECL()
        {
            return await DataPortal.CreateChildAsync<PaymentSourceECL>();
        }

        internal static async Task<PaymentSourceECL> GetPaymentSourceECL(List<PaymentSource> childData)
        {
            return await DataPortal.FetchChildAsync<PaymentSourceECL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(List<PaymentSource> childData)
        {

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await PaymentSourceEC.GetPaymentSourceEC(domainObjToAdd);
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

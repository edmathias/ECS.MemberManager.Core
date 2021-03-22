


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/18/2021 16:28:33
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
    public partial class PaymentTypeECL : BusinessListBase<PaymentTypeECL,PaymentTypeEC>
    {
        #region Factory Methods

        internal static async Task<PaymentTypeECL> NewPaymentTypeECL()
        {
            return await DataPortal.CreateChildAsync<PaymentTypeECL>();
        }

        internal static async Task<PaymentTypeECL> GetPaymentTypeECL(List<PaymentType> childData)
        {
            return await DataPortal.FetchChildAsync<PaymentTypeECL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(List<PaymentType> childData)
        {

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await PaymentTypeEC.GetPaymentTypeEC(domainObjToAdd);
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

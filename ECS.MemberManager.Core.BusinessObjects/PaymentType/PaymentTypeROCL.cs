


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
    public partial class PaymentTypeROCL : ReadOnlyListBase<PaymentTypeROCL,PaymentTypeROC>
    {
        #region Factory Methods

        internal static async Task<PaymentTypeROCL> NewPaymentTypeROCL()
        {
            return await DataPortal.CreateChildAsync<PaymentTypeROCL>();
        }

        internal static async Task<PaymentTypeROCL> GetPaymentTypeROCL(List<PaymentType> childData)
        {
            return await DataPortal.FetchChildAsync<PaymentTypeROCL>(childData);
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
                    var objectToAdd = await PaymentTypeROC.GetPaymentTypeROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}




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
    public partial class PaymentSourceROCL : ReadOnlyListBase<PaymentSourceROCL,PaymentSourceROC>
    {
        #region Factory Methods


        internal static async Task<PaymentSourceROCL> GetPaymentSourceROCL(List<PaymentSource> childData)
        {
            return await DataPortal.FetchChildAsync<PaymentSourceROCL>(childData);
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
                    var objectToAdd = await PaymentSourceROC.GetPaymentSourceROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}

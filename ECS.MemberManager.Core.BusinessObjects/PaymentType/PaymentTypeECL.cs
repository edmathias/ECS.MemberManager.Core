using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PaymentTypeECL : BusinessListBase<PaymentTypeECL, PaymentTypeEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<PaymentTypeECL> NewPaymentTypeECL()
        {
            return await DataPortal.CreateAsync<PaymentTypeECL>();
        }

        internal static async Task<PaymentTypeECL> GetPaymentTypeECL(List<PaymentType> childData)
        {
            return await DataPortal.FetchAsync<PaymentTypeECL>(childData);
        }

        [Fetch]
        private async Task Fetch(List<PaymentType> childData)
        {
            using (LoadListMode)
            {
                foreach (var PaymentType in childData)
                {
                    var PaymentTypeToAdd = await PaymentTypeEC.GetPaymentTypeEC(PaymentType);
                    Add(PaymentTypeToAdd);
                }
            }
        }

        [Update]
        private void Update()
        {
            Child_Update();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PaymentSourceECL : BusinessListBase<PaymentSourceECL, PaymentSourceEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<PaymentSourceECL> NewPaymentSourceECL()
        {
            return await DataPortal.CreateAsync<PaymentSourceECL>();
        }

        internal static async Task<PaymentSourceECL> GetPaymentSourceECL(List<PaymentSource> childData)
        {
            return await DataPortal.FetchAsync<PaymentSourceECL>(childData);
        }

        [Fetch]
        private async Task Fetch(List<PaymentSource> childData)
        {
            using (LoadListMode)
            {
                foreach (var PaymentSource in childData)
                {
                    var PaymentSourceToAdd = await PaymentSourceEC.GetPaymentSourceEC(PaymentSource);
                    Add(PaymentSourceToAdd);
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
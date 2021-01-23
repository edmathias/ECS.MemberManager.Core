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
    public class PaymentSourceInfoList : ReadOnlyListBase<PaymentSourceInfoList,PaymentSourceInfo>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<PaymentSourceInfoList> NewPaymentSourceInfoList()
        {
            return await DataPortal.CreateAsync<PaymentSourceInfoList>();
        }

        public static async Task<PaymentSourceInfoList> GetPaymentSourceInfoList()
        {
            return await DataPortal.FetchAsync<PaymentSourceInfoList>();
        }

        public static async Task<PaymentSourceInfoList> GetPaymentSourceInfoList(List<PaymentSourceInfo> childData)
        {
            return await DataPortal.FetchAsync<PaymentSourceInfoList>(childData);
        }

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentSourceDal>();
            var childData = await dal.Fetch();

            await Fetch(childData);
        }

        [Fetch]
        private async Task Fetch(List<PaymentSource> childData)
        {
            using (LoadListMode)
            {
                foreach (var paymentSource in childData)
                {
                    var paymentSourceToAdd = await PaymentSourceInfo.GetPaymentSourceInfo(paymentSource);
                    Add(paymentSourceToAdd);
                }
            }
        }
    }
}
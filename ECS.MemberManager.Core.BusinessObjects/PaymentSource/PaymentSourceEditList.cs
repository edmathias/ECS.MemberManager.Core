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
    public class PaymentSourceEditList : BusinessListBase<PaymentSourceEditList,PaymentSourceEdit>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<PaymentSourceEditList> NewPaymentSourceEditList()
        {
            return await DataPortal.CreateAsync<PaymentSourceEditList>();
        }

        public static async Task<PaymentSourceEditList> GetPaymentSourceEditList()
        {
            return await DataPortal.FetchAsync<PaymentSourceEditList>();
        }

        public static async Task<PaymentSourceEditList> GetPaymentSourceEditList(List<PaymentSourceEdit> childData)
        {
            return await DataPortal.FetchAsync<PaymentSourceEditList>(childData);
        }
        
        
        [RunLocal]
        [Create]
        private void Create()
        {
            base.DataPortal_Create();
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
                    var paymentSourceToAdd = await PaymentSourceEdit.GetPaymentSourceEdit(paymentSource);
                    Add(paymentSourceToAdd);
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
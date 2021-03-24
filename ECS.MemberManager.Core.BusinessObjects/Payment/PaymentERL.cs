//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:57:29
//******************************************************************************    

using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class PaymentERL : BusinessListBase<PaymentERL, PaymentEC>
    {
        #region Factory Methods

        public static async Task<PaymentERL> NewPaymentERL()
        {
            return await DataPortal.CreateAsync<PaymentERL>();
        }

        public static async Task<PaymentERL> GetPaymentERL()
        {
            return await DataPortal.FetchAsync<PaymentERL>();
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task Fetch([Inject] IPaymentDal dal)
        {
            var childData = await dal.Fetch();

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
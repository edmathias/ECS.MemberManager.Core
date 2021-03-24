//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:57:35
//******************************************************************************    

using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class PaymentTypeERL : BusinessListBase<PaymentTypeERL, PaymentTypeEC>
    {
        #region Factory Methods

        public static async Task<PaymentTypeERL> NewPaymentTypeERL()
        {
            return await DataPortal.CreateAsync<PaymentTypeERL>();
        }

        public static async Task<PaymentTypeERL> GetPaymentTypeERL()
        {
            return await DataPortal.FetchAsync<PaymentTypeERL>();
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task Fetch([Inject] IPaymentTypeDal dal)
        {
            var childData = await dal.Fetch();

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
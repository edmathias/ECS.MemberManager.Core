


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/09/2021 14:34:39
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
    public partial class PaymentTypeERL : BusinessListBase<PaymentTypeERL,PaymentTypeEC>
    {
        #region Factory Methods

        public static async Task<PaymentTypeERL> NewPaymentTypeERL()
        {
            return await DataPortal.CreateAsync<PaymentTypeERL>();
        }

        public static async Task<PaymentTypeERL> GetPaymentTypeERL( )
        {
            return await DataPortal.FetchAsync<PaymentTypeERL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentTypeDal>();
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

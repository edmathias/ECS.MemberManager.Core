


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 04/07/2021 09:32:31
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
    public partial class PaymentSourceERL : BusinessListBase<PaymentSourceERL,PaymentSourceEC>
    {
        #region Factory Methods

        public static async Task<PaymentSourceERL> NewPaymentSourceERL()
        {
            return await DataPortal.CreateAsync<PaymentSourceERL>();
        }

        public static async Task<PaymentSourceERL> GetPaymentSourceERL( )
        {
            return await DataPortal.FetchAsync<PaymentSourceERL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch([Inject] IDal<PaymentSource> dal)
        {
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await PaymentSourceEC.GetPaymentSourceEC(domainObjToAdd);
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

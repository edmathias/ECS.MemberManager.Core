﻿


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/03/2021 13:27:48
//******************************************************************************    

using System; 
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class PaymentRORL : ReadOnlyListBase<PaymentRORL,PaymentROC>
    {
        #region Factory Methods


        public static async Task<PaymentRORL> GetPaymentRORL( )
        {
            return await DataPortal.FetchAsync<PaymentRORL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await PaymentROC.GetPaymentROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}

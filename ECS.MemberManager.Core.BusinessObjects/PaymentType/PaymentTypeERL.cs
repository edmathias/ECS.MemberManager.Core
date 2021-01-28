using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PaymentTypeERL : BusinessListBase<PaymentTypeERL,PaymentTypeEC>
    {
        #region Authorization Rules
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
       
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
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentTypeDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var PaymentType in childData)
                {
                    var PaymentTypeToAdd = 
                        await PaymentTypeEC.GetPaymentTypeEC(PaymentType);
                    Add(PaymentTypeToAdd);
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
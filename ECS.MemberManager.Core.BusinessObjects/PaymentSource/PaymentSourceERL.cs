using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PaymentSourceERL : BusinessListBase<PaymentSourceERL,PaymentSourceEC>
    {
        #region Authorization Rules
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
       
        #region Factory Methods
        
        public static async Task<PaymentSourceERL> NewPaymentSourceERL()
        {
            return await DataPortal.CreateAsync<PaymentSourceERL>();
        }

        public static async Task<PaymentSourceERL> GetPaymentSourceERL()
        {
            return await DataPortal.FetchAsync<PaymentSourceERL>();
        }
       
        #endregion
        
        #region Data Access
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentSourceDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var PaymentSource in childData)
                {
                    var PaymentSourceToAdd = 
                        await PaymentSourceEC.GetPaymentSourceEC(PaymentSource);
                    Add(PaymentSourceToAdd);
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
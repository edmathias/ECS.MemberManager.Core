


using System; 
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class PaymentSourceRORL : ReadOnlyListBase<PaymentSourceRORL,PaymentSourceROC>
    {
        #region Factory Methods

        public static async Task<PaymentSourceRORL> NewPaymentSourceRORL()
        {
            return await DataPortal.CreateAsync<PaymentSourceRORL>();
        }

        public static async Task<PaymentSourceRORL> GetPaymentSourceRORL( )
        {
            return await DataPortal.FetchAsync<PaymentSourceRORL>();
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
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await PaymentSourceROC.GetPaymentSourceROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}

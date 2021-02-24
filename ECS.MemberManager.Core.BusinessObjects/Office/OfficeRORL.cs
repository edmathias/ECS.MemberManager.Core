


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
    public partial class MembershipTypeRORL : ReadOnlyListBase<MembershipTypeRORL,MembershipTypeROC>
    {
        #region Factory Methods

        public static async Task<MembershipTypeRORL> NewMembershipTypeRORL()
        {
            return await DataPortal.CreateAsync<MembershipTypeRORL>();
        }

        public static async Task<MembershipTypeRORL> GetMembershipTypeRORL( )
        {
            return await DataPortal.FetchAsync<MembershipTypeRORL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await MembershipTypeROC.GetMembershipTypeROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}

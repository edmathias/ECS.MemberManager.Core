using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class MemberStatusRORL : ReadOnlyListBase<MemberStatusRORL,MemberStatusROC>
    {
        #region Business Methods
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }
        
        #endregion
        
        #region Factory Methods

        public static async Task<MemberStatusRORL> GetMemberStatusRORL()
        {
            return await DataPortal.FetchAsync<MemberStatusRORL>();
        }
        
        #endregion
        
        #region Data Access

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberStatusDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var memberStatus in childData)
                {
                    var memberStatusToAdd = await MemberStatusROC.GetMemberStatusROC(memberStatus);
                    Add(memberStatusToAdd);
                }
            }
        }
        
        #endregion
    }
}
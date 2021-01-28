using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class MemberStatusERL : BusinessListBase<MemberStatusERL,MemberStatusEC>
    {
        #region Authorization Rules
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
       
        #region Factory Methods
        
        public static async Task<MemberStatusERL> NewMemberStatusERL()
        {
            return await DataPortal.CreateAsync<MemberStatusERL>();
        }

        public static async Task<MemberStatusERL> GetMemberStatusERL()
        {
            return await DataPortal.FetchAsync<MemberStatusERL>();
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
                    var memberStatusToAdd = 
                        await MemberStatusEC.GetMemberStatusEC(memberStatus);
                    Add(memberStatusToAdd);
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
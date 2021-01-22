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
    public class MemberStatusInfoList : ReadOnlyListBase<MemberStatusInfoList,MemberStatusInfo>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<MemberStatusInfoList> GetMemberStatusInfoList()
        {
            return await DataPortal.FetchAsync<MemberStatusInfoList>();
        }

        public static async Task<MemberStatusInfoList> GetMemberStatusInfoList(IList<MemberStatus> listOfChildren)
        {
            return await DataPortal.FetchChildAsync<MemberStatusInfoList>(listOfChildren);
        }

        [RunLocal]
        [Create]
        private void Create()
        {
        }
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberStatusDal>();
            var childData = await dal.Fetch();

            await Fetch(childData);
        }
 
        [Fetch]
        private async Task Fetch(List<MemberStatus> childData)
        {
            using (LoadListMode)
            {
                foreach (var memberStatus in childData)
                {
                    Add(await MemberStatusInfo.GetMemberStatusInfo(memberStatus));             
                }
            }
        }
        
    }
}
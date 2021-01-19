using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PrivacyLevelInfoList : ReadOnlyListBase<PrivacyLevelInfoList,PrivacyLevelInfo>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<PrivacyLevelInfoList> GetPrivacyLevelInfoList()
        {
            return await DataPortal.FetchAsync<PrivacyLevelInfoList>();
        }

        public static async Task<PrivacyLevelInfoList> GetPrivacyLevelInfoList(List<PrivacyLevelInfo> childData)
        {
            return await DataPortal.FetchChildAsync<PrivacyLevelInfoList>(childData);
        }
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPrivacyLevelDal>();
            var childData = await dal.Fetch();

            await Fetch(childData);

        }

        [FetchChild]
        private async Task Fetch(List<PrivacyLevel> childData)
        {
            using (LoadListMode)
            {
                foreach (var privacyLevel in childData)
                {
                    var privacyLevelToAdd = await PrivacyLevelInfo.GetPrivacyLevelInfo(privacyLevel);
                    Add(privacyLevelToAdd);
                }
            }
        }
    }
}
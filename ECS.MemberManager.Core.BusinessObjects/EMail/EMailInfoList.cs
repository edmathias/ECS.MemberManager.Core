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
    public class EMailInfoList : ReadOnlyListBase<EMailInfoList,EMailInfo>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<EMailInfoList> GetEMailInfoList()
        {
            return await DataPortal.FetchAsync<EMailInfoList>();
        }

        public static async Task<EMailInfoList> GetEMailInfoList(List<EMailInfo> childData)
        {
            return await DataPortal.FetchAsync<EMailInfoList>(childData);
        }

        [Fetch]
        private async void Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();
            var childData = dal.Fetch();

            await Fetch(childData);
        }

        [Fetch]
        private async Task Fetch(List<EMail> childData)
        {
            using (LoadListMode)
            {
                foreach (var eMailType in childData)
                {
                    var eMailTypeToAdd = await EMailInfo.GetEMailInfo(eMailType);
                    Add(eMailTypeToAdd);
                }
            }
        }
    }
}
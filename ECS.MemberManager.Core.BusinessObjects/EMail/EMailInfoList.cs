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

        public static async Task<EMailInfoList> NewEMailInfoList()
        {
            return await DataPortal.CreateAsync<EMailInfoList>();
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
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();
            var childData = await dal.Fetch();

            await Fetch(childData);

        }

        [Fetch]
        private async Task Fetch(List<EMail> childData)
        {
            using (LoadListMode)
            {
                foreach (var eMail in childData)
                {
                    var eMailToAdd = await EMailInfo.GetEMailInfo(eMail);
                    Add(eMailToAdd);
                }
            }
        }
    }
}
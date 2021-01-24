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
    public class PhoneInfoList : ReadOnlyListBase<PhoneInfoList,PhoneInfo>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<PhoneInfoList> GetPhoneInfoList()
        {
            return await DataPortal.FetchAsync<PhoneInfoList>();
        }

        public static async Task<PhoneInfoList> GetPhoneInfoList(List<PhoneInfo> childData)
        {
            return await DataPortal.FetchAsync<PhoneInfoList>(childData);
        }

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPhoneDal>();
            var childData = await dal.Fetch();

            await FetchChild(childData);
        }

        [FetchChild]
        private async Task FetchChild(List<Phone> childData)
        {
            using (LoadListMode)
            {
                foreach (var phone in childData)
                {
                    var phoneToAdd = await PhoneInfo.GetPhoneInfo(phone);
                    Add(phoneToAdd);
                }
            }
        }

    }
}
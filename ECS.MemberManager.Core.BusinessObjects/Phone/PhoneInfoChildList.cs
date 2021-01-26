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
    public class PhoneInfoChildList : ReadOnlyListBase<PhoneInfoChildList,PhoneInfoChild>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<PhoneInfoChildList> GetPhoneInfoChildList()
        {
            return await DataPortal.FetchAsync<PhoneInfoChildList>();
        }

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPhoneDal>();
            var childData = await dal.Fetch();
            
            using (LoadListMode)
            {
                foreach (var phone in childData)
                {
                    var phoneToAdd = await PhoneInfoChild.GetPhoneInfoChild(phone);
                    Add(phoneToAdd);
                }
            }
        }
    }
}
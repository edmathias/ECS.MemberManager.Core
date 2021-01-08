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
    public class AddressInfoList : ReadOnlyListBase<AddressInfoList,AddressInfo>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<AddressInfoList> GetAddressInfoList()
        {
            return await DataPortal.FetchAsync<AddressInfoList>();
        }

        public static async Task<AddressInfoList> GetAddressInfoList(List<AddressInfo> childData)
        {
            return await DataPortal.FetchAsync<AddressInfoList>(childData);
        }
        
        
        [Fetch]
        private async void Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
            var childData = dal.Fetch();

            await Fetch(childData);

        }

        [Fetch]
        private async Task Fetch(List<Address> childData)
        {
            using (LoadListMode)
            {
                foreach (var eMailType in childData)
                {
                    var eMailTypeToAdd = await AddressInfo.GetAddressInfo(eMailType);
                    Add(eMailTypeToAdd);
                }
            }
        }
    }
}
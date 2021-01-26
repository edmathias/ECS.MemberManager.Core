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
    public class AddressRORL : ReadOnlyListBase<AddressRORL,AddressROC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<AddressRORL> GetAddressRORL()
        {
            return await DataPortal.FetchAsync<AddressRORL>();
        }

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var address in childData)
                {
                    var addressToAdd = await AddressROC.GetAddressROC(address);
                    Add(addressToAdd);
                }
            }
        }
    }
}
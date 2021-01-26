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
    public class AddressROCL : ReadOnlyListBase<AddressROCL,AddressROC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<AddressROCL> GetAddressROCL(List<Address> childData)
        {
            return await DataPortal.FetchChildAsync<AddressROCL>(childData);
        }

        [FetchChild]
        private async Task Fetch(List<Address> childData)
        {
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
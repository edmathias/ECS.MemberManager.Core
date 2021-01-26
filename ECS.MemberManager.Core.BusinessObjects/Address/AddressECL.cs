using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class AddressECL : BusinessListBase<AddressECL,AddressEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<AddressECL> NewAddressECL()
        {
            return await DataPortal.CreateAsync<AddressECL>();
        }

        internal static async Task<AddressECL> GetAddressECL(List<Address> childData)
        {
            return await DataPortal.FetchAsync<AddressECL>(childData);
        }
        
        [Fetch]
        private async Task Fetch(List<Address> childData)
        {
            using (LoadListMode)
            {
                foreach (var address in childData)
                {
                    var addressToAdd = await AddressEC.GetAddressEC(address);
                    Add(addressToAdd);
                }
            }
        }
        
        [Update]
        private void Update()
        {
            Child_Update();
        }

    }
}
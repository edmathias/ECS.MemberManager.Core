using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class AddressECL : BusinessListBase<AddressECL, AddressEC>
    {
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<AddressECL> NewAddressList()
        {
            return await DataPortal.CreateChildAsync<AddressECL>();
        }

        public static async Task<AddressECL> GetAddressList(IList<Address> listOfChildren)
        {
            return await DataPortal.FetchChildAsync<AddressECL>(listOfChildren);
        }

        [FetchChild]
        private async void FetchChild(IList<Address> listOfChildren)
        {
            RaiseListChangedEvents = false;
            
            foreach (var addressData in listOfChildren)
            {
                this.Add(await AddressEC.GetAddress(addressData));
            }
            
            RaiseListChangedEvents = true;
        }
            
    }
}
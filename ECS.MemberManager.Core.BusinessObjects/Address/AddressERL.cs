using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class AddressERL : BusinessListBase<AddressERL, AddressEC>
    {
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<AddressERL> NewAddressList()
        {
            return await DataPortal.CreateAsync<AddressERL>();
        }

        public static async Task<AddressERL> GetAddressList(IList<Address> listOfChildren)
        {
            return await DataPortal.FetchAsync<AddressERL>(listOfChildren);
        }

        [Fetch]
        private async void Fetch(IList<Address> listOfChildren)
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
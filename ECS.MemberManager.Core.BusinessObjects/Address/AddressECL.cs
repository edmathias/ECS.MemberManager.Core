using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
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

        internal static async Task<AddressECL> NewAddressList()
        {
            return await DataPortal.CreateChildAsync<AddressECL>();
        }

        internal static async Task<AddressECL> GetAddressList(IList<Address> listOfChildren)
        {
            return await DataPortal.FetchChildAsync<AddressECL>(listOfChildren);
        }

        [FetchChild]
        private async void Fetch(IList<Address> listOfChildren)
        {
            RaiseListChangedEvents = false;
            
            foreach (var childData in listOfChildren)
            {
                this.Add( await AddressEC.GetAddress(childData)  );
            }

            RaiseListChangedEvents = true;
        }
  
    }
}